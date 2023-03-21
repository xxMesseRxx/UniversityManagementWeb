using UniversityMVCapp.Models;

namespace UniversityMVCapp.Library.Interfaces
{
	public interface IGroupService
	{
		public Task<List<Group>> GetAllAsync();
		public Task<List<Group>> GetGroupsWithCourseIdAsync(int courseId);
		public Task<Group> GetGroupAsync(int groupId);
		public Task DelGroupAsync(int groupId);
		public Task AddAsync(int courseId, string name);
		public Task EditAsync(int groupId, int courseId, string name);
	}
}
