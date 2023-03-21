using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.Models;

namespace UniversityMVCapp.Tests
{
	public class CourseServiceTests
	{
		[Fact]
		public void AddAsync_CorrectArg_CourseCountIs5Expected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();

			//Act
			courseService.AddAsync("New Course").Wait();
			var result = courseService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(5, result);
		}
		[Fact]
		public async void AddAsync_NullArg_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => courseService.AddAsync(null));
		}

		[Fact]
		public void DelCourseAsync_CorrectArg_CourseCountIs3Expected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();
			Course course = courseService.GetAllAsync().Result[3];

			//Act
			courseService.DelCourseAsync(course.CourseId).Wait();
			int result = courseService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(3, result);
		}
		[Fact]
		public async void DelCourseAsync_CourseWithGroups_InvalidOperationExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();
			Course course = courseService.GetAllAsync().Result[1];

			//Act
			await Assert.ThrowsAsync<InvalidOperationException>(() => courseService.DelCourseAsync(course.CourseId));
		}
		[Fact]
		public async void DelCourseAsync_IncorArg_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => courseService.DelCourseAsync(-35));
		}

		[Fact]
		public void EditAsync_CorrectArgs_EditedCourseExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();
			Course course = courseService.GetAllAsync().Result[1];

			//Act
			courseService.EditAsync(course.CourseId, "New course name").Wait();
			string result = courseService.GetAllAsync().Result[1].Name;

			//Assert
			Assert.Equal("New course name", result);
		}
		[Fact]
		public async void EditAsync_CourseIDIsNotExist_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => courseService.EditAsync(-55, "Test course 5"));
		}

		[Fact]
		public void GetAllAsync_Invoke_ListWith4CoursesExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();

			//Act
			int result = courseService.GetAllAsync().Result.Count();

			//Assert
			Assert.Equal(4, result);
		}

		[Fact]
		public void GetCourseAsync_CorrectArg_CourseWithReqIdExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();
			Course expectedCourse = courseService.GetAllAsync().Result[1];

			//Act
			var result = courseService.GetCourseAsync(expectedCourse.CourseId).Result;

			//Assert
			Assert.Equal(expectedCourse, result);
		}
		[Fact]
		public async void GetCourseAsync_IncorrectArg_ArgumentExceptionExpected()
		{
			//Arrange
			var serviceCreator = new CreatorOfTestDataServices(true);
			var courseService = serviceCreator.GetCourseService();

			//Act
			await Assert.ThrowsAsync<ArgumentException>(() => courseService.GetCourseAsync(-55));
		}
	}
}