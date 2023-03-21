using UniversityMVCapp.Library.Interfaces;
using UniversityMVCapp.ViewModels;
using UniversityMVCapp.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityMVCapp.Services
{
	public class GroupService : IGroupService
	{
		private UniversityContext _db;

		public GroupService(UniversityContext context)
		{
			_db = context;
		}

		public async Task AddAsync(int courseId, string name)
		{
			try
			{
				Group group = new Group() { Name = name, CourseId = courseId };
				await _db.Groups.AddAsync(group);
				_db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				throw new ArgumentException("Name is null/isn't unique or courseId isn't exist");
			}
		}
		public async Task DelGroupAsync(int groupId)
		{
			Group? group = await _db.Groups.FindAsync(groupId);
			if (group != null) 
			{
				try
				{
					_db.Groups.Remove(group);
					_db.SaveChanges();
				}
				catch (DbUpdateException)
				{
					throw new InvalidOperationException("Group cannot be deleted");
				}
			}
			else
			{
				throw new ArgumentException("GroupId isn't exist");
			}	
		}
		public async Task EditAsync(int groupId, int courseId, string name)
		{
			Group? group = await _db.Groups.FindAsync(groupId);
			if (group != null)
			{
				group.Name = name;
				group.CourseId = courseId;
				try
				{
					_db.Groups.Update(group);
					_db.SaveChanges();
				}
				catch (DbUpdateException)
				{
					throw new ArgumentException("Name isn't unique or courseId isn't exist");
				}
			}
			else
			{
				throw new ArgumentException("GroupId isn't exist");
			}
		}
		public async Task<List<Group>> GetAllAsync()
		{
			return await _db.Groups.ToListAsync();
		}
		public async Task<Group> GetGroupAsync(int groupId)
		{
			Group? group = await _db.Groups.FindAsync(groupId);
			if (group != null)
			{
				return group;
			}
			else
			{
				throw new ArgumentException("GroupId isn't exist");
			}
		}
		public async Task<List<Group>> GetGroupsWithCourseIdAsync(int courseId)
		{
			List<Group>? groups = await _db.Groups.Where(g => g.CourseId == courseId).ToListAsync();

			return groups;
		}
	}
}