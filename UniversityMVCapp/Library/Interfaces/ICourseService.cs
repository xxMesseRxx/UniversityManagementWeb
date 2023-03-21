using System.Collections.Generic;
using UniversityMVCapp.Models;

namespace UniversityMVCapp.Library.Interfaces
{
	public interface ICourseService
	{
		public Task<List<Course>> GetAllAsync();
		public Task<Course?> GetCourseAsync(int id);
		public Task DelCourseAsync(int courseId);
		public Task AddAsync(string name, string description);
		public Task EditAsync(int courseId, string name, string description);
	}
}
