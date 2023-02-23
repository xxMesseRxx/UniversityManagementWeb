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
        public IActionResult Course(string id)
        {
            List<Group> groups = db.Groups.Where(group => group.CourseId.ToString() == id).ToList();
            return Json(groups);
        }
    }
}
