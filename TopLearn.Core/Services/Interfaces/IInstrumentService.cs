using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task<List<Instrument>> GetAll();
        Task<Instrument> GetById(int instrumentId);
        Task AddInstrument(Instrument instrument, IFormFile imgInstrumentUp, IFormFile imgInstrumentLogoUp);
        Task UpdateInstrument(Instrument instrument, IFormFile imgInstrumentUp, IFormFile imgInstrumentLogoUp);
        Task DeleteInstrument(Instrument instrument);
    }
}
