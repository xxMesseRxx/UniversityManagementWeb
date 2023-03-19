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
			try
			{
				return Json(await _groupService.GetGroupsWithCourseIdAsync(courseId));
			}
			catch (ArgumentException) { }

			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> Students(int groupId)
		{
			try
			{
				return Json(await _studentService.GetStudentsWithGroupIdAsync(groupId));
			}
			catch (ArgumentException) { }

			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> EditGroups()
		{
			ViewBag.Courses = await _courseService.GetAllAsync();
			return View("EditGroups", await _groupService.GetAllAsync());
		}
		[HttpPost]
		public async Task<IActionResult> EditGroups(int? groupId, string name, int courseId)
		{
			try
			{
				if (groupId is null)
				{
					await _groupService.AddAsync(courseId, name);
				}
				else
				{
					await _groupService.EditAsync((int)groupId, courseId, name);
				}
			}
			catch (ArgumentException) { }

			return RedirectToAction("EditGroups");
		}
		[HttpDelete]
		public async Task<JsonResult> EditGroups(int groupId)
		{
			try
			{
				await _groupService.DelGroupAsync(groupId);
				return Json(new { groupId });
			}
			catch (InvalidOperationException)
			{
				Response.StatusCode = 405;
				return Json(new { message = "Группа не может быть удалена пока в ней есть студенты" });
			}
			catch (ArgumentException)
			{
				Response.StatusCode = 405;
				return Json(new { message = "Группа с данным Id не найдена" });
			}
		}
		[HttpGet]
		public async Task<IActionResult> EditStudents()
		{
			ViewBag.Groups = await _groupService.GetAllAsync();
			return View("EditStudents", await _studentService.GetAllAsync());
		}
		[HttpPost]
		public async Task<IActionResult> EditStudents(int? studentId, string firstName, string lastName, int groupId)
		{
			try
			{
				if (studentId is null)
				{
					await _studentService.AddAsync(groupId, firstName, lastName);
				}
				else
				{
					await _studentService.EditAsync((int)studentId, groupId, firstName, lastName);
				}
			}
			catch (ArgumentException) { }

			return RedirectToAction("EditStudents");
		}
		[HttpDelete]
		public async Task<JsonResult> EditStudents(int studentId)
		{
			try
			{
				await _studentService.DelStudentAsync(studentId);
				return Json(new { studentId });
			}
			catch (ArgumentException) { }

			Response.StatusCode = 405;
			return Json(new { message = "Студент с данным Id не найден" });
		}
	}
}
