using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.DTOs.Course;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group

        List<CourseGroup> GetAllGroup();
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int groupId);
        CourseGroup GetById(int groupId);
        void AddGroup(CourseGroup group);
        void UpdateGroup(CourseGroup group);

        void DeleteGroup(CourseGroup group);
        #endregion

        #region Course

        List<ShowCourseForAdminViewModel> GetCoursesForAdmin();

        int AddCourse(Product product, IFormFile imgCourse, IFormFile courseDemo);
        Product GetCourseById(int courseId);
        void UpdateCourse(Product product, IFormFile imgCourse, IFormFile courseDemo);

        Tuple<List<ShowCourseListItemViewModel>,int> GetCourse(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null,int take=0);

        Product GetCourseForShow(int courseId);

        List<ShowCourseListItemViewModel> GetPopularCourse();

        #endregion

    }
}
