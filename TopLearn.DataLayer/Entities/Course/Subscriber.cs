using System.ComponentModel.DataAnnotations;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Subscriber
    {
        [Key]
        public int SubscriberId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Mobile { get; set; }
    }
}
