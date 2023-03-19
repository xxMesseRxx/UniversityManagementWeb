using Microsoft.AspNetCore.Mvc;
using UniversityMVCapp.Library.Interfaces;

namespace UniversityMVCapp.Controllers
{
	public class GroupsController : Controller
	{
		private ICourseService _courseService;
		private IGroupService _groupService;

		public GroupsController(ICourseService courseService, IGroupService groupService)
		{
			_courseService = courseService;
			_groupService = groupService;
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
	}
}
