using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Core.DTOs
{
    public static class ConstantValue
    {
        public enum Type
        {
            StudentConcert = 1,
            Workshop = 2,
            Havana = 3,
            Book = 4,
            VahidConcert = 5,
            Media = 6
        }

        public enum StudentType
        {
            Mentor = 1,
            TopStudent = 2
        }

    }
}
