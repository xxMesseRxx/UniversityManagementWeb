using UniversityMVCapp.Models;

namespace UniversityMVCapp.Library.Interfaces
{
	public interface IStudentService
	{
		public Task<List<Student>> GetAllAsync();
		public Task<List<Student>> GetStudentsWithGroupIdAsync(int groupId);
		public Task<Student> GetStudentAsync(int studentId);
		public Task DelStudentAsync(int studentId);
		public Task AddAsync(int groupId, string firstName, string lastName);
		public Task EditAsync(int studentId, int groupId, string firstName, string lastName);
	}
}