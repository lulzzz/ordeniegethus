using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Controllers;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Arkitektum.Orden.Test.Controllers
{
    public class SectorsControllerTest
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly Mock<ISectorService> _sectorServiceMock;
        private Mock<ISecurityService> _securityServiceMock;
        private readonly Mock<IApplicationSectorService> _applicationSectorServiceMock;
        private readonly Mock<IApplicationService> _applicationServiceMock;

        private int CurrentOrganizationId = 1;

        public SectorsControllerTest()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _sectorServiceMock = new Mock<ISectorService>();
            _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(CurrentOrganizationId).Mock();
            _applicationSectorServiceMock = new Mock<IApplicationSectorService>();
            _applicationServiceMock = new Mock<IApplicationService>();
        }

        [Fact]
        public async Task IndexShouldReturnSectorsForCurrentOrganization()
        {
            var currentOrganizationId = 1;
            _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(currentOrganizationId).Mock();

            var sectors = new List<Sector>()
            {
                new Sector()
                {
                    Name = "Teknisk etat"
                }
            };

            _sectorServiceMock.Setup(ssm => ssm.GetAll()).ReturnsAsync(sectors);

            var controller = CreateController();
            var result = await controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SectorViewModel>>(viewResult.ViewData.Model);

            model.Should().HaveCount(1);
        }

        [Fact]
        public async Task DetailsShouldReturnNotFoundWhenIdIsNull()
        {
            var result = await CreateController().Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DetailsShouldReturnNotFoundWhenNoSectorWithGivenIdExists()
        {
            var currentOrganizationId = 1;
            _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(currentOrganizationId).Mock();

            var result = await CreateController().Details(111111);
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task DetailsShouldReturnSectorWithGivenId()
        {
            var sectorId = 11111;
            var currentOrganizationId = 1;

            _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(currentOrganizationId).Mock();

            _sectorServiceMock.Setup(ssm => ssm.GetSectorWithNoTracking(sectorId)).ReturnsAsync(new Sector()
            {
                Id = sectorId,

            });
            var result = await CreateController().Details(sectorId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SectorViewModel>(viewResult.ViewData.Model);
            model.Id.Should().Be(sectorId);
        }



        [Fact]
        public async Task CreatePostShouldReturnRedirectToActionResultIfNewSectorIsCreated()
        {
            var currentOrganizationId = 1;
            _securityServiceMock = new SecurityServiceMock().ReturnCurrentOrganizationWithId(currentOrganizationId).Mock();

            var sectorToCreate = new Sector()
            {
                Name = "Teknisk etat"
            };

            _sectorServiceMock.Setup(ssm => ssm.Create(sectorToCreate)).ReturnsAsync(sectorToCreate);

            var controller = CreateController();
            var result = await controller.Create(new SectorViewModel().Map(sectorToCreate));

            Assert.IsType<RedirectToActionResult>(result);

        }

        [Fact]
        public async Task EditReturnsNotFoundIfSectorIdIsNull()
        {
            var result = await CreateController().Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnNotFoundWhenNoSectorWithGivenIdExists()
        {
            var result = await CreateController().Edit(1111111111);
            Assert.IsType<NotFoundResult>(result);
        }

 

        private SectorsController CreateController()
        {
            return new SectorsController(_contextMock.Object, _sectorServiceMock.Object, _securityServiceMock.Object, _applicationSectorServiceMock.Object, _applicationServiceMock.Object);
        }

        [Fact]
        public async void GetApplicationSectorsShouldReturnNotFoundWhenIdIsZero()
        {
            var result = await CreateController().GetApplicationSectors(0);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetApplicationSectorsShouldReturnNotFoundWhenApplicationDoesNotExist()
        {
            var result = await CreateController().GetApplicationSectors(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetApplicationSectorsShouldReturnForbiddenWhenUserDoesNotHaveAccessToApplication()
        {
            int applicationId = 42;
            _applicationServiceMock.Setup(a => a.GetAsync(applicationId)).ReturnsAsync(new Application() { Id = applicationId });
            var result = await CreateController().GetApplicationSectors(applicationId);
            Assert.IsType<ForbidResult>(result);
        }
    }
}
