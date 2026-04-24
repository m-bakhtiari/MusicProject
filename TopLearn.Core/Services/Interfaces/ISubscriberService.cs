using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ISubscriberService
    {
        Task AddSubscriber(string mobile);
        Task<List<Subscriber>> GetSubscribers();
    }
}
