﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IUserService _userService;

        public CourseController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.Groups = _courseService.GetAllGroup();
            ViewBag.pageId = pageId;
            return View(_courseService.GetCourse(pageId,filter,getType,orderByType,startPrice,endPrice,selectedGroups,9));
        }


        [Route("ShowCourse/{id}")]
        public IActionResult ShowCourse(int id)
        {
            var course = _courseService.GetCourseForShow(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


    }
}