using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
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
        public async Task AddInstrument(Instrument instrument, IFormFile imgInstrumentUp, IFormFile imgInstrumentLogoUp)
        {
            instrument.IconImage = "no-photo.jpg";
            instrument.ImageName = "no-photo.jpg";
            if (imgInstrumentLogoUp != null && imgInstrumentLogoUp.IsImage())
            {
                instrument.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgInstrumentLogoUp.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/instrument", instrument.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgInstrumentLogoUp.CopyToAsync(stream);
                }
            }
            if (imgInstrumentUp != null && imgInstrumentUp.IsImage())
            {
                instrument.IconImage = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgInstrumentUp.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/instrument", instrument.IconImage);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgInstrumentUp.CopyToAsync(stream);
                }
            }

            instrument.CreatedDate = DateTime.Now.Date;
            await _context.Instruments.AddAsync(instrument);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInstrument(Instrument instrument)
        {
            if (instrument.IconImage != "no-photo.jpg")
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/instrument", instrument.IconImage);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            if (instrument.ImageName != "no-photo.jpg")
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/instrument", instrument.ImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.Instruments.Remove(instrument);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Instrument>> GetAll()
        {
            return await _context.Instruments.ToListAsync();
        }

        public async Task<List<Instrument>> GetInstrumentHasNote()
        {
            return await _context.Instruments.Where(x => x.MusicNotes.Any()).ToListAsync();
        }

        public async Task<Instrument> GetById(int instrumentId)
        {
            return await _context.Instruments.FirstOrDefaultAsync(x => x.InstrumentId == instrumentId);
        }

        public async Task UpdateInstrument(Instrument instrument, IFormFile imgInstrumentUp, IFormFile imgInstrumentLogoUp)
        {
            if (imgInstrumentUp != null && imgInstrumentUp.IsImage())
            {
                if (instrument.IconImage != "no-photo.jpg")
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", instrument.IconImage);
                    if (File.Exists(deleteImagePath))
                    {
                        File.Delete(deleteImagePath);
                    }
                }
                instrument.IconImage = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgInstrumentUp.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", instrument.IconImage);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgInstrumentUp.CopyToAsync(stream);
                }
            }
            if (imgInstrumentLogoUp != null && imgInstrumentLogoUp.IsImage())
            {
                if (instrument.ImageName != "no-photo.jpg")
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/instrument", instrument.ImageName);
                    if (File.Exists(deleteImagePath))
                    {
                        File.Delete(deleteImagePath);
                    }
                }
                instrument.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgInstrumentLogoUp.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/instrument", instrument.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgInstrumentLogoUp.CopyToAsync(stream);
                }
            }

            _context.Instruments.Update(instrument);
            await _context.SaveChangesAsync();
        }
    }
}
