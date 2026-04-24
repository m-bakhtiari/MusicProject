using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.SiteInput;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IContactMessageService
    {
        Task AddMessage(ContactInput input);
        Task<List<ContactMessage>> GetContactMessages();
        Task<ContactMessage> GetItemById(int id);
        Task ToggleSeenStatus(int id);
    }
}
