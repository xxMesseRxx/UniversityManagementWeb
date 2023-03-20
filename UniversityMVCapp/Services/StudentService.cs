using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.Library.Interfaces;
using UniversityMVCapp.Models;
using UniversityMVCapp.ViewModels;

namespace UniversityMVCapp.Services
{
	public class StudentService : IStudentService
	{
		private UniversityContext _db;

		public StudentService(UniversityContext context)
		{
			_db = context;
		}

		public async Task AddAsync(int groupId, string firstName, string lastName)
		{
			try
			{
				Student student = new Student() { GroupId = groupId, FirstName = firstName, LastName = lastName };
				await _db.Students.AddAsync(student);
				_db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				throw new ArgumentException("GroupId isn't exist");
			}
		}
		public async Task DelStudentAsync(int studentId)
		{
			Student? student = await _db.Students.FindAsync(studentId);
			if (student != null)
			{
				_db.Students.Remove(student);
				_db.SaveChanges();
			}
			else
			{
				throw new ArgumentException("StudentId isn't exist");
			}
		}
		public async Task EditAsync(int studentId, int groupId, string firstName, string lastName)
		{
			Student? student = await _db.Students.FindAsync(studentId);
			if (student != null)
			{
				student.FirstName = firstName;
				student.LastName = lastName;
				student.GroupId = groupId;
				try
				{
					_db.Students.Update(student);
					_db.SaveChanges();
				}
				catch (DbUpdateException)
				{
					throw new ArgumentException("GroupId isn't exist");
				}
			}
			else
			{
				throw new ArgumentException("StudentId isn't exist");
			}
		}
		public async Task<List<Student>> GetAllAsync()
		{
			return await _db.Students.ToListAsync();
		}
		public async Task<Student> GetStudentAsync(int studentId)
		{
			Student? student = await _db.Students.FindAsync(studentId);
			if (student != null)
			{
				return student;
			}
			else
			{
				throw new ArgumentException("StudentId isn't exist");
			}
		}
		public async Task<List<Student>> GetStudentsWithGroupIdAsync(int groupId)
		{
			List<Student>? student = await _db.Students.Where(s => s.GroupId == groupId).ToListAsync();

			return student;
		}
	}
}
