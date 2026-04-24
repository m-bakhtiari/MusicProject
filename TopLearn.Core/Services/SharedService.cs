using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class SharedService : ISharedService
    {
        private readonly TopLearnContext _context;
        public SharedService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task<SearchVm> Search(string q, int pageId, int take = 10)
        {
            q = FixedText.FixEmail(q);
            var model = new List<SearchItemVM>();

            #region Course

            var course = await _context.Courses.Where(x => x.Tags.Contains(q) || x.CourseTitle.Contains(q) || x.ShortDescription.Contains(q) ||
                x.CourseDescription.Contains(q)).ToListAsync();
            if (course.Any() && course != null)
            {
                model.AddRange(course.Select(x => new SearchItemVM()
                {
                    Id = x.ProductId,
                    ImageName = $"/course/{x.CourseImageName}",
                    Title = x.CourseTitle,
                    Link = $"/StoreInfo?id={x.ProductId}",
                    IsNoteType = false
                }));
            }

            #endregion

            #region Course Group

            var group = await _context.CourseGroups.Where(x => x.GroupTitle.Contains(q)).ToListAsync();
            if (group.Any() && group != null)
            {
                var groupId = group.Select(x => x.GroupId).ToList();
                model.AddRange(await _context.Courses.Where(c => groupId.Contains(c.GroupId)).Select(x => new SearchItemVM()
                {
                    Id = x.GroupId,
                    ImageName = $"/course/{x.CourseImageName}",
                    Title = x.CourseTitle,
                    Link = $"/StoreInfo?id={x.ProductId}",
                    IsNoteType = false
                }).ToListAsync());
            }

            #endregion

            #region Student Concert

            var concert = await _context.StudentConcerts.Include(x => x.StudentConcertImages)
                .Where(x => x.Title.Contains(q) || x.Description.Contains(q)).ToListAsync();
            if (concert.Any() && concert != null)
            {
                model.AddRange(concert.Select(x => new SearchItemVM()
                {
                    Id = x.StudentConcertId,
                    ImageName = $"/studentConcert/{x.StudentConcertImages.FirstOrDefault().ImageName}",
                    Title = x.Title,
                    Link = $"/StudentConcertInfo?id={x.StudentConcertId}",
                    IsNoteType = false
                }));
            }

            #endregion

            #region Academy

            var academy = await _context.Academies.Where(x => x.AcademyTitle.Contains(q) || x.ActiveDays.Contains(q) || x.Address.Contains(q) ||
                x.LearningInstrument.Contains(q) || x.Address.Contains(q)).ToListAsync();
            if (academy.Any() && academy != null)
            {
                model.AddRange(academy.Select(x => new SearchItemVM()
                {
                    Id = x.AcademyId,
                    ImageName = $"/academy/{x.LogoImageName}",
                    Title = x.AcademyTitle,
                    Link = $"/Academy",
                    IsNoteType = false
                }));
            }

            #endregion

            #region Instrument

            var instrument = await _context.Instruments.Where(x => x.InstrumentTitle.Contains(q) || x.Description.Contains(q)).ToListAsync();
            if (instrument.Any() && instrument != null)
            {
                model.AddRange(instrument.Select(x => new SearchItemVM()
                {
                    Id = x.InstrumentId,
                    ImageName = $"/instrument/{x.ImageName}",
                    Title = x.InstrumentTitle,
                    Link = $"/InstrumentInfo?id={x.InstrumentId}",
                    IsNoteType = false
                }));
            }

            #endregion

            #region Note

            var note = await _context.MusicNotes.Where(x => x.Title.Contains(q)).ToListAsync();
            if (note.Any() && note != null)
            {
                model.AddRange(note.Select(x => new SearchItemVM()
                {
                    Id = x.MusicNoteId,
                    ImageName = (string.IsNullOrWhiteSpace(x.ImageName) ? "SiteTemplate/img/no-image.jpg" : $"/note/{x.ImageName}"),
                    Link = $"/Download/{x.MusicNoteId}",
                    Title = x.Title,
                    IsNoteType = true
                }));
            }

            #endregion

            var skip = (pageId - 1) * take;
            var itemCount = model.Count();
            double count = (double)itemCount / take;
            var pageCount = (int)Math.Ceiling(count);
            var query = model.Skip(skip).Take(take).ToList();
            if (pageCount > 0)
                query.FirstOrDefault().q = q;

            var res = Tuple.Create(query, pageCount);
            return new SearchVm() { Search = res };
        }

    }
}
