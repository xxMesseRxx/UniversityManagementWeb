using Microsoft.AspNetCore.Mvc;
using UniversityMVCapp.ViewModels;
using UniversityMVCapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace UniversityMVCapp.Controllers
{
    public class HomeController : Controller
    {
        UniversityContext db;

        public HomeController(UniversityContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(db.Courses.ToList());
        }
        [HttpGet]
        public IActionResult Groups(string courseId)
        {
            List<Group> groups = db.Groups.Where(group => group.CourseId.ToString() == courseId).ToList();
            return Json(groups);
        }
        [HttpGet]
        public IActionResult Students(string groupId)
        {
            List<Student> students = db.Students.Where(student => student.GroupId.ToString() == groupId).ToList();
            return Json(students);
        }
		[HttpGet]
		public IActionResult EditGroups()
        {
            ViewBag.Courses = db.Courses.ToList();
            return View(db.Groups.Include(g => g.Course).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> EditGroups(int? groupId, string name, int courseId)
        {
            if (groupId is null)
            {
                Group newGroup = new Group() { Name = name, CourseId = courseId };
                db.Groups.Add(newGroup);
                await db.SaveChangesAsync();
            }
            else
            {
				Group group = await db.Groups.FirstOrDefaultAsync(g => g.GroupId == groupId.Value);
                group.Name = name;
                group.CourseId = courseId;
				db.Groups.Update(group);
				await db.SaveChangesAsync();
			}

			return RedirectToAction("EditGroups");
		}
        [HttpDelete]
        public async Task<JsonResult> EditGroups(string groupId)
        {
            try
            {
				Group group = await db.Groups.FirstOrDefaultAsync(g => g.GroupId.ToString() == groupId);
				db.Groups.Remove(group);
				await db.SaveChangesAsync();
				return Json(group);
			}
            catch (DbUpdateException)
            {
                Response.StatusCode = 405;
                return Json(new { message = "Группа не может быть удалена пока в ней есть студенты" });
			}
		}
		[HttpGet]
		public IActionResult EditStudents()
		{
            ViewBag.Groups = db.Groups.ToList();
			return View(db.Students.Include(s => s.Group).ToList());
		}
		[HttpPost]
		public async Task<IActionResult> EditStudents(int? studentId, string firstName, string lastName, int groupId)
		{
			if (studentId is null)
			{
				Student newStudent = new Student() { FirstName = firstName, LastName = lastName, GroupId = groupId };
				db.Students.Add(newStudent);
				await db.SaveChangesAsync();
			}
            else
            {
                Student student = await db.Students.FirstOrDefaultAsync(g => g.StudentId == studentId.Value);
                student.FirstName = firstName;
                student.LastName = lastName;
                student.GroupId = groupId;
                db.Students.Update(student);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("EditStudents");
		}
		[HttpDelete]
		public async Task<JsonResult> EditStudents(string studentId)
		{
			Student student = await db.Students.FirstOrDefaultAsync(s => s.StudentId.ToString() == studentId);
			db.Students.Remove(student);
			await db.SaveChangesAsync();
			return Json(student);
		}
	}
}
