using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.Core.DTOs.Course;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.DTOs
{
   public class NoteViewModel
    {
        public List<MusicNote> MusicNotes { get; set; }
        public Tuple<List<NoteViewModelItem>, int> NoteViewModelItem { get; set; }
    }

   public class NoteViewModelItem
   {
       public int NoteId { get; set; }
       public string Title { get; set; }
       public string FileName { get; set; }
       public int InstrumentId { get; set; }
       public string InstrumentTitle { get; set; }
       public string InstrumentImageName { get; set; }
    }
}
