using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly TopLearnContext _context;
        public SubscriberService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task AddSubscriber(string mobile)
        {
            if (await _context.Subscribers.AnyAsync(subscriber => subscriber.Mobile == mobile))
                return;
            await _context.Subscribers.AddAsync(new Subscriber() { Mobile = mobile });
            await _context.SaveChangesAsync();
        }

        public async Task<List<Subscriber>> GetSubscribers()
        {
            return await _context.Subscribers.ToListAsync();
        }
    }
}
