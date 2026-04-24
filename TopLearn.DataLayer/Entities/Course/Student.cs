using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [MaxLength(200)]
        public string StudentFullName { get; set; }

        [MaxLength(200)]
        public string LearningInstrument { get; set; }

        public string Description { get; set; }

        [MaxLength(500)]
        public string ImageName { get; set; }

        public int? Position { get; set; }

        [MaxLength(5)]
        public string ShortKey { get; set; }

        [MaxLength(1500)]
        public string InstagramUrl { get; set; }

        [MaxLength(1500)]
        public string TelegramUrl { get; set; }

        [MaxLength(1500)]
        public string YoutubeUrl { get; set; }

        [MaxLength(1500)]
        public string VideoUrl { get; set; }

        [MaxLength(50)]
        public string HistoryYear { get; set; }

        [MaxLength(50)]
        public string HavanaYear { get; set; }

        public List<StudentImage> StudentImages { get; set; }
    }
}
