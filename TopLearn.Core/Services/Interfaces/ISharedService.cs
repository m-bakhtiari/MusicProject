using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ISharedService
    {
        Task<SearchVm> Search(string q, int pageId, int take = 0);

    }
}
