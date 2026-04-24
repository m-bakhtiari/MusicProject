using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TopLearn.DataLayer.Entities.Course
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Link { get; set; }

        [Required]
        public int Position { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public List<MenuItem> menuItems { get; set; }
    }
}
