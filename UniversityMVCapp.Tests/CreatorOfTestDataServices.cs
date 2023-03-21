using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityMVCapp.Services;
using UniversityMVCapp.ViewModels;

namespace UniversityMVCapp.Tests
{
	public class CreatorOfTestDataServices
	{
		private UniversityContext _testDB;

		public CreatorOfTestDataServices(bool DBIsFilled)
		{
			if (DBIsFilled)
			{
				_testDB = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			}
			else
			{
				_testDB = CreatorOfTestUniversityContext.CreateEmptyContext();
			}
		}

		public CourseService GetCourseService()
		{
			return new CourseService(_testDB);
		}

		public GroupService GetGroupService()
		{
			return new GroupService(_testDB);
		}

		public StudentService GetStudentService()
		{
			return new StudentService(_testDB);
		}
	}
}
