using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;

namespace TopLearn.Core.Services
{
    public class ConcertPrizeService : IConcertPrizeService
    {
        private readonly TopLearnContext _context;
        public ConcertPrizeService(TopLearnContext context)
        {
            _context = context;
        }
        public async Task<string> AddItem(string Ip)
        {
            var data = await _context.ConcertPrizes.FirstOrDefaultAsync(x => x.UserIp == Ip);
            if (data == null)
            {
                var prize = await _context.ConcertPrizeTypes.ToListAsync();
                var random = prize.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                var result = await _context.ConcertPrizes.AddAsync(new DataLayer.Entities.Course.ConcertPrize()
                {
                    PrizeTypeId = random.Id,
                    UserIp = Ip,
                    CreatedDate = DateTime.Now
                });
                await _context.SaveChangesAsync();
                var res = result.Entity.PrizeTypeId;
                var reward = await _context.ConcertPrizeTypes.FindAsync(res);
                return reward.Name;
            }
            else
            {
                var result = await _context.ConcertPrizes.FirstOrDefaultAsync(x => x.UserIp == Ip);
                var res = result.PrizeTypeId;
                var reward = await _context.ConcertPrizeTypes.FindAsync(res);
                return reward.Name;
            }
        }
    }
}
