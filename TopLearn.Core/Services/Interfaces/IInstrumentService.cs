using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IInstrumentService
    {
        List<Instrument> GetAll();
        Instrument GetById(int instrumentId);
        void AddInstrument(Instrument instrument);
        void UpdateInstrument(Instrument instrument);
        void DeleteInstrument(Instrument instrument);
    }
}
