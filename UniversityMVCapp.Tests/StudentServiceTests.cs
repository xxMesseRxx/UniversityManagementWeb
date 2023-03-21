using UniversityMVCapp.Models;
using Xunit;

namespace UniversityMVCapp.Tests
{
	public class StudentServiceTests
	{
		[Fact]
		public void AddAsync_CorrectArg_StudentsCountIs12Expected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			Group group = groupService.GetAllAsync().Result[1];

			//Act
			studentService.AddAsync(group.GroupId, "Name", "Last name").Wait();
			var result = studentService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(12, result);
		}
		[Fact]
		public async void AddAsync_NullArg_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			Group group = groupService.GetAllAsync().Result[1];

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => studentService.AddAsync(group.GroupId, null, "Last name"));
		}

		[Fact]
		public void DelStudentAsync_CorrectArg_StudentCountIs10Expected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			Student student = studentService.GetAllAsync().Result[1];

			//Act
			studentService.DelStudentAsync(student.StudentId);
			var result = studentService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(10, result);
		}
		[Fact]
		public async void DelStudentAsync_IncorrectArg_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => studentService.DelStudentAsync(-56));
		}

		[Fact]
		public void EditAsync_CorrectArgs_EditedStudentExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			Student student = studentService.GetAllAsync().Result[1];

			//Act
			studentService.EditAsync(student.StudentId, student.GroupId, "New name first name", "New name last name").Wait();
			var result = studentService.GetAllAsync().Result[1].FirstName;

			//Assert
			Assert.Equal("New name first name", result);
		}
		[Fact]
		public async void EditAsync_IncorrectStudentID_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			Student student = studentService.GetAllAsync().Result[1];

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => studentService.EditAsync(-55, student.GroupId,
																					   "New name first name",
																					   "New name last name"));
		}

		[Fact]
		public void GetAllAsync_Invoke_ListWith11StudentsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();

			//Act
			int result = studentService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(11, result);
		}

		[Fact]
		public void GetStudentAsync_CorrectArg_StudentWithReqIdExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			Student expectedStudent = studentService.GetAllAsync().Result[1];

			//Act
			var result = studentService.GetStudentAsync(expectedStudent.StudentId).Result;

			//Assert
			Assert.Equal(expectedStudent, result);
		}
		[Fact]
		public async void GetStudentAsync_IncorrectStudentID_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => studentService.GetStudentAsync(-55));
		}

		[Fact]
		public void GetStudentsWithGroupIdAsync_CorrectArg_2StudentsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();
			var groupService = serviceCreator.GetGroupService();
			Group group = groupService.GetAllAsync().Result[1];

			//Act
			var students = studentService.GetStudentsWithGroupIdAsync(group.GroupId).Result;
			int result = students.Count();

			//Assert
			Assert.Equal(2, result);
		}
		[Fact]
		public void GetStudentsWithGroupIdAsync_IncorStudentID_EmptyListExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var studentService = serviceCreator.GetStudentService();

			//Act
			var students = studentService.GetStudentsWithGroupIdAsync(-35).Result;
			int result = students.Count();

			//Assert
			Assert.Equal(0, result);
		}
	}
}
