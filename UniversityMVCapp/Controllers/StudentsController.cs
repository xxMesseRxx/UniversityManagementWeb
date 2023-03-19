using Microsoft.AspNetCore.Mvc;
using UniversityMVCapp.Library.Interfaces;

namespace UniversityMVCapp.Controllers
{
	public class StudentsController : Controller
	{
		private IGroupService _groupService;
		private IStudentService _studentService;

		public StudentsController( IGroupService groupService, IStudentService studentService)
		{
			_groupService = groupService;
			_studentService = studentService;
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
