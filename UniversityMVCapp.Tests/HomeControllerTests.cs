using Xunit;
using UniversityMVCapp.Controllers;
using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.ViewModels;
using UniversityMVCapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Internal;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		public void EditGroupsPost_AddGroupWithCorData_7GroupExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			int courseId = testDBContext.Courses.ToList()[0].CourseId;

			//Act
			controller.EditGroups(null, "Added Group", courseId);
			int result = testDBContext.Groups.Count();

			//Assert
			Assert.Equal(7, result);
		}
		[Fact]
		public void EditGroupsPost_AddGroupWithIncorData_6GroupExpected()
		{
			//Arrange
			var testDBContext = CreatorOfTestUniversityContext.CreateFilledTestDBContext();
			var controller = new HomeController(testDBContext);
			int courseId = testDBContext.Courses.ToList()[0].CourseId;

			//Act
			controller.EditGroups(null, null, courseId);
			int result = testDBContext.Groups.Count();

			//Assert
			Assert.Equal(6, result);
		}
	}
}