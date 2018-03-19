using System;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Arkitektum.Orden.Test
{
    /// <summary>
    /// Builder utility for creating mocks of ISecurityService.
    /// </summary>
    public class SecurityServiceMock
    {
        private readonly Mock<ISecurityService> _mock;

        public SecurityServiceMock()
        {
            _mock = new Mock<ISecurityService>();
        }

        public SecurityServiceMock ReturnCurrentOrganizationWithId(int id)
        {
            var currentOrganization = new SimpleOrganization()
            {
                Id = id
            };
            _mock.Setup(m => m.GetCurrentOrganization(It.IsAny<HttpContext>())).Returns(currentOrganization);
            return this;
        }

        public Mock<ISecurityService> Mock()
        {
            return _mock;
        }
    }
}