using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Tests
{
    public class UserControllerTests
    {
        private UserController _controller;

        public UserControllerTests()
        {
            // Initialize UserController with an empty list for each test
            UserController.userlist = new List<User>();
            _controller = new UserController();
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAListOfUsers()
        {
            // Arrange
            UserController.userlist.Add(new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" });
            UserController.userlist.Add(new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" });

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Details_WithUnexistingId_ReturnsNotFound()
        {
            // Act
            var result = _controller.Details(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_WithExistingId_ReturnsViewResult()
        {
            // Arrange
            UserController.userlist.Add(new User { Id = 1, Name = "Test User", Email = "test@example.com" });

            // Act
            var result = _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Create_PostValidUser_RedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = 1, Name = "New User", Email = "new@example.com" };

            // Act
            var result = _controller.Create(user);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Single(UserController.userlist);
        }

        [Fact]

        public void Create_PostInvalidUser_ReturnsViewWithModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = _controller.Create(new User());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }
    }
}