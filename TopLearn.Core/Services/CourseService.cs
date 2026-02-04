using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs.Course;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly TopLearnContext _context;

        public CourseService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task<List<CourseGroup>> GetAllGroup(int? id)
        {
            if (id.HasValue)
                return await _context.CourseGroups.Where(x => x.ParentId == id).ToListAsync();
            return await _context.CourseGroups.Where(x => x.ParentId == null).ToListAsync();
        }

        public async Task<List<CourseGroup>> GetAllGroupWithSub()
        {
            return await _context.CourseGroups.Include(x => x.CourseGroups).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetGroupForManageCourse()
        {
            return await _context.CourseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId)
        {
            return await _context.CourseGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToListAsync();
        }



        public async Task<CourseGroup> GetById(int groupId)
        {
            return await _context.CourseGroups.FindAsync(groupId);
        }

        public async Task AddGroup(CourseGroup group, IFormFile imgName)
        {
            if (imgName != null && imgName.IsImage())
            {
                group.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgName.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/group", group.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgName.CopyToAsync(stream);
                }
            }
            await _context.CourseGroups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroup(CourseGroup group, IFormFile imgName)
        {
            if (imgName != null && imgName.IsImage())
            {
                if (group.ImageName != "no-photo.jpg")
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/group", group.ImageName);
                    if (File.Exists(deleteImagePath))
                    {
                        File.Delete(deleteImagePath);
                    }
                }
                group.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgName.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/group", group.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgName.CopyToAsync(stream);
                }
            }
            _context.CourseGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdmin()
        {
            return await _context.Courses.Select(c => new ShowCourseForAdminViewModel()
            {
                CourseId = c.ProductId,
                ImageName = c.CourseImageName,
                Title = c.CourseTitle,
                Price = c.CoursePrice
            }).ToListAsync();
        }

        public async Task<int> AddCourse(Product product, IFormFile imgCourse, List<IFormFile> imagesFiles)
        {
            product.CreateDate = DateTime.Now;
            product.CourseImageName = "no-photo.jpg";

            if (imgCourse != null && imgCourse.IsImage())
            {
                product.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course", product.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgCourse.CopyToAsync(stream);
                }
            }
            var insert = await _context.AddAsync(product);
            if (imagesFiles != null)
            {
                foreach (var file in imagesFiles)
                {
                    var photo = new ProductImage()
                    {
                        ProductId = insert.Entity.ProductId,
                        ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName)
                    };
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course", photo.ProductImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _context.ProductImages.AddAsync(photo);
                }
            }
            await _context.SaveChangesAsync();

            return product.ProductId;
        }

        public async Task<Product> GetCourseById(int courseId)
        {
            return await _context.Courses.Include(x => x.ProductImages).Include(x => x.CourseGroup)
                .Include(x => x.Group).FirstOrDefaultAsync(x => x.ProductId == courseId);
        }

        public async Task<List<Product>> GetProductsBySubGroup(int subId)
        {
            var group = await _context.CourseGroups.FindAsync(subId);
            if (group.ParentId == null)
                return await _context.Courses.Where(x => x.GroupId == subId).ToListAsync();
            return await _context.Courses.Where(x => x.SubGroup == subId).ToListAsync();
        }

        public async Task UpdateCourse(Product product, IFormFile imgCourse, List<IFormFile> imagesFiles)
        {
            product.UpdateDate = DateTime.Now;

            if (imagesFiles != null)
            {
                foreach (var file in imagesFiles)
                {
                    var photo = new ProductImage()
                    {
                        ProductId = product.ProductId,
                        ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName)
                    };
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course", photo.ProductImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _context.ProductImages.AddAsync(photo);
                }
            }
            if (imgCourse != null && imgCourse.IsImage())
            {
                if (product.CourseImageName != "no-photo.jpg")
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course", product.CourseImageName);
                    if (File.Exists(deleteImagePath))
                    {
                        File.Delete(deleteImagePath);
                    }
                }
                product.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course", product.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgCourse.CopyToAsync(stream);
                }
            }
            _context.Courses.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<List<ShowCourseListItemViewModel>, int>> GetCourse(int pageId = 1, string filter = "", int groupId = 0, int take = 0)
        {
            if (take == 0)
                take = 8;

            IQueryable<Product> result = _context.Courses;
            var group = await _context.CourseGroups.ToListAsync();
            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }
            if (groupId != 0)
            {
                result = result.Where(c => c.GroupId == groupId || c.SubGroup == groupId);
            }
            var skip = (pageId - 1) * take;
            var pageCount = await result.Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.ProductId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                GroupTitle = group.FirstOrDefault(x => x.GroupId == c.GroupId).GroupTitle,
            }).CountAsync() / take;

            var query = await result.Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.ProductId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                GroupTitle = group.FirstOrDefault(x => x.GroupId == c.GroupId).GroupTitle,
            }).Skip(skip).Take(take).ToListAsync();

            return Tuple.Create(query, pageCount);
        }

        public async Task<Product> GetCourseForShow(int courseId)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.ProductId == courseId);
        }

        public async Task<List<ShowCourseListItemViewModel>> GetPopularCourse()
        {
            return await _context.Courses
                .Take(8)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.ProductId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    Title = c.CourseTitle,
                })
                .ToListAsync();
        }

        public async Task DeleteGroup(CourseGroup group)
        {
            var model = await _context.CourseGroups.Where(x => x.ParentId == group.GroupId).ToListAsync();
            if (model != null)
            {
                foreach (var item in model)
                {
                    _context.CourseGroups.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            if (group.ImageName != "no-photo.jpg")
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/group", group.ImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.CourseGroups.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(int courseId)
        {
            var product = await _context.Courses.FindAsync(courseId);
            if (product.CourseImageName != "no-photo.jpg")
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course", product.CourseImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            var image = await _context.ProductImages.Where(x => x.ProductId == courseId).ToListAsync();
            foreach (var item in image)
            {
                var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course", item.ProductImageName);
                if (File.Exists(deleteImagePath))
                {
                    File.Delete(deleteImagePath);
                }
            }
            _context.ProductImages.RemoveRange(image);
            _context.Courses.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(int id)
        {
            var image = await _context.ProductImages.FindAsync(id);
            var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course", image.ProductImageName);
            if (File.Exists(deleteImagePath))
            {
                File.Delete(deleteImagePath);
            }
            _context.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductImage> GetImageById(int imageId)
        {
            return await _context.ProductImages.FindAsync(imageId);
        }
    }
}
