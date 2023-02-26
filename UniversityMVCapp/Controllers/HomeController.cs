using Microsoft.AspNetCore.Mvc;
using UniversityMVCapp.ViewModels;
using UniversityMVCapp.Models;

namespace UniversityMVCapp.Controllers
{
    public class HomeController : Controller
    {
        UniversityContext db;

        public HomeController(UniversityContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Courses.ToList());
        }
        public IActionResult Groups(string courseId)
        {
            List<Group> groups = db.Groups.Where(group => group.CourseId.ToString() == courseId).ToList();
            return Json(groups);
        }
        public IActionResult Students(string groupId)
        {
            List<Student> students = db.Students.Where(student => student.GroupId.ToString() == groupId).ToList();
            return Json(students);
        }
    }
}
