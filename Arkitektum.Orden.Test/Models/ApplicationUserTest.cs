using System.Collections.Generic;
using Arkitektum.Orden.Models;
using FluentAssertions;
using Xunit;

namespace Arkitektum.Orden.Test.Models
{
    public class ApplicationUserTest
    {
        [Fact]
        public void ShouldReturnFalseWhenUserDoesNotHaveAccessToOrganization()
        {
            var user = new ApplicationUser()
            {
                Organizations = new List<OrganizationApplicationUser>()
            };

            user.HasAccessToOrganization(1).Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnFalseWhenOrganizationsIsNull()
        {
            var user = new ApplicationUser()
            {
                Organizations = null
            };

            user.HasAccessToOrganization(1).Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnTrueWhenUserHaveAccessToOrganization()
        {
            var user = new ApplicationUser()
            {
                Organizations = new List<OrganizationApplicationUser>()
                {
                    new OrganizationApplicationUser() {OrganizationId = 1}
                }
            };

            user.HasAccessToOrganization(1).Should().BeTrue();
        }

    }
}