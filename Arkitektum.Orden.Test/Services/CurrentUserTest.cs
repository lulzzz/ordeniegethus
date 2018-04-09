using System.Collections.Generic;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using FluentAssertions;
using Xunit;

namespace Arkitektum.Orden.Test.Services
{
    public class CurrentUserTest
    {
        private const int OrganizationId = 1;

        [Fact]
        public void CurrentUserIsOrganizationAdminShouldBeFalseWhenNotRoleNotPresent()
        {
            var user = new CurrentUser(null, UserMemberOfOrganization(OrganizationId, Roles.User));

            user.IsOrganizationAdminForOrganization(OrganizationWithId(OrganizationId)).Should().BeFalse();
        }

        [Fact]
        public void CurrentUserIsOrganizationAdminShouldBeTrueWhenMembershipMatches()
        {
            var user = new CurrentUser(null, UserMemberOfOrganization(OrganizationId, Roles.OrganizationAdmin));

            user.IsOrganizationAdminForOrganization(OrganizationWithId(OrganizationId)).Should().BeTrue();
        }

        [Fact]
        public void CurrentUserIsOrganizationAdminShouldBeTrueWhenUserHasMultipleRolesInSameOrganization()
        {
            var membership1 = new OrganizationApplicationUser
            {
                OrganizationId = OrganizationId,
                Role = Roles.User
            };
            
            var membership2 = new OrganizationApplicationUser
            {
                OrganizationId = OrganizationId,
                Role = Roles.OrganizationAdmin
            };

            var user = new CurrentUser(null, new ApplicationUser()
            {
                Organizations = new List<OrganizationApplicationUser> {membership1, membership2}
            });


            user.IsOrganizationAdminForOrganization(OrganizationWithId(OrganizationId)).Should().BeTrue();
        }



        private SimpleOrganization OrganizationWithId(int organizationId)
        {
            return new SimpleOrganization(new List<Organization> {new Organization {Id = organizationId}},
                organizationId);
        }

        private ApplicationUser UserMemberOfOrganization(int organizationId, string role)
        {
            return new ApplicationUser
            {
                Organizations = new List<OrganizationApplicationUser>
                {
                    new OrganizationApplicationUser
                    {
                        OrganizationId = organizationId,
                        Role = role
                    }
                }
            };
        }
    }
}