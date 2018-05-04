using System.Collections.Generic;
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
    public class ApplicationSuperUsersControllerTest
    {
        private readonly Mock<ISuperUsersService> _superUsersServiceMock = new Mock<ISuperUsersService>();
        private readonly SecurityServiceMock _securityServiceMock = new SecurityServiceMock();

        private const int OrganizationId = 9;
        private const int ApplicationId = 7;
        private const int SuperUserId = 13;
        private readonly SuperUserViewModel _superUser = new SuperUserViewModel() {Id = SuperUserId};
        
        public ApplicationSuperUsersControllerTest()
        {
            _securityServiceMock.ReturnCurrentOrganizationWithId(OrganizationId);
        }
        
        [Fact]
        public async void GetSuperUsersForApplicationShouldReturnForbiddenWhenUserDoesNotHaveReadAccess()
        {
            var result = await Controller().GetSuperUsersForApplication(ApplicationId);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async void GetSuperUsersForApplicationShouldReturnSuperUsers()
        {
            SetAccessLevelOnApplication(AccessLevel.Read);

            _superUsersServiceMock.Setup(s => s.GetSuperUsersForApplication(ApplicationId))
                .ReturnsAsync(new List<SuperUser>());

            var result = await Controller().GetSuperUsersForApplication(ApplicationId);
            Assert.IsType<JsonResult>(result);
        }
        
        [Fact]
        public async void AddSuperUserToApplicationShouldReturnForbiddenWhenUserDoesNotHaveWriteAccess()
        {
            SetAccessLevelOnApplication(AccessLevel.Read);

            var result = await Controller().AddSuperUserToApplication(ApplicationId, _superUser);
            Assert.IsType<ForbidResult>(result);
        }
        
        [Fact]
        public async void ShouldAddSuperUserToApplication()
        {
            SetAccessLevelOnApplication(AccessLevel.Write);

            var result = await Controller().AddSuperUserToApplication(ApplicationId, _superUser);
            Assert.IsType<NoContentResult>(result);
            _superUsersServiceMock.Verify(s => s.AddSuperUserToApplication(ApplicationId, SuperUserId));
        }
        
        [Fact]
        public async void RemoveSuperUserFromApplicationShouldReturnForbiddenWhenUserDoesNotHaveWriteAccess()
        {
            SetAccessLevelOnApplication(AccessLevel.Read);

            var result = await Controller().AddSuperUserToApplication(ApplicationId, _superUser);
            Assert.IsType<ForbidResult>(result);
        }
        
        [Fact]
        public async void ShouldRemoveSuperUserFromApplication()
        {
            SetAccessLevelOnApplication(AccessLevel.Write);

            var result = await Controller().RemoveSuperUserFromApplication(ApplicationId, SuperUserId);
            
            Assert.IsType<NoContentResult>(result);
            
            _superUsersServiceMock.Verify(s => s.RemoveSuperUserFromApplication(ApplicationId, SuperUserId));
        }
        
        private void SetAccessLevelOnApplication(AccessLevel accessLevel)
        {
            _securityServiceMock.SetAccessToApplication(ApplicationId, accessLevel, OrganizationId);
        }

        private ApplicationSuperUsersController Controller()
        {
            var controller = new ApplicationSuperUsersController(_superUsersServiceMock.Object, _securityServiceMock.Mock().Object);
            return controller;
        }
    }
}