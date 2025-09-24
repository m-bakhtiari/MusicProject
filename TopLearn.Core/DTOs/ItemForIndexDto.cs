using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.DTOs
{
    public class ItemForIndexDto
    {
        public List<Instrument> Instruments { get; set; }
        public List<Academy> Academies { get; set; }
    }
}
