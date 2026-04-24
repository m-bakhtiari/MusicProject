using System.ComponentModel.DataAnnotations;

namespace TopLearn.DataLayer.Entities.Course
{
    public class GalleryImage
    {
        [Key]
        public int GalleryImageId { get; set; }

        [MaxLength(100)]
        public string ImageName { get; set; }
    }
}
