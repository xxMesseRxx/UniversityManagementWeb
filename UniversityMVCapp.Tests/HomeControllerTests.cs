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
using UniversityMVCapp.Services;

namespace UniversityMVCapp.Tests
{
	public class HomeControllerTests
	{
		[Fact]
		public void Index_HttpGet_IndexViewExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(false);
			var controller = new HomeController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService(),
												serviceCreator.GetStudentService());

			//Act
			var result = controller.Index().Result as ViewResult;

			//Assert
			Assert.Equal("Index", result?.ViewName);
		}
		[Fact]
		public void Index_HttpGet_ResultNotNullExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(false);
			var controller = new HomeController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService(),
												serviceCreator.GetStudentService());

			//Act
			var result = controller.Index().Result as ViewResult;

			//Assert
			Assert.NotNull(result);
		}
		[Fact]
		public void Index_DBContextWith4Courses_InModel4CoursesExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var controller = new HomeController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService(),
												serviceCreator.GetStudentService());

			//Act
			var result = controller.Index().Result as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Course>>(result?.Model);
			Assert.Equal(4, model.Count());
		}

		[Fact]
		public void Groups_CorrectCourseID_GroupsWithReqCourIDExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();
			var groupService = serviceCreator.GetGroupService();
			var controller = new HomeController(courseService, groupService, serviceCreator.GetStudentService());
			Course someCourse = courseService.GetAllAsync().Result[2];
			List<Group> expected = groupService.GetGroupsWithCourseIdAsync(someCourse.CourseId).Result;

			//Act
			var jsonResult = controller.Groups(someCourse.CourseId).Result as JsonResult;
			var result = jsonResult?.Value as List<Group>;

			//Assert
			Assert.Equal(expected, result);
		}
		[Fact]
		public void Groups_CourseIDNotExist_EmptyListExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var controller = new HomeController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService(),
												serviceCreator.GetStudentService());

			//Act
			var jsonResult = controller.Groups(-55).Result as JsonResult;
			var groups = jsonResult?.Value as List<Group>;
			int result = groups.Count();

			//Assert
			Assert.Equal(0, result);
		}

		[Fact]
		public void Students_CorrectGroupID_StudentsWithReqGrIDExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			var controller = new HomeController(serviceCreator.GetCourseService(), groupService, studentService);
			Group someGroup = groupService.GetAllAsync().Result[1];
			List<Student> expected = studentService.GetStudentsWithGroupIdAsync(someGroup.GroupId).Result;

			//Act
			var jsonResult = controller.Students(someGroup.GroupId).Result as JsonResult;
			var result = jsonResult?.Value as List<Student>;

			//Assert
			Assert.Equal(expected, result);
		}
		[Fact]
		public void Students_GroupIDNotExist_EmptyListExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var controller = new HomeController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService(),
												serviceCreator.GetStudentService());

			//Act
			var jsonResult = controller.Students(-66).Result as JsonResult;
			var students = jsonResult?.Value as List<Student>;
			int result = students.Count();

			//Assert
			Assert.Equal(0, result);
		}
	}
}