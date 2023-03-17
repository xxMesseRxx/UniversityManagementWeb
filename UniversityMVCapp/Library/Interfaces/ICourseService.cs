using System.Collections.Generic;
using UniversityMVCapp.Models;

namespace UniversityMVCapp.Library.Interfaces
{
	public interface ICourseService
	{
		public Task<List<Course>> GetAll();
	}
}
