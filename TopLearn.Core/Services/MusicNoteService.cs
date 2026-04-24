using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.Core.Security;

namespace TopLearn.Core.Services
{
    public class MusicNoteService : IMusicNoteService
    {
        private readonly TopLearnContext _context;

        public MusicNoteService(TopLearnContext context)
        {
            _context = context;
        }
        public async Task<int> AddNote(MusicNote musicNote, IFormFile noteFile,IFormFile imageFile)
        {
            if (noteFile != null)
            {
                musicNote.FileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(noteFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", musicNote.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await noteFile.CopyToAsync(stream);
                }
            }
            if (imageFile != null && imageFile.IsImage())
            {
                musicNote.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imageFile.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", musicNote.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            await _context.MusicNotes.AddAsync(musicNote);
            await _context.SaveChangesAsync();

            return musicNote.MusicNoteId;
        }

        public async Task DeleteNote(int noteId)
        {
            var note = await _context.MusicNotes.FindAsync(noteId);
            var deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", note.FileName);
            if (File.Exists(deleteFilePath))
            {
                File.Delete(deleteFilePath);
            }
            var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", note.ImageName);
            if (File.Exists(deleteImagePath))
            {
                File.Delete(deleteImagePath);
            }
            _context.MusicNotes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MusicNote>> GetAllNotes()
        {
            return await _context.MusicNotes.ToListAsync();
        }

        public async Task<MusicNote> GetNoteById(int noteId)
        {
            return await _context.MusicNotes.FindAsync(noteId);
        }

        public async Task UpdateNote(MusicNote musicNote, IFormFile noteFile,IFormFile imageFile)
        {
            if (noteFile != null)
            {
                var deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", musicNote.FileName);
                if (File.Exists(deleteFilePath))
                {
                    File.Delete(deleteFilePath);
                }

                musicNote.FileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(noteFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", musicNote.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await noteFile.CopyToAsync(stream);
                }
            }
            if (imageFile != null && imageFile.IsImage())
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", musicNote.ImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }

                musicNote.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imageFile.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/note", musicNote.ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            _context.MusicNotes.Update(musicNote);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<List<NoteViewModelItem>, int>> GetMusicNote(int pageId = 1, string filter = "",
            int instrumentId = 0, int take = 0)
        {
            if (take == 0)
                take = 12;

            IQueryable<MusicNote> result = _context.MusicNotes.Include(x => x.Instrument);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.Title.Contains(filter));
            }
            result = result.OrderByDescending(c => c.MusicNoteId);

            if (instrumentId != 0)
            {
                result = result.Where(c => c.InstrumentId == instrumentId);
            }

            var skip = (pageId - 1) * take;

            var itemCount = await result.Select(c => new NoteViewModelItem()
            {
                NoteId = c.MusicNoteId,
                FileName = c.FileName,
                Title = c.Title
            }).CountAsync();

            double count = (double)itemCount / take;
            var pageCount = (int)Math.Ceiling(count);
            var query = await result.Select(c => new NoteViewModelItem()
            {
                NoteId = c.MusicNoteId,
                FileName = c.FileName,
                Title = c.Title,
                InstrumentId = c.InstrumentId.Value,
                InstrumentTitle = c.Instrument.InstrumentTitle,
                NoteImageName = c.ImageName
            }).Skip(skip).Take(take).ToListAsync();
            return Tuple.Create(query, pageCount);
        }
    }
}
