using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    internal class CertificateService : ICertificateService
    {
        private readonly TopLearnContext _context;
        public CertificateService(TopLearnContext context)
        {
            _context = context;
        }
        public async Task<List<Certificate>> GetCertificates()
        {
            return await _context.Certificates.ToListAsync();
        }
    }
}
