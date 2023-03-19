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

		public async Task AddAsync(string name, string? description)
		{
			try
			{
				Course course = new Course() { Name = name, Description = description };
				await _db.Courses.AddAsync(course);
				_db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				throw new ArgumentException("Name isn't unique");
			}
		}

		public async Task DelCourseAsync(int courseId)
		{
			Course? course = await _db.Courses.FindAsync(courseId);
			if (course != null)
			{
				try
				{
					_db.Courses.Remove(course);
					_db.SaveChanges();
				}
				catch (DbUpdateException)
				{
					throw new InvalidOperationException("Course cannot be deleted");
				}
			}
			else
			{
				throw new ArgumentException("CourseId isn't exist");
			}
		}

		public async Task EditAsync(int courseId, string name, string? description)
		{
			Course? course = await _db.Courses.FindAsync(courseId);
			if (course != null)
			{
				course.Name = name;
				course.Description = description;
				try
				{
					_db.Courses.Update(course);
					_db.SaveChanges();
				}
				catch (DbUpdateException)
				{
					throw new ArgumentException("Name isn't unique");
				}
			}
			else
			{
				throw new ArgumentException("CourseId isn't exist");
			}
		}

		public async Task<List<Course>> GetAllAsync()
		{
			return await _db.Courses.ToListAsync();
		}
		public async Task<Course> GetCourseAsync(int courseId)
		{
			Course? course = await _db.Courses.FindAsync(courseId);
			if (course != null)
			{
				return course;
			}
			else
			{
				throw new ArgumentException("CourseId isn't exist");
			}
		}
	}
}
