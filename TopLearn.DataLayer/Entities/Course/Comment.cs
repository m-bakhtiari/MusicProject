using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [MaxLength(3500)]
        public string CommentText { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; }

        [MaxLength(150)]
        public string FullName { get; set; }

        public int? ParentId { get; set; }
        public int? InstrumentId { get; set; }
        public int? StudentConcertId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public bool IsShowOnSite { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CommentType { get; set; }

        #region Relations

        [ForeignKey(nameof(ParentId))]
        public virtual List<Comment> Comments { get; set; }

        [ForeignKey(nameof(InstrumentId))]
        public virtual Instrument Instrument { get; set; }

        [ForeignKey(nameof(StudentConcertId))]
        public virtual StudentConcert StudentConcert { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User.User User { get; set; }

        #endregion
    }
}
