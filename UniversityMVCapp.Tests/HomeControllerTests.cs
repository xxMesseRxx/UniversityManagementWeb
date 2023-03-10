using Xunit;
using UniversityMVCapp.Controllers;
using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.ViewModels;
using UniversityMVCapp.Models;
using Microsoft.AspNetCore.Mvc;

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
		public void Index_DBContextWith3Courses_ModelCountIs3Expected()
		{
			//Arrange
			var controller = new HomeController(CreatorOfTestUniversityContext.CreateFilledTestDBContext());

			//Act
			var result = controller.Index() as ViewResult;

			//Assert
			var model = Assert.IsAssignableFrom<List<Course>>(result?.Model);
			Assert.Equal(3, model.Count());
		}
	}
}