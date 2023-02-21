using Microsoft.AspNetCore.Mvc;
using UniversityMVCapp.ViewModels;

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
    }
}
