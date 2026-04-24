using System.ComponentModel.DataAnnotations;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }

        public int VideoType { get; set; }

        [MaxLength(100)]
        public string ThumbnailImage { get; set; }

        [MaxLength(200)]
        public string VideoUrl { get; set; }

        [MaxLength(350)]
        public string ThumbnailImageUrl { get; set; }

        public int Position { get; set; }

        [MaxLength(300)]
        public string Title { get; set; }
    }
}
