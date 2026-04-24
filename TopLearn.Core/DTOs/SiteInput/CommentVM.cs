using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.DTOs.SiteInput
{
    public class CommentVM
    {
        public List<Comment> Comments { get; set; }
        public Comment CommentInput { get; set; }
    }

    public class UserComment
    {
        public string comment { get; set; }
        public string author { get; set; }
        public string mobile { get; set; }
        public int type { get; set; }
        public int relationId { get; set; }
    }
}
