using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<List<Certificate>> GetCertificates();
        Task AddCertificate(string firstName, string lastName, string mobile, string nationalCode,
            string academy, string instrument, string description,string address,string postalCode);
        Task<List<Certificate>> GetCertificatesByMobile(string mobile);
        Task<Certificate> GetCertificateById(int id);
        Task PayedCertificateById(int id);
        Task UploadCertificate(int id, IFormFile imgLogo);
    }
}
