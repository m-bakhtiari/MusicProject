using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class MusicNote
    {
        [Key]
        public int MusicNoteId { get; set; }

        [MaxLength(3500)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string FileName { get; set; }
    }
}
