using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly TopLearnContext _context;

        public InstrumentService(TopLearnContext context)
        {
            _context = context;
        }
        public void AddInstrument(Instrument instrument)
        {
            instrument.CreatedDate = DateTime.Now.Date;
            _context.Instruments.Add(instrument);
            _context.SaveChanges();
        }

        public void DeleteInstrument(Instrument instrument)
        {
            _context.Instruments.Remove(instrument);
            _context.SaveChanges();
        }

        public List<Instrument> GetAll()
        {
            return _context.Instruments.ToList();
        }

        public Instrument GetById(int instrumentId)
        {
            return _context.Instruments.FirstOrDefault(x => x.InstrumentId == instrumentId);
        }

        public void UpdateInstrument(Instrument instrument)
        {
            _context.Instruments.Update(instrument);
            _context.SaveChanges();
        }
    }
}
