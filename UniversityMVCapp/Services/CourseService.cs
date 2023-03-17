using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.Library.Interfaces;
using UniversityMVCapp.Models;
using UniversityMVCapp.ViewModels;

namespace UniversityMVCapp.Services
{
	public class CourseService : ICourseService
	{
		private UniversityContext db;

		public CourseService(UniversityContext context)
		{
			db = context;
		}

		public async Task<List<Course>> GetAll()
		{
			return await db.Courses.ToListAsync();
		}

		public async Task<Course?> GetCourse(int id)
		{
			return await db.Courses.FindAsync(id);
		}
	}
}
