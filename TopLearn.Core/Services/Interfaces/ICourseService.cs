using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.DTOs.Course;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group

        Task<List<CourseGroup>> GetAllGroup(int? id);
        Task<List<SelectListItem>> GetGroupForManageCourse();
        Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId);
        Task<CourseGroup> GetById(int groupId);
        Task AddGroup(CourseGroup group, IFormFile imgName);
        Task UpdateGroup(CourseGroup group, IFormFile imgName);
        Task DeleteGroup(CourseGroup group);
        Task<List<CourseGroup>> GetAllGroupWithSub();
        #endregion

        #region Course

        Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdmin();

        Task<int> AddCourse(Product product, IFormFile imgCourse, List<IFormFile> imagesFiles);
        Task<Product> GetCourseById(int courseId);
        Task UpdateCourse(Product product, IFormFile imgCourse, List<IFormFile> imagesFiles);

        Task<Tuple<List<Product>, int>> GetCourse(int pageId = 1, string filter = "", int groupId = 0, int take = 0);

        Task<Product> GetCourseForShow(int courseId);

        Task<List<ShowCourseListItemViewModel>> GetPopularCourse();

        Task DeleteCourse(int courseId);

        Task<List<Product>> GetProductsBySubGroup(int subId);

        Task DeleteImage(int id);

        Task<ProductImage> GetImageById(int imageId);

        #endregion

    }
}
