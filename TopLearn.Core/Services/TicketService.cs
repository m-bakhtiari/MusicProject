using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly TopLearnContext _context;

        public TicketService(TopLearnContext context)
        {
            _context = context;
        }
        public async Task<int> AddTicket(TicketDto ticket)
        {
            var seat = ticket.SeatNumber.Split(",").ToList();
            var model = new ConcertTicket()
            {
                FirstName = ticket.FirstName,
                LastName = ticket.LastName,
                Mobile = ticket.Mobile,
                CreatedDate = DateTime.Now,
                TicketCount = seat.Count,
                Amount = seat.Count * 400000
            };
            var add = await _context.ConcertTickets.AddAsync(model);
            foreach (var item in seat)
            {
                await _context.ConcertTicketSeats.AddAsync(new ConcertTicketSeat()
                {
                    SeatNumber = item,
                    ConcertTicketId = add.Entity.ConcertTicketId,
                    IsPay = false,
                    Description = null,
                    CreatedDate = null
                });
            }

            await _context.SaveChangesAsync();
            return add.Entity.ConcertTicketId;
        }

        public async Task CancelBuyTicket(int ticketId)
        {
            var model = await _context.ConcertTicketSeats.Where(x => x.ConcertTicketId == ticketId).ToListAsync();
            _context.ConcertTicketSeats.RemoveRange(model);
            var ticket = await _context.ConcertTickets.FindAsync(ticketId);
            _context.ConcertTickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task FinalizeBuyTicket(int ticketId, string refId)
        {
            var model = await _context.ConcertTicketSeats.Where(x => x.ConcertTicketId == ticketId).ToListAsync();
            foreach (var item in model)
            {
                item.IsPay = true;
                item.Description = $"کد پیگیری {refId}";
                item.CreatedDate = DateTime.Now;
            }


            await _context.SaveChangesAsync();
        }

        public async Task<List<ConcertTicket>> GetTicketByName(string mobile)
        {
            return await _context.ConcertTickets.Include(x => x.ConcertTicketSeats)
                .Where(x => x.Mobile == mobile).ToListAsync();
        }

        public async Task<List<ConcertTicket>> GetReservedSeat()
        {
            return await _context.ConcertTickets.Include(x => x.ConcertTicketSeats).ToListAsync();
        }

        public async Task<List<ConcertTicketSeat>> GetTickets()
        {
            return await _context.ConcertTicketSeats.ToListAsync();
        }

        public async Task<ConcertTicket> GetTicketById(int id)
        {
            return await _context.ConcertTickets.Include(x => x.ConcertTicketSeats)
                .FirstOrDefaultAsync(x => x.ConcertTicketId == id);
        }

        public async Task<string> FinalizeTicketManual(string mobile, string seat,string nationalCode)
        {
            var ticket = await _context.ConcertTickets.FirstOrDefaultAsync(x => x.Mobile == mobile);
            if (ticket == null)
                return "شماره موبایل معتبر نمی باشد";
            var seatCount = seat.Split(",").Length;
            if (seatCount > ticket.TicketCount)
                return "تعداد بلیط انتخاب شده مجاز نمی باشد";
            var oldseat = await _context.ConcertTicketSeats.CountAsync(x => x.ConcertTicketId == ticket.ConcertTicketId);
            if (oldseat + seatCount > ticket.TicketCount)
                return "تعداد بلیط انتخاب شده مجاز نمی باشد";

            var seatNo = new List<ConcertTicketSeat>();
            foreach (var item in seat.Split(","))
            {
                seatNo.Add(new ConcertTicketSeat()
                {
                    ConcertTicketId = ticket.ConcertTicketId,
                    CreatedDate = DateTime.Now,
                    IsPay = true,
                    Description = nationalCode,
                    SeatNumber = item
                });
            }

            await _context.ConcertTicketSeats.AddRangeAsync(seatNo);
            await _context.SaveChangesAsync();
            return string.Empty;
        }

        public async Task AddTicketManual(ConcertTicket concertTicket)
        {
            var ticket = await _context.ConcertTickets.AnyAsync(x => x.Mobile == concertTicket.Mobile);
            if (ticket)
                return;
            concertTicket.CreatedDate = DateTime.Now;
            await _context.ConcertTickets.AddAsync(concertTicket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicketManual(ConcertTicket concertTicket)
        {
            var ticket = await _context.ConcertTickets.AnyAsync(x => x.Mobile == concertTicket.Mobile && x.ConcertTicketId != concertTicket.ConcertTicketId);
            if (ticket)
                return;
            var data = await _context.ConcertTickets.FirstOrDefaultAsync(x => x.ConcertTicketId == concertTicket.ConcertTicketId);
            data.Amount = concertTicket.Amount;
            data.CreatedDate = data.CreatedDate;
            data.FirstName = concertTicket.FirstName;
            data.LastName = concertTicket.LastName;
            data.Mobile = concertTicket.Mobile;
            data.TicketCount = concertTicket.TicketCount;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketManual(ConcertTicket concertTicket)
        {
            _context.ConcertTickets.Remove(concertTicket);
            await _context.SaveChangesAsync();
        }
    }
}
