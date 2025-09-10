using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Context
{
    public class TopLearnContext : DbContext
    {

        public TopLearnContext(DbContextOptions<TopLearnContext> options) : base(options)
        {

        }


        #region Tables

        public DbSet<User> Users { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Product> Courses { get; set; }
        public DbSet<Academy> Academies { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<StudentConcert> StudentConcerts { get; set; }
        public DbSet<StudentConcertImage> StudentConcertImages { get; set; }
        public DbSet<MusicNote> MusicNotes { get; set; }
        public DbSet<Student> Students { get; set; }


        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(new User()
            {
                Email = "vahidnajafizadeh@gmail.com",
                Password = EncodePasswordMd5("123456"),
                RegisterDate = new DateTime(2024,02,02),
                UserId = 1,
                IsActive = true,
                IsDelete = false,
                UserName = "Vahid Najafizadeh"
            });


            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(g => !g.IsDelete);


            base.OnModelCreating(modelBuilder);
        }

        private string EncodePasswordMd5(string pass) //Encrypt using MD5   
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }

    }
}
