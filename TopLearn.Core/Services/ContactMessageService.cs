using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.SiteInput;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly TopLearnContext _context;
        public ContactMessageService(TopLearnContext context)
        {
            _context = context;
        }
        public async Task AddMessage(ContactInput input)
        {
            var model = new ContactMessage()
            {
                CreatedDate = DateTime.Now,
                Email = input.Email,
                IsSeen = false,
                Mobile = input.Mobile,
                Name = input.Name,
                Text = input.Message,
            };
            await _context.ContactMessage.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ContactMessage>> GetContactMessages()
        {
            return await _context.ContactMessage.OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<ContactMessage> GetItemById(int id)
        {
            return await _context.ContactMessage.FindAsync(id);
        }

        public async Task ToggleSeenStatus(int id)
        {
            var model = await _context.ContactMessage.FindAsync(id);
            if (model == null)
                return;
            model.IsSeen = !model.IsSeen;
            await _context.SaveChangesAsync();
        }
    }
}
