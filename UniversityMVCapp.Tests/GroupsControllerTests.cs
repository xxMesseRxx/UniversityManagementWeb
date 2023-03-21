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
	public class GroupsControllerTests
	{
		[Fact]
		public void EditGroupsGet_HttpGet_EditGroupsViewExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(false);
			var controller = new GroupsController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService());

			//Act
			var result = controller.EditGroups().Result as ViewResult;

			//Assert
			Assert.Equal("EditGroups", result?.ViewName);
		}
		[Fact]
		public void EditGroupsGet_HttpGet_ResultNotNullExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(false);
			var controller = new GroupsController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService());

			//Act
			var result = controller.EditGroups().Result as ViewResult;

			//Assert
			Assert.NotNull(result);
		}
		[Fact]
		public void EditGroupsGet_DBContextWith6Groups_InModel6GroupsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var controller = new GroupsController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService());

			//Act
			var result = controller.EditGroups().Result as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Group>>(result?.Model);
			Assert.Equal(6, model.Count());
		}
		[Fact]
		public void EditGroupsGet_DBContextWith4Courses_InViewBag4CoursesExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var controller = new GroupsController(serviceCreator.GetCourseService(),
												serviceCreator.GetGroupService());

			//Act
			var result = controller.EditGroups().Result as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Course>>(result?.ViewData["Courses"]);
			Assert.Equal(4, model.Count());
		}

		[Fact]
		public async void EditGroupsPost_AddGroupWithCorData_7GroupsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			var controller = new GroupsController(courseService, groupService);
			int courseId = courseService.GetAllAsync().Result[0].CourseId;

			//Act
			await controller.EditGroups(null, "Added Group", courseId);
			int result = groupService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(7, result);
		}
		[Fact]
		public async void EditGroupsPost_AddGroupWithIncorData_6GroupsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			var controller = new GroupsController(courseService, groupService);
			int courseId = courseService.GetAllAsync().Result[0].CourseId;

			//Act
			await controller.EditGroups(null, null, courseId);
			int result = groupService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(6, result);
		}
		[Fact]
		public async void EditGroupsPost_EditGroupWithCorData_EditedGroupExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			var controller = new GroupsController(courseService, groupService);
			Group someGroup = groupService.GetAllAsync().Result[1];

			//Act
			await controller.EditGroups(someGroup.GroupId, "New group name", someGroup.CourseId);
			string result = groupService.GetAllAsync().Result[1].Name;

			//Assert
			Assert.Equal("New group name", result);
		}
		[Fact]
		public async void EditGroupsPost_EditGroupWithIncorId_UneditedGroupExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			var controller = new GroupsController(courseService, groupService);
			Group someGroup = groupService.GetAllAsync().Result[1];
			string expected = someGroup.Name;

			//Act
			await controller.EditGroups(-666, "New group name", someGroup.CourseId);
			string result = groupService.GetAllAsync().Result[1].Name;

			//Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public async void EditGroupsDel_DelEmptyGroup_GroupWasDelExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			var controller = new GroupsController(courseService, groupService);
			Group someGroup = groupService.GetAllAsync().Result[0];

			//Act
			await controller.EditGroups(someGroup.GroupId);
			int result = groupService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(5, result);
		}
		[Fact]
		public async void EditGroupsDel_DelGroupWithStudents_ExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			var controller = new GroupsController(courseService, groupService);
			Group someGroup = groupService.GetAllAsync().Result[2];

			//Act
			await Assert.ThrowsAsync<NullReferenceException>(() => controller.EditGroups(someGroup.GroupId));
		}
		[Fact]
		public async void EditGroupsDel_DelGroupIncorId_ExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			var controller = new GroupsController(courseService, groupService);

			//Act
			await Assert.ThrowsAsync<NullReferenceException>(() => controller.EditGroups(-55));
		}
	}
}