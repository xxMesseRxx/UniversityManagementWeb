using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.Models;
using UniversityMVCapp.ViewModels;

namespace UniversityMVCapp.Tests
{
	public static class CreatorOfTestUniversityContext
	{
		public static UniversityContext CreateFilledTestDBContext()
		{
			var testDBContext = CreateEmptyContext();

			var courses = CreateTestCourses();
			testDBContext.Courses.AddRange(courses);
			testDBContext.SaveChanges();

			var groups = CreateTestGroups(testDBContext.Courses.ToArray());
			testDBContext.Groups.AddRange(groups);
			testDBContext.SaveChanges();

			var students = CreateTestStudents(testDBContext.Groups.ToArray());
			testDBContext.Students.AddRange(students);
			testDBContext.SaveChanges();

			return testDBContext;
		}
		public static UniversityContext CreateEmptyContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

			return new UniversityContext(optionsBuilder.Options);
		}

		private static Course[] CreateTestCourses()
		{
			Course[] courses =
			{
				new Course {Name = "Test course 1", Description = "Some description 1"},
				new Course {Name = "Test course 2", Description = "Some description 2"},
				new Course {Name = "Test course 3", Description = "Some description 3"},
				new Course {Name = "Test course 4", Description = "Some description 4"}
			};

			return courses;
		}
		private static Group[] CreateTestGroups(Course[] courses)
		{
			Group[] groups =
			{
				new Group {Name = "Test group 1", CourseId = courses[0].CourseId},
				new Group {Name = "Test group 2", CourseId = courses[1].CourseId},
				new Group {Name = "Test group 3", CourseId = courses[1].CourseId},
				new Group {Name = "Test group 4", CourseId = courses[2].CourseId},
				new Group {Name = "Test group 5", CourseId = courses[2].CourseId},
				new Group {Name = "Test group 6", CourseId = courses[2].CourseId}
			};

			return groups;
		}
		private static Student[] CreateTestStudents(Group[] groups)
		{
			Student[] students =
			{
				new Student {FirstName = "First name 1", LastName = "Last name 1", GroupId = groups[1].GroupId},
				new Student {FirstName = "First name 2", LastName = "Last name 2", GroupId = groups[1].GroupId},
				new Student {FirstName = "First name 3", LastName = "Last name 3", GroupId = groups[2].GroupId},
				new Student {FirstName = "First name 4", LastName = "Last name 4", GroupId = groups[3].GroupId},
				new Student {FirstName = "First name 5", LastName = "Last name 5", GroupId = groups[3].GroupId},
				new Student {FirstName = "First name 6", LastName = "Last name 6", GroupId = groups[3].GroupId},
				new Student {FirstName = "First name 7", LastName = "Last name 7", GroupId = groups[4].GroupId},
				new Student {FirstName = "First name 8", LastName = "Last name 8", GroupId = groups[4].GroupId},
				new Student {FirstName = "First name 9", LastName = "Last name 9", GroupId = groups[5].GroupId},
				new Student {FirstName = "First name 10", LastName = "Last name 10", GroupId = groups[5].GroupId},
				new Student {FirstName = "First name 11", LastName = "Last name 11", GroupId = groups[5].GroupId},
			};

			return students;
		}
	}
}
