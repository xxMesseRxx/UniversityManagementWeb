using Xunit;
using UniversityMVCapp.Controllers;
using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.ViewModels;
using UniversityMVCapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Internal;
using Azure;

namespace UniversityMVCapp.Tests
{
	public class HomeControllerTests
	{
		[Fact]
		public void Index_HttpGet_IndexViewExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateEmptyContext());

			//Act
			var result = controller.Index() as ViewResult;

			//Assert
			Assert.Equal("Index", result?.ViewName);
		}
		[Fact]
		public void Index_HttpGet_ResultNotNullExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateEmptyContext());

			//Act
			var result = controller.Index() as ViewResult;

			//Assert
			Assert.NotNull(result);
		}
		[Fact]
		public void Index_DBContextWith3Courses_InModel3CoursesExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateFilledTestDBContext());

			//Act
			var result = controller.Index() as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Course>>(result?.Model);
			Assert.Equal(3, model.Count());
		}

		[Fact]
		public void Groups_CorrectCourseID_GroupsWithReqCourIDExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Course someCourse = testDBContext.Courses.ToList()[2];
			List<Group> expected = testDBContext.Groups.Where(g => g.CourseId == someCourse.CourseId).ToList();

			//Act
			var jsonResult = controller.Groups(someCourse.CourseId.ToString()) as JsonResult;
			var result = jsonResult?.Value as List<Group>;

			//Assert
            Assert.Equal(expected, result);
		}
		[Fact]
		public void Groups_ParamIsNull_EmptyListExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);

			//Act
			var jsonResult = controller.Groups("") as JsonResult;
			var groups = jsonResult?.Value as List<Group>;
			int result = groups.Count();

			//Assert
			Assert.Equal(0, result);
		}
		[Fact]
		public void Groups_ParamIsUncorrect_EmptyListExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);

			//Act
			var jsonResult = controller.Groups("sg11") as JsonResult;
			var groups = jsonResult?.Value as List<Group>;
			int result = groups.Count();

			//Assert
			Assert.Equal(0, result);
		}

		[Fact]
		public void Students_CorrectGroupID_studentsWithReqGrIDExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Group someGroup = testDBContext.Groups.ToArray()[1];
			List<Student> expected = testDBContext.Students.Where(s => s.GroupId == someGroup.GroupId).ToList();

			//Act
			var jsonResult = controller.Students(someGroup.GroupId.ToString()) as JsonResult;
			var result = jsonResult?.Value as List<Student>;

			//Assert
			Assert.Equal(expected, result);
		}
		[Fact]
		public void Students_ParamIsNull_EmptyListExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);

			//Act
			var jsonResult = controller.Students("") as JsonResult;
			var students = jsonResult?.Value as List<Student>;
			int result = students.Count();

			//Assert
			Assert.Equal(0, result);
		}
		[Fact]
		public void Students_ParamIsUncorrect_EmptyListExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);

			//Act
			var jsonResult = controller.Students("sdff11") as JsonResult;
			var students = jsonResult?.Value as List<Student>;
			int result = students.Count();

			//Assert
			Assert.Equal(0, result);
		}

		[Fact]
		public void EditGroupsGet_HttpGet_EditGroupsViewExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateEmptyContext());

			//Act
			var result = controller.EditGroups() as ViewResult;

			//Assert
			Assert.Equal("EditGroups", result?.ViewName);
		}
		[Fact]
		public void EditGroupsGet_HttpGet_ResultNotNullExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateEmptyContext());

			//Act
			var result = controller.EditGroups() as ViewResult;

			//Assert
			Assert.NotNull(result);
		}
		[Fact]
		public void EditGroupsGet_DBContextWith6Groups_InModel6GroupsExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateFilledTestDBContext());

			//Act
			var result = controller.EditGroups() as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Group>>(result?.Model);
			Assert.Equal(6, model.Count());
		}
		[Fact]
		public void EditGroupsGet_DBContextWith3Courses_InViewBag3CoursesExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateFilledTestDBContext());

			//Act
			var result = controller.EditGroups() as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Course>>(result?.ViewData["Courses"]);
			Assert.Equal(3, model.Count());
		}

		[Fact]
		public async void EditGroupsPost_AddGroupWithCorData_7GroupsExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			int courseId = testDBContext.Courses.ToList()[0].CourseId;

			//Act
			await controller.EditGroups(null, "Added Group", courseId);
			int result = testDBContext.Groups.Count();

			//Assert
			Assert.Equal(7, result);
		}
		[Fact]
		public async void EditGroupsPost_AddGroupWithIncorData_6GroupsExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			int courseId = testDBContext.Courses.ToList()[0].CourseId;

			//Act
			await controller.EditGroups(null, null, courseId);
			int result = testDBContext.Groups.Count();

			//Assert
			Assert.Equal(6, result);
		}
		[Fact]
		public async void EditGroupsPost_EditGroupWithCorData_EditedGroupExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Group someGroup = testDBContext.Groups.ToList()[1];

			//Act
			await controller.EditGroups(someGroup.GroupId, "New group name", someGroup.CourseId);
			string result = testDBContext.Groups.ToList()[1].Name;

			//Assert
			Assert.Equal("New group name", result);
		}
		[Fact]
		public async void EditGroupsPost_EditGroupWithIncorId_UneditedGroupExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Group someGroup = testDBContext.Groups.ToList()[1];
			string expected = someGroup.Name;

			//Act
			await controller.EditGroups(-666, "New group name", someGroup.CourseId);
			string result = testDBContext.Groups.ToList()[1].Name;

			//Assert
			Assert.Equal(expected, result);
		}
		[Fact]
		public async void EditGroupsDel_DelEmptyGroup_GroupWasDelExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Group someGroup = testDBContext.Groups.ToList()[0];

			//Act
			await controller.EditGroups(someGroup.GroupId.ToString());
			int result = testDBContext.Groups.Count();

			//Assert
			Assert.Equal(5, result);
		}
		[Fact]
		public void EditGroupsDel_DelGroupWithStudents_ArgumentExceptionExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Group someGroup = testDBContext.Groups.ToList()[2];

			//Act
			Assert.ThrowsAsync<ArgumentException>(() => controller.EditGroups(someGroup.GroupId.ToString()));
		}
		[Fact]
		public void EditGroupsDel_DelGroupIncorId_ArgumentExceptionExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);

			//Act
			Assert.ThrowsAsync<ArgumentException>(() => controller.EditGroups("-55"));
		}

		[Fact]
		public void EditStudentsGet_HttpGet_EditStudentsViewExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateEmptyContext());

			//Act
			var result = controller.EditStudents() as ViewResult;

			//Assert
			Assert.Equal("EditStudents", result?.ViewName);
		}
		[Fact]
		public void EditStudentsGet_HttpGet_ResultNotNullExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateEmptyContext());

			//Act
			var result = controller.EditStudents() as ViewResult;

			//Assert
			Assert.NotNull(result);
		}
		[Fact]
		public void EditStudentsGet_DBContextWith11students_InModel11StudentsExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateFilledTestDBContext());

			//Act
			var result = controller.EditStudents() as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Student>>(result?.Model);
			Assert.Equal(11, model.Count());
		}
		[Fact]
		public void EditStudentsGet_DBContextWith6Groups_InViewBag6GroupsExpected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateFilledTestDBContext());

			//Act
			var result = controller.EditStudents() as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Group>>(result?.ViewData["Groups"]);
			Assert.Equal(6, model.Count());
		}

		[Fact]
		public async void EditStudentsPost_AddStudentWithCorData_12StudentsExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			int groupId = testDBContext.Groups.ToList()[3].GroupId;

			//Act
			await controller.EditStudents(null, "New first name", "New second name", groupId);
			int result = testDBContext.Students.Count();

			//Assert
			Assert.Equal(12, result);
		}
		[Fact]
		public async void EditStudentsPost_AddStudentWithIncorData_11StudentsExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			int groupId = testDBContext.Groups.ToList()[3].GroupId;

			//Act
			await controller.EditStudents(null, null, "Second name", groupId);
			int result = testDBContext.Students.Count();

			//Assert
			Assert.Equal(11, result);
		}
		[Fact]
		public async void EditStudentsPost_EditStudentWithCorData_EditedStudentExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Student someStudent = testDBContext.Students.ToList()[1];

			//Act
			await controller.EditStudents(someStudent.StudentId, "New first name", "New second name", someStudent.GroupId);
			string result = testDBContext.Students.ToList()[1].FirstName;

			//Assert
			Assert.Equal("New first name", result);
		}
		[Fact]
		public async void EditStudentsPost_EditStudentWithIncorId_UneditedStudentExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Student someStudent = testDBContext.Students.ToList()[1];
			string expected = someStudent.FirstName;

			//Act
			await controller.EditStudents(-5598, "New first name", "New second name", someStudent.GroupId);
			string result = testDBContext.Students.ToList()[1].FirstName;

			//Assert
			Assert.Equal(expected, result);
		}
		[Fact]
		public async void EditStudentsDel_DelStudentWithCorId_StudentWasDelExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			Student someStudent = testDBContext.Students.ToList()[1];

			//Act
			await controller.EditStudents(someStudent.StudentId.ToString());
			int result = testDBContext.Students.Count();

			//Assert
			Assert.Equal(10, result);
		}
		[Fact]
		public void EditStudentsDel_DelStudentIncorId_ArgumentExceptionExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);

			//Act
			Assert.ThrowsAsync<ArgumentException>(() => controller.EditStudents("-55"));
		}
	}
}