using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IConcertPrizeService
    {
        Task<string> AddItem(string Ip);
    }
}
