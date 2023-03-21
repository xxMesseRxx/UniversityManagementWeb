using UniversityMVCapp.Models;
using Xunit;

namespace UniversityMVCapp.Tests
{
	public class GroupServiceTests
	{
		[Fact]
		public void AddAsync_CorrectArg_GroupCountIs7Expected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			Course someCource = courseService.GetAllAsync().Result[1];

			//Act
			groupService.AddAsync(someCource.CourseId, "New group").Wait();
			var result = groupService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(7, result);
		}
		[Fact]
		public async void AddAsync_NameIsNull_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			Course someCource = courseService.GetAllAsync().Result[1];

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => groupService.AddAsync(someCource.CourseId, null));
		}

		[Fact]
		public void DelGroupAsync_CorrectArg_GroupCountIs5Expected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			Group group = groupService.GetAllAsync().Result[0];

			//Act
			groupService.DelGroupAsync(group.GroupId);
			var result = groupService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(5, result);
		}
		[Fact]
		public async void DelGroupAsync_IncorrectArg_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => groupService.DelGroupAsync(-56));
		}
		[Fact]
		public async void DelGroupAsync_GroupWithStudents_InvalidOperationExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			Group group = groupService.GetAllAsync().Result[2];

			//Act
			await Assert.ThrowsAsync<InvalidOperationException>(() => groupService.DelGroupAsync(group.GroupId));
		}

		[Fact]
		public void EditAsync_CorrectArgs_EditedGroupExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			Group group = groupService.GetAllAsync().Result[0];

			//Act
			groupService.EditAsync(group.GroupId, group.CourseId, "New name").Wait();
			var result = groupService.GetAllAsync().Result[0].Name;

			//Assert
			Assert.Equal("New name", result);
		}
		[Fact]
		public async void EditAsync_IncorrectGroupID_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			Group group = groupService.GetAllAsync().Result[0];

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => groupService.EditAsync(-55, group.CourseId, "New name"));
		}

		[Fact]
		public void GetAllAsync_Invoke_ListWith6GroupsExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();

			//Act
			int result = groupService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(6, result);
		}

		[Fact]
		public void GetGroupAsync_CorrectArg_GroupWithReqIdExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			Group expecteGroup = groupService.GetAllAsync().Result[0];

			//Act
			var result = groupService.GetGroupAsync(expecteGroup.GroupId).Result;

			//Assert
			Assert.Equal(expecteGroup, result);
		}
		[Fact]
		public async void GetGroupAsync_IncorrectGroupID_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => groupService.GetGroupAsync(-55));
		}

		[Fact]
		public void GetGroupsWithCourseIdAsync_CorrectArg_3GroupExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();
			var courseService = serviceCreator.GetCourseService();
			Course course = courseService.GetAllAsync().Result[2];

			//Act
			var groups = groupService.GetGroupsWithCourseIdAsync(course.CourseId).Result;
			int result = groups.Count();

			//Assert
			Assert.Equal(3, result);
		}
		[Fact]
		public void GetGroupsWithCourseIdAsync_IncorCourseID_EmptyListExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var groupService = serviceCreator.GetGroupService();

			//Act
			var groups = groupService.GetGroupsWithCourseIdAsync(-55).Result;
			int result = groups.Count();

			//Assert
			Assert.Equal(0, result);
		}
	}
}
