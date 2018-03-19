using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Arkitektum.Orden.Controllers;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Arkitektum.Orden.Test.Controllers
{
    public class ApplicationsControllerTest
    {
        private readonly Mock<IApplicationService> _applicationServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private Mock<ISecurityService> _securityServiceMock;
        private readonly Mock<ISectorService> _sectorServiceMock;

        public ApplicationsControllerTest()
        {
            _applicationServiceMock = new Mock<IApplicationService>();
            _userServiceMock = new Mock<IUserService>();
            _securityServiceMock = new Mock<ISecurityService>();
            _sectorServiceMock = new Mock<ISectorService>();
        }

        [Fact]
        public async Task IndexShouldReturnApplicationsForCurrentOrganization()
        {
            var currentOrganizationId = 1;
            _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(currentOrganizationId).Mock();
            var applications = new List<Application>()
            {
                new Application()
                {
                    Name = "Test application"
                }
            };
            
            _applicationServiceMock.Setup(m => m.GetAllApplicationsForOrganisation(currentOrganizationId)).ReturnsAsync(applications);
            
            var controller = CreateController();

            var result = await controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ApplicationViewModel>>(viewResult.ViewData.Model);

            model.Should().HaveCount(1);
        }

        [Fact]
        public async Task DetailsShouldReturnNotFoundWhenIdIsNull()
        {
            var result = await CreateController().Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DetailsShouldReturnNotFoundWhenNoApplicationWithGivenIdExists()
        {
            var result = await CreateController().Details(12345);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DetailsShouldReturnApplicationWithGivenId()
        {
            var applicationId = 12345;
            _applicationServiceMock.Setup(m => m.GetAsync(applicationId)).ReturnsAsync(new Application()
            {
                Id = applicationId
            });
            
            var result = await CreateController().Details(applicationId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ApplicationViewModel>(viewResult.ViewData.Model);

            model.Id.Should().Be(applicationId);
        }

        private ApplicationsController CreateController()
        {
            return new ApplicationsController(_securityServiceMock.Object, _applicationServiceMock.Object, _userServiceMock.Object,
              _sectorServiceMock.Object, null);
        }
    }
}
