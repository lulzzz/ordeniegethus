using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Arkitektum.Orden.Controllers;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Services.AppRegistry;
using Arkitektum.Orden.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Arkitektum.Orden.Test.Controllers
{
    public class ApplicationsControllerTest
    {
        private const int OrganizationId = 1;
        private const int ApplicationId = 42;

        private readonly Mock<IApplicationService> _applicationServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private Mock<ISecurityService> _securityServiceMock;
        private readonly Mock<ISectorService> _sectorServiceMock;
        private Mock<IVendorService> _vendorServiceMock;

        public ApplicationsControllerTest()
        {
            _applicationServiceMock = new Mock<IApplicationService>();
            _userServiceMock = new Mock<IUserService>();
            _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(OrganizationId).Mock();
            _sectorServiceMock = new Mock<ISectorService>();
            _vendorServiceMock = new Mock<IVendorService>();
            _vendorServiceMock.Setup(v => v.GetAll()).ReturnsAsync(new List<Vendor>());
        }

        //[Fact]
        //public async Task IndexShouldReturnApplicationsForCurrentOrganization()
        //{
        //    _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(OrganizationId).Mock();
        //    UserHasAccessToOrganization(AccessLevel.Read, true);

        //    var applications = new List<Application>()
        //    {
        //        new Application()
        //        {
        //            Name = "Test application"
        //        }
        //    };
            
        //    _applicationServiceMock.Setup(m => m.GetAllApplicationsForOrganization(OrganizationId)).ReturnsAsync(applications);
            
        //    var controller = CreateController();

        //    var result = await controller.Index(null);
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<ApplicationListViewModel>(viewResult.ViewData.Model);

        //    model.Should().Be(applications);
        //}

        [Fact]
        public async Task IndexShouldReturnForbiddenWhenUserDontHaveReadAccess()
        {
            UserHasAccessToOrganization(AccessLevel.Read, false);
            var result = await CreateController().Index(null);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task DetailsJsonShouldReturnNotFoundWhenIdIsNull()
        {
            UserHasAccessToOrganization(AccessLevel.Read, true);

            var result = await CreateController().DetailsJson(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DetailsJsonShouldReturnNotFoundWhenNoApplicationWithGivenIdExists()
        {
            UserHasAccessToOrganization(AccessLevel.Read, true);

            var result = await CreateController().DetailsJson(12345);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DetailsJsonShouldReturnApplicationWithGivenId()
        {
            UserHasAccessToOrganization(AccessLevel.Read, true);

            var applicationId = 12345;
            _applicationServiceMock.Setup(m => m.GetAsync(applicationId)).ReturnsAsync(new Application()
            {
                Id = applicationId
            });
            
            var result = await CreateController().DetailsJson(applicationId);

            var viewResult = Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task DetailsJsonShouldReturnForbiddenWhenUserDontHaveAccess()
        {
            UserHasAccessToOrganization(AccessLevel.Read, false);
            var result = await CreateController().DetailsJson(1);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task CreateShouldReturnForbiddenWhenUserDontHaveAccess()
        {
            UserHasAccessToOrganization(AccessLevel.Write, false);
            var result = await CreateController().Create();
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task CreateWithModelShouldReturnForbiddenWhenUserDontHaveAccess()
        {
            UserHasAccessToOrganization(AccessLevel.Write, false);
            var result = await CreateController().Create(new ApplicationViewModel());
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnForbiddenWhenUserDontHaveAccess()
        {
            UserHasAccessToApplication(ApplicationId, AccessLevel.Write, false);
            var result = await CreateController().Edit(ApplicationId);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task DeleteShouldReturnForbiddenWhenUserDontHaveAccess()
        {
            UserHasAccessToApplication(ApplicationId, AccessLevel.Write, false);
            var result = await CreateController().Delete(ApplicationId);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmedShouldReturnForbiddenWhenUserDontHaveAccess()
        {
            UserHasAccessToApplication(ApplicationId, AccessLevel.Write, false);
            var result = await CreateController().DeleteConfirmed(ApplicationId);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task SubmitAppRegistryShouldReturnForbiddenWhenUserDontHaveAccess()
        {
            UserHasAccessToApplication(ApplicationId, AccessLevel.Write, false);
            var result = await CreateController().SubmitAppRegistry(ApplicationId);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task SubmitAppRegistryConfirmShouldReturnForbiddenWhenUserDontHaveAccess()
        {
            UserHasAccessToApplication(ApplicationId, AccessLevel.Write, false);
            var result = await CreateController().SubmitAppRegistryConfirm(ApplicationId);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnModelWhenUserHaveAccess()
        {
            UserHasAccessToApplication(ApplicationId, AccessLevel.Write, true);
            var result = await CreateController().Edit(ApplicationId);
            Assert.IsType<ViewResult>(result);
        }

        private void UserHasAccessToApplication(int applicationId, AccessLevel accessLevel, bool value)
        {
            var application = new Application();
            _applicationServiceMock.Setup(s => s.GetAsync(applicationId)).ReturnsAsync(application);
            _securityServiceMock.Setup(s => s.CurrrentUserHasAccessToApplication(application, accessLevel)).Returns(value);
        }

        private void UserHasAccessToOrganization(AccessLevel accessLevel, bool value)
        {
            _securityServiceMock.Setup(s => s.CurrrentUserHasAccessToOrganization(OrganizationId, accessLevel)).Returns(value);
        }

        private ApplicationsController CreateController()
        {
            return new ApplicationsController(_securityServiceMock.Object, _applicationServiceMock.Object, _userServiceMock.Object,
              _sectorServiceMock.Object, 
              new Mock<INationalComponentService>().Object,
              new Mock<IAppRegistry>().Object,
              _vendorServiceMock.Object);
              
        }
    }
}
