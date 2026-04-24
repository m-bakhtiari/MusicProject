using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
   public class Instrument
    {
        [Key]
        public int InstrumentId { get; set; }

        [MaxLength(450)]
        public string InstrumentTitle { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(500)]
        public string IconImage { get; set; }

        [MaxLength(500)]
        public string ImageName { get; set; }

        [MaxLength(3500)]
        public string VideoUrl { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<MusicNote> MusicNotes { get; set; }
    }
}
