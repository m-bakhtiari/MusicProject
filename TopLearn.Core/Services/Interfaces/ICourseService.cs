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

        Task<List<CourseGroup>> GetAllGroup();
        Task<List<SelectListItem>> GetGroupForManageCourse();
        Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId);
        Task<CourseGroup> GetById(int groupId);
        Task AddGroup(CourseGroup group);
        Task UpdateGroup(CourseGroup group);
        Task DeleteGroup(CourseGroup group);

        #endregion

        #region Course

        Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdmin();

        Task<int> AddCourse(Product product, IFormFile imgCourse);
        Task<Product> GetCourseById(int courseId);
        Task UpdateCourse(Product product, IFormFile imgCourse);

        Task<Tuple<List<ShowCourseListItemViewModel>, int>> GetCourse(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null,int take=0);

        Task<Product> GetCourseForShow(int courseId);

        Task<List<ShowCourseListItemViewModel>> GetPopularCourse();

        Task DeleteCourse(int courseId);

        #endregion

    }
}
