using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly TopLearnContext _context;
        public CertificateService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task AddCertificate(string firstName, string lastName, string mobile, string nationalCode, string academy, string instrument, string description, string address, string postalCode)
        {
            var model = new Certificate()
            {
                FirstName = firstName,
                LastName = lastName,
                Mobile = mobile,
                NationalCode = nationalCode,
                Academy = academy,
                Instrument = instrument,
                Description = description,
                CreatedDate = DateTime.Now,
                IsDone = false,
                IsPay = false,
                FileName = null,
                Address = address,
                PostalCode = postalCode,
                TrackingCode = null,
                SendDate = null
            };
            await _context.AddAsync<Certificate>(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Certificate>> GetCertificates()
        {
            return await _context.Certificates.ToListAsync();
        }

        public async Task<List<Certificate>> GetCertificatesByMobile(string mobile)
        {
            return await _context.Certificates.Where(x => x.Mobile == mobile && x.IsDone).ToListAsync();
        }

        public async Task<Certificate> GetCertificateById(int id)
        {
            return await _context.Certificates.FindAsync(id);
        }

        public async Task PayedCertificateById(int id)
        {
            var model = await _context.Certificates.FindAsync(id);
            model.IsPay = !model.IsPay;
            await _context.SaveChangesAsync();
        }

        public async Task UploadCertificate(int id, IFormFile imgLogo)
        {
            var model = await _context.Certificates.FindAsync(id);
            model.FileName = "no-photo.jpg";
            if (imgLogo != null)
            {
                model.FileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgLogo.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/certificate", model.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgLogo.CopyToAsync(stream);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
