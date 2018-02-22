using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Controllers;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Arkitektum.Orden.Test.Controllers
{
    public class UsersControllerTest
    {
        [Fact]
        public async Task AdminShouldGetAListOfAllUsers()
        {
            var userServiceMock = new Mock<IUserService>();
            List<ApplicationUser> listOfUsers = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = "john@doe.com" },
                new ApplicationUser() { UserName = "mary@ann.com" },
            };
            userServiceMock.Setup(u => u.GetAll()).ReturnsAsync(listOfUsers);
            var controller = new UsersController(userServiceMock.Object, null, null, null);

            IActionResult result = await controller.Index();

            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();

            var model = viewResult?.Model as List<UserViewModel>;
            model.Should().NotBeNull();

            model?.Count.Should().Be(2);
        }
    }
}