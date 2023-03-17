using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.Library.Interfaces;
using UniversityMVCapp.Models;
using UniversityMVCapp.ViewModels;

namespace UniversityMVCapp.Services
{
	public class CourseService : ICourseService
	{
		private UniversityContext _db;

		public CourseService(UniversityContext context)
		{
			_db = context;
		}

		public async Task<List<Course>> GetAllAsync()
		{
			return await _db.Courses.ToListAsync();
		}
		public async Task<Course?> GetCourseAsync(int id)
		{
			return await _db.Courses.FindAsync(id);
		}
	}
}
