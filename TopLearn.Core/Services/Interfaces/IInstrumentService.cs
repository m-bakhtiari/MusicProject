using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task<List<Instrument>> GetAll();
        Task<Instrument> GetById(int instrumentId);
        Task AddInstrument(Instrument instrument);
        Task UpdateInstrument(Instrument instrument);
        Task DeleteInstrument(Instrument instrument);
    }
}
