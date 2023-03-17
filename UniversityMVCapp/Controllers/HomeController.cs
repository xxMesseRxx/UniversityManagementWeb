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

		public HomeController(ICourseService courseService, IGroupService groupService)
        {
			_courseService = courseService;
			_groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await _courseService.GetAllAsync());

        }
		[HttpGet]
		public async Task<IActionResult> Groups(string courseId)
		{
			if (int.TryParse(courseId, out int id))
			{
				try
				{
					return Json(await _groupService.GetGroupsWithCourseIdAsync(id));
				}
				catch (ArgumentException) { }
			}

			return RedirectToAction("Index");
		}
		//      [HttpGet]
		//      public IActionResult Students(string groupId)
		//      {
		//          List<Student> students = db.Students.Where(student => student.GroupId.ToString() == groupId).ToList();
		//          return Json(students);
		//      }
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
		//[HttpGet]
		//public IActionResult EditStudents()
		//{
		//          ViewBag.Groups = db.Groups.ToList();
		//	return View("EditStudents", db.Students.Include(s => s.Group).ToList());
		//}
		//[HttpPost]
		//public async Task<IActionResult> EditStudents(int? studentId, string firstName, string lastName, int groupId)
		//{
		//          try
		//          {
		//		if (studentId is null)
		//		{
		//			Student newStudent = new Student() { FirstName = firstName, LastName = lastName, GroupId = groupId };
		//			db.Students.Add(newStudent);
		//			await db.SaveChangesAsync();
		//		}
		//		else
		//		{
		//			Student student = await db.Students.FirstOrDefaultAsync(g => g.StudentId == studentId.Value);
		//			if (student != null)
		//			{
		//				student.FirstName = firstName;
		//				student.LastName = lastName;
		//				student.GroupId = groupId;
		//				db.Students.Update(student);
		//				await db.SaveChangesAsync();
		//			}
		//		}
		//	}
		//	catch (DbUpdateException) { }

		//          return RedirectToAction("EditStudents");
		//}
		//[HttpDelete]
		//public async Task<JsonResult> EditStudents(string studentId)
		//{
		//	Student student = await db.Students.FirstOrDefaultAsync(s => s.StudentId.ToString() == studentId);
		//          if (student != null)
		//          {
		//		db.Students.Remove(student);
		//		await db.SaveChangesAsync();
		//		return Json(student);
		//	}
		//	Response.StatusCode = 405;
		//	return Json(new { message = "Студент с данным Id не найден" });
		//}
	}
}
