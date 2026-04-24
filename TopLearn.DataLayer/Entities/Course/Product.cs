using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubGroup { get; set; }

        [Required]
        [MaxLength(450)]
        public string CourseTitle { get; set; }

        [Required]
        public string CourseDescription { get; set; }

        [Required]
        public int CoursePrice { get; set; }

        public int? SalePrice { get; set; }

        [MaxLength(600)]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string CourseImageName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsAvailable { get; set; }

        public int? Quantity { get; set; }

        [MaxLength(1500)]
        public string ShortDescription { get; set; }

        #region Relations

        [ForeignKey("GroupId")]
        public CourseGroup CourseGroup { get; set; }

        [ForeignKey("SubGroup")]
        public CourseGroup Group { get; set; }

        public List<ProductImage> ProductImages { get; set; }
        #endregion
    }
}
