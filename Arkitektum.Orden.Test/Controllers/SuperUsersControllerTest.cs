using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Arkitektum.Orden.Controllers;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Arkitektum.Orden.Test.Controllers
{
    public class SuperUsersControllerTest
    {
        private const int IllegalOrganizationId = 42;
        private const int ValidOrganizationId = 7;

        private Mock<ISecurityService> _securityServiceMock;
        private Mock<ISuperUsersService> _superUsersService;

        public SuperUsersControllerTest()
        {
            _superUsersService = new Mock<ISuperUsersService>();
            _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(ValidOrganizationId).Mock();
        }

        [Fact]
        public async Task GetSuperUsersShouldReturnNotFoundWhenOrganizationIsZero()
        {
            var result = await CreateController().GetSuperUsersForOrganization(0) as NotFoundResult;
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSuperUsersShouldReturnForbiddenWhenUserDoesNotHaveAccessToOrganization()
        {
            var result = await CreateController().GetSuperUsersForOrganization(IllegalOrganizationId) as ForbidResult;
            result.Should().NotBeNull();
        }

        
        [Fact]
        public async Task GetSuperUsersShouldReturnOrganizationsWhenUserHaveAccessToOrganization()
        {
            var result = await CreateController().GetSuperUsersForOrganization(ValidOrganizationId) as JsonResult;
            result.Should().NotBeNull();
        }


        [Fact]
        public async Task CreateShouldReturnBadRequestWhenOrganizationIdIsZero()
        {
            var result = await CreateController().Create(new SuperUser()) as BadRequestResult;
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateShouldReturnForbiddenWhenUserDoesNotHaveAccessToOrganization()
        {
            var result = await CreateController().Create(new SuperUser() { OrganizationId = IllegalOrganizationId}) as ForbidResult;
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateShouldReturnJsonWhenCreated()
        {
            _superUsersService.Setup(s => s.AddSuperUserToOrganization(It.IsAny<SuperUser>())).ReturnsAsync(new SuperUser() { Name = "testname"});
            var result = await CreateController().Create(new SuperUser() { OrganizationId = ValidOrganizationId}) as JsonResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
        }



        private SuperUsersController CreateController()
        {
            return new SuperUsersController(_superUsersService.Object, _securityServiceMock.Object);
        }

    }
}
