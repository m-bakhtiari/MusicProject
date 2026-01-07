using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private TopLearnContext _context;
        //private const string ZarinPalApiUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
        //private const string MerchantId = ""; // کد دریافتی از زرین پال
        public TicketController(ITicketService ticketService, TopLearnContext context)
        {
            _ticketService = ticketService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("ReserveTicket")]
        [HttpGet]
        public async Task<IActionResult> ReserveTicket()
        {
            var model = await _ticketService.GetReservedSeat();
            ViewData["Seat"] = "14-17-18-19-20-21-22-23-22-23-22-21-20";

            var ticket = await _ticketService.GetTickets();
            ViewData["SeatReserve"] = ticket.Select(x => x.SeatNumber).ToList();
            ViewData["mobile"] = model.Select(x => x.Mobile);
            return View(model);
        }

        [Route("BuyTicket")]
        [HttpGet]
        public async Task<IActionResult> BuyTicket(string nationalCode)
        {
            var res = await _context.ConcertTicketSeats.Include(x => x.ConcertTicket).FirstOrDefaultAsync(x => x.SeatNumber == nationalCode);

            var data = await _ticketService.GetReservedSeat();
            ViewData["Seat"] = "14-17-18-19-20-21-22-23-22-23-22-21-20";

            var ticket = await _ticketService.GetTickets();
            ViewData["SeatReserve"] = ticket.Select(x => x.SeatNumber).ToList();
            ViewData["mobile"] = data.Select(x => x.Mobile);
            ViewData["Error"] = res.ConcertTicket.FirstName + " " + res.ConcertTicket.LastName;
            return View("ReserveTicket", data);


            //if (string.IsNullOrWhiteSpace(mobile) || mobile.Length != 11 || string.IsNullOrWhiteSpace(selectedSeats) ||
            //    string.IsNullOrWhiteSpace(nationalCode))
            //{
            //    return Redirect("/ReserveTicket");
            //}
            //var res = await _ticketService.FinalizeTicketManual(mobile, selectedSeats, nationalCode);
            //if (string.IsNullOrWhiteSpace(res) == false)
            //{
            //    var data = await _ticketService.GetReservedSeat();
            //    ViewData["Seat"] = "14-17-18-19-20-21-22-23-22-23-22-21-20";

            //    var ticket = await _ticketService.GetTickets();
            //    ViewData["SeatReserve"] = ticket.Select(x => x.SeatNumber).ToList();
            //    ViewData["mobile"] = data.Select(x => x.Mobile);
            //    ViewData["Error"] = res;
            //    return View("ReserveTicket", data);
            //}
            //var model = await _ticketService.GetTicketByName(mobile.Trim());
            //return View("DownloadTicket", model);

            //var reservedSeat = await _ticketService.GetReservedSeat();
            //ViewData["Seat"] = "14-17-18-19-20-21-22-23-22-23-22-21-20";
            //var tickets = await _ticketService.GetTickets();
            //ViewData["SeatReserve"] = tickets.Select(x => x.SeatNumber).ToList();
            //ViewData["mobile"] = reservedSeat.Select(x => x.Mobile);
            //ViewData["Error"] = "";
            //return View("Index", reservedSeat);
        }

        [Route("DownloadTicket")]
        [HttpGet]
        public async Task<IActionResult> DownloadTicket([FromQuery] string mobile)
        {
            var model = await _ticketService.GetTicketByName(mobile.Trim());
            return View(model);
        }

        [HttpGet]
        [Route("OnlinePayment")]
        public async Task<IActionResult> OnlinePayment([FromQuery] int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                   HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                   && HttpContext.Request.Query["Authority"] != "")
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var ticket = await _ticketService.GetTicketById(id);
                var request = new
                {
                    merchant_id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                    amount = ticket.Amount, // مبلغ به ریال
                    authority = HttpContext.Request.Query["Authority"]
                };
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.zarinpal.com/pg/v4/payment/verify.json", content);
                var result = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(result);
                if (data.data.code == 100)
                {
                    string refId = data.data.ref_id;
                    await _ticketService.FinalizeBuyTicket(id, refId);
                    var model = await _ticketService.GetTicketById(id);
                    var mySeat = new List<ConcertTicket> { model };
                    return View("DownloadTicket", mySeat);
                }
            }
            await _ticketService.CancelBuyTicket(id);
            var reservedSeat = await _ticketService.GetReservedSeat();
            ViewData["Seat"] = "14-17-18-19-20-21-22-23-22-23-22-21-20";
            return View("Index", reservedSeat);
        }

        [Route("ZarinpalBuyTicket")]
        [HttpGet]
        public async Task<IActionResult> ZarinpalBuyTicket(string firstname, string lastname, string selectedSeats, string mobile)
        {
            if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname) ||
               string.IsNullOrWhiteSpace(mobile) ||
               mobile.Length != 11 || string.IsNullOrWhiteSpace(selectedSeats))
            {
                var reserved = await _ticketService.GetReservedSeat();
                ViewData["Seat"] = "14-17-18-19-20-21-22-23-22-23-22-21-20";
                return View("Index", reserved);
            }


            var model = new TicketDto()
            {
                Mobile = mobile,
                FirstName = firstname,
                LastName = lastname,
                SeatNumber = selectedSeats
            };
            var ticket = await _ticketService.AddTicket(model);

            #region Online Payment

            var seat = selectedSeats.Split(",").ToList();
            var amount = (seat.Count) * 4000000;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var desc = $"خرید بلیط {firstname} {lastname} - ردیف های {selectedSeats}";
            var data = new
            {
                merchant_id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                amount,
                desc,
                callback_url = "https://vahidnajafizadeh.com/OnlinePayment?id=" + ticket,
                metadata = new { mobile = mobile }
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.zarinpal.com/pg/v4/payment/request.json", content);
            var result = await response.Content.ReadAsStringAsync();

            dynamic res = JsonConvert.DeserializeObject(result);
            if (res.data.code == 100)
            {
                string authority = res.data.authority;
                return Redirect($"https://www.zarinpal.com/pg/StartPay/{authority}");
            }
            #endregion

            var reservedSeat = await _ticketService.GetReservedSeat();
            ViewData["Seat"] = "14-17-18-19-20-21-22-23-22-23-22-21-20";
            return View("Index", reservedSeat);
        }
    }
}
