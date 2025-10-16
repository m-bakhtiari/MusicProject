using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ITicketService
    {
        Task<int> AddTicket(TicketDto ticket);
        Task<List<ConcertTicket>> GetTicketByName(string mobile);
        Task FinalizeBuyTicket(int ticketId, string refId);
        Task CancelBuyTicket(int ticketId);
        Task<List<ConcertTicket>> GetReservedSeat();
        Task<ConcertTicket> GetTicketById(int id);
        Task<List<ConcertTicketSeat>> GetTickets();
        Task<string> FinalizeTicketManual(string mobile, string seat);
        Task AddTicketManual(ConcertTicket concertTicket);
        Task UpdateTicketManual(ConcertTicket concertTicket);
        Task DeleteTicketManual(ConcertTicket concertTicket);
    }
}
