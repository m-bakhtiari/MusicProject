using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            RadioJavan = 6,
            JaamJam = 7
        }

        public enum StudentType
        {
            Mentor = 1,
            TopStudent = 2
        }

        public enum CommentType
        {
            StudentConcert = 1,
            Instrument = 2,
            Product = 3
        }

        public enum VideoType
        {
            Learning = 1,
            Student = 2,
            Concert = 3,
        }

        public const string TELEGRAM = "https://t.me/vahidnajafizadeh";
        public const string YOUTUBE = "https://youtube.com/vahidnajafizadeh";
        public const string APARAT = "https://www.aparat.com/Vahid.najafizadeh";
        public const string INSTA_VAHID = "https://instagram.com/vahidnajafizadeh";
        public const string INSTA_STUDENT = "https://instagram.com/vahidnajafizadeh_student";
        public const string INSTA_HAVANA = "https://instagram.com/havanaCompanyOfficial";
        public const string INSTA_VPAN = "https://instagram.com/vpan.instruments";
        public const string WHATSAPP = "https://wa.me/989354868864";
        public const string PHONE = "tel:+989354868864";
        public const string EMAIL = "mailto:info@vahidnajafizadeh.com";
    }
}
