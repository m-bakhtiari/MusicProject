using System;
using System.Collections.Generic;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.DTOs
{
    public class StoreVM
    {
        public Tuple<List<Product>, int> Product { get; set; }
        public List<CourseGroup> Group { get; set; }
    }
}
