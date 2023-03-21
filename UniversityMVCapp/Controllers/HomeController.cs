using Microsoft.AspNetCore.Mvc;
using UniversityMVCapp.ViewModels;
using UniversityMVCapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using UniversityMVCapp.Library.Interfaces;

namespace UniversityMVCapp.Controllers
{
    public class HomeController : Controller
    {
        private ICourseService _courseService;
		private IGroupService _groupService;
		private IStudentService _studentService;

		public HomeController(ICourseService courseService, IGroupService groupService, IStudentService studentService)
        {
			_courseService = courseService;
			_groupService = groupService;
			_studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await _courseService.GetAllAsync());
        }
		[HttpGet]
		public async Task<IActionResult> Groups(int courseId)
		{
			return Json(await _groupService.GetGroupsWithCourseIdAsync(courseId));
		}
		[HttpGet]
		public async Task<IActionResult> Students(int groupId)
		{
			return Json(await _studentService.GetStudentsWithGroupIdAsync(groupId));
		}
	}
}
