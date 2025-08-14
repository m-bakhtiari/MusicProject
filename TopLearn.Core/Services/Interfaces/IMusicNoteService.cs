using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IMusicNoteService
    {
        Task<int> AddNote(MusicNote musicNote, IFormFile noteFile);
        Task UpdateNote(MusicNote musicNote, IFormFile noteFile);
        Task DeleteNote(int noteId);
        Task<Tuple<List<MusicNote>, int>> GetCourse(int pageId = 1, string filter = "", int take = 0);
        Task<List<MusicNote>> GetAllNotes();
        Task<MusicNote> GetNoteById(int noteId);
    }
}
