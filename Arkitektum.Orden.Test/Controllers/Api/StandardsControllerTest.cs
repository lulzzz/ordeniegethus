using Arkitektum.Orden.Controllers.Api;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.Api;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Arkitektum.Orden.Test.Controllers.Api
{
    public class StandardsControllerTest
    {
        private SecurityServiceMock _securityServiceMock = new SecurityServiceMock();
        private Mock<IStandardService> _standardServiceMock = new Mock<IStandardService>();
        private Mock<IApplicationService> _applicationServiceMock = new Mock<IApplicationService>();

        private const int ApplicationId = 2;
        private readonly Application _application = new Application();
        
        [Fact]
        public async void ShouldReturnForbiddenWhenUserDoesNotHaveWriteAccessToApplication()
        {
            ApplicationServiceReturnsApplication();
            _securityServiceMock.SetAccessToApplication(_application, AccessLevel.Read);

            var result = await Controller().AddStandardToApplication(new ApplicationStandardViewModel() { ApplicationId = ApplicationId});

            Assert.IsType<ForbidResult>(result);
        }
        
        [Fact]
        public async void ShouldAddStandardToApplicationWhenUserHasWriteAccess()
        {
            ApplicationServiceReturnsApplication();
            _securityServiceMock.SetAccessToApplication(_application, AccessLevel.Write);

            var result = await Controller().AddStandardToApplication(new ApplicationStandardViewModel() { ApplicationId = ApplicationId});

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void ShouldRemoveStandardFromApplicationWhenUserHasWriteAccess()
        {
            ApplicationServiceReturnsApplication();
            _securityServiceMock.SetAccessToApplication(_application, AccessLevel.Write);

            var result = await Controller().RemoveStandardFromApplication(42, ApplicationId);

            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public async void RemoveStandardFromApplicationShouldReturnForbiddenWhenUserHasDoesNotHaveWriteAccessToApplication()
        {
            ApplicationServiceReturnsApplication();
            _securityServiceMock.SetAccessToApplication(_application, AccessLevel.Read);

            var result = await Controller().RemoveStandardFromApplication(42, ApplicationId);

            Assert.IsType<ForbidResult>(result);
        }
        
        private StandardsController Controller()
        {
            var controller = new StandardsController(_securityServiceMock.Mock().Object, _applicationServiceMock.Object,
                _standardServiceMock.Object);
            return controller;
        }

        private void ApplicationServiceReturnsApplication()
        {
            _applicationServiceMock.Setup(a => a.GetAsync(ApplicationId)).ReturnsAsync(_application);
        }
    }
}