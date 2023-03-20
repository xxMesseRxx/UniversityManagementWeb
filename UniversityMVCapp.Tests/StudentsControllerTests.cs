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
	public class StudentsControllerTests
	{
		[Fact]
		public void EditStudentsGet_HttpGet_EditStudentsViewExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(false);
			var controller = new StudentsController(serviceCreator.GetGroupService(),
													serviceCreator.GetStudentService());

			//Act
			var result = controller.EditStudents().Result as ViewResult;

			//Assert
			Assert.Equal("EditStudents", result?.ViewName);
		}
		[Fact]
		public void EditStudentsGet_HttpGet_ResultNotNullExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(false);
			var controller = new StudentsController(serviceCreator.GetGroupService(),
													serviceCreator.GetStudentService());

			//Act
			var result = controller.EditStudents().Result as ViewResult;

			//Assert
			Assert.NotNull(result);
		}
		[Fact]
		public void EditStudentsGet_DBContextWith11Students_InModel11StudentsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var controller = new StudentsController(serviceCreator.GetGroupService(),
													serviceCreator.GetStudentService());

			//Act
			var result = controller.EditStudents().Result as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Student>>(result?.Model);
			Assert.Equal(11, model.Count());
		}
		[Fact]
		public void EditStudentsGet_DBContextWith6Groups_InViewBag6GroupsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var controller = new StudentsController(serviceCreator.GetGroupService(),
													serviceCreator.GetStudentService());

			//Act
			var result = controller.EditStudents().Result as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Group>>(result?.ViewData["Groups"]);
			Assert.Equal(6, model.Count());
		}

		[Fact]
		public async void EditStudentsPost_AddStudentWithCorData_12StudentsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			var controller = new StudentsController(groupService, studentService);
			int groupId = groupService.GetAllAsync().Result[3].GroupId;

			//Act
			await controller.EditStudents(null, "New first name", "New second name", groupId);
			int result = studentService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(12, result);
		}
		[Fact]
		public async void EditStudentsPost_AddStudentWithIncorData_11StudentsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			var controller = new StudentsController(groupService, studentService);
			int groupId = groupService.GetAllAsync().Result[3].GroupId;

			//Act
			await controller.EditStudents(null, null, "Second name", groupId);
			int result = studentService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(11, result);
		}
		[Fact]
		public async void EditStudentsPost_EditStudentWithCorData_EditedStudentExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			var controller = new StudentsController(groupService, studentService);
			Student someStudent = studentService.GetAllAsync().Result[1];

			//Act
			await controller.EditStudents(someStudent.StudentId, "New first name", "New second name", someStudent.GroupId);
			string result = studentService.GetAllAsync().Result[1].FirstName;

			//Assert
			Assert.Equal("New first name", result);
		}
		[Fact]
		public async void EditStudentsPost_EditStudentWithIncorId_UneditedStudentExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			var controller = new StudentsController(groupService, studentService);
			Student someStudent = studentService.GetAllAsync().Result[1];
			string expected = someStudent.FirstName;

			//Act
			await controller.EditStudents(-5598, "New first name", "New second name", someStudent.GroupId);
			string result = studentService.GetAllAsync().Result[1].FirstName;

			//Assert
			Assert.Equal(expected, result);
		}
		[Fact]

		public async void EditStudentsDel_DelStudentWithCorId_StudentWasDelExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			var controller = new StudentsController(groupService, studentService);
			Student someStudent = studentService.GetAllAsync().Result[1];

			//Act
			await controller.EditStudents(someStudent.StudentId);
			int result = studentService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(10, result);
		}
		[Fact]
		public void EditStudentsDel_DelStudentIncorId_ExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var controller = new StudentsController(serviceCreator.GetGroupService(),
													serviceCreator.GetStudentService());

			//Act
			Assert.ThrowsAsync<Exception>(() => controller.EditStudents(-55));
		}
	}
}