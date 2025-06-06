using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task AddInstrument(Instrument instrument)
        {
            instrument.CreatedDate = DateTime.Now.Date;
            await _context.Instruments.AddAsync(instrument);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInstrument(Instrument instrument)
        {
            _context.Instruments.Remove(instrument);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Instrument>> GetAll()
        {
            return await _context.Instruments.ToListAsync();
        }

        public async Task<Instrument> GetById(int instrumentId)
        {
            return await _context.Instruments.FirstOrDefaultAsync(x => x.InstrumentId == instrumentId);
        }

        public async Task UpdateInstrument(Instrument instrument)
        {
            _context.Instruments.Update(instrument);
            await _context.SaveChangesAsync();
        }
    }
}
