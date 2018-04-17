using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Arkitektum.Orden.Test.Services
{
    public class SecurityServiceTest
    {
        private const string UserId = "42";
        private const int OrganizationId = 2;

        private Mock<IUserService> _userServiceMock; 
        private Mock<FakeUserManager> _userManagerMock;

        public SecurityServiceTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _userManagerMock = new Mock<FakeUserManager>();
        }

        [Fact]
        public void AdminCanDelegateAllRoles()
        {
            var delegateableRoles = CreateServiceForUserWithSystemRole(Roles.Admin).GetDelegateableRoles();
            foreach (var role in Roles.All)
            {
                delegateableRoles.Contains(role).Should().BeTrue($"role [{role}] should be delegateable");
            }
        }

        [Fact]
        public void RegularUserCannotDelegateAnyRoles()
        {
            var delegateableRoles = CreateServiceForUserWithSystemRole(Roles.User).GetDelegateableRoles();
            delegateableRoles.Should().BeEmpty();
        }

        [Fact]
        public void ReaderUserCannotDelegateAnyRoles()
        {
            var delegateableRoles = CreateServiceForUserWithSystemRole(Roles.Reader).GetDelegateableRoles();
            delegateableRoles.Should().BeEmpty();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationReadShouldBeOkWithReaderRole()
        {
            ApplicationAccess(Roles.Reader, AccessLevel.Read).Should().BeTrue();
        }
        
        [Fact]
        public void CurrrentUserHasAccessToApplicationReadShouldBeOkWithUserRole()
        {
            ApplicationAccess(Roles.User, AccessLevel.Read).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationReadShouldBeOkWithOrganizationAdminRole()
        {
            ApplicationAccess(Roles.OrganizationAdmin, AccessLevel.Read).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationReadShouldBeOkWithAdminRole()
        {
            ApplicationAccessAsSystemAdmin(AccessLevel.Read).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationWriteShouldBeFalseWithReaderRole()
        {
            ApplicationAccess(Roles.Reader, AccessLevel.Write).Should().BeFalse();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationWriteShouldBeOkWithUserRole()
        {
            ApplicationAccess(Roles.User, AccessLevel.Write).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationWriteShouldBeOkWithOrganizationAdminRole()
        {
            ApplicationAccess(Roles.OrganizationAdmin, AccessLevel.Write).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationWriteShouldBeOkWithAdminRole()
        {
            ApplicationAccessAsSystemAdmin(AccessLevel.Write).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationOrganizationAdminShouldBeFalseWithReaderRole()
        {
            ApplicationAccess(Roles.Reader, AccessLevel.OrganizationAdmin).Should().BeFalse();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationOrganizationAdminShouldBeFalseWithUserRole()
        {
            ApplicationAccess(Roles.User, AccessLevel.OrganizationAdmin).Should().BeFalse();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationOrganizationAdminShouldBeOkWithOrganizationAdminRole()
        {
            ApplicationAccess(Roles.OrganizationAdmin, AccessLevel.OrganizationAdmin).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToApplicationOrganizationAdminShouldBeOkWithAdminRole()
        {
            ApplicationAccessAsSystemAdmin(AccessLevel.OrganizationAdmin).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationReadShouldBeOkWithReaderRole()
        {
            OrganizationAccess(Roles.Reader, AccessLevel.Read).Should().BeTrue();
        }
        
        [Fact]
        public void CurrrentUserHasAccessToOrganizationReadShouldBeOkWithUserRole()
        {
            OrganizationAccess(Roles.User, AccessLevel.Read).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationReadShouldBeOkWithOrganizationAdminRole()
        {
            OrganizationAccess(Roles.OrganizationAdmin, AccessLevel.Read).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationReadShouldBeOkWithAdminRole()
        {
            OrganizationAccessAsSystemAdmin(AccessLevel.Read).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationWriteShouldBeFalseWithReaderRole()
        {
            OrganizationAccess(Roles.Reader, AccessLevel.Write).Should().BeFalse();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationWriteShouldBeOkWithUserRole()
        {
            OrganizationAccess(Roles.User, AccessLevel.Write).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationWriteShouldBeOkWithOrganizationAdminRole()
        {
            OrganizationAccess(Roles.OrganizationAdmin, AccessLevel.Write).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationWriteShouldBeOkWithAdminRole()
        {
            OrganizationAccessAsSystemAdmin(AccessLevel.Write).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationOrganizationAdminShouldBeFalseWithReaderRole()
        {
            OrganizationAccess(Roles.Reader, AccessLevel.OrganizationAdmin).Should().BeFalse();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationOrganizationAdminShouldBeFalseWithUserRole()
        {
            OrganizationAccess(Roles.User, AccessLevel.OrganizationAdmin).Should().BeFalse();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationOrganizationAdminShouldBeOkWithOrganizationAdminRole()
        {
            OrganizationAccess(Roles.OrganizationAdmin, AccessLevel.OrganizationAdmin).Should().BeTrue();
        }

        [Fact]
        public void CurrrentUserHasAccessToOrganizationOrganizationAdminShouldBeOkWithAdminRole()
        {
            OrganizationAccessAsSystemAdmin(AccessLevel.OrganizationAdmin).Should().BeTrue();
        }

        private bool OrganizationAccess(string userRole, AccessLevel accessLevel)
        {
            var service = SetupUserWithRoleOnOrganization(userRole, OrganizationId);
            return service.CurrrentUserHasAccessToOrganization(OrganizationId, accessLevel);
        }

        private bool OrganizationAccessAsSystemAdmin(AccessLevel accessLevel)
        {
            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(UserId);
            _userServiceMock.Setup(u => u.Get(UserId)).ReturnsAsync(new ApplicationUser());
            var service = CreateServiceForUserWithSystemRole(Roles.Admin);
            return service.CurrrentUserHasAccessToOrganization(OrganizationId, accessLevel);
        }

        private bool ApplicationAccess(string userRole, AccessLevel accessLevel)
        {
            var service = SetupUserWithRoleOnOrganization(userRole, OrganizationId);
            return service.CurrrentUserHasAccessToApplication(new Application { OrganizationId = OrganizationId }, accessLevel);
        }

        private bool ApplicationAccessAsSystemAdmin(AccessLevel accessLevel)
        {
            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(UserId);
            _userServiceMock.Setup(u => u.Get(UserId)).ReturnsAsync(new ApplicationUser());
            var service = CreateServiceForUserWithSystemRole(Roles.Admin);
            return service.CurrrentUserHasAccessToApplication(new Application { OrganizationId = OrganizationId }, accessLevel);
        }

        private SecurityService SetupUserWithRoleOnOrganization(string role, int organizationId)
        {
            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(UserId);
            _userServiceMock.Setup(u => u.Get(UserId)).ReturnsAsync(CreateApplicationUserWithMembership(organizationId, role));
            return CreateServiceForUserWithSystemRole(Roles.User);
        }

        private ApplicationUser CreateApplicationUserWithMembership(int organizationId, string role)
        {
            return new ApplicationUser()
            {
                Organizations = new List<OrganizationApplicationUser>()
                {
                    new OrganizationApplicationUser()
                    {
                        OrganizationId = organizationId,
                        Role = role
                    }
                }
            };
        }

        private SecurityService CreateServiceForUserWithSystemRole(string role)
        {
            return new SecurityService(new MockPrincipal("johndoe", role), null, _userServiceMock.Object, _userManagerMock.Object, null);
        }
    }

    /// <summary>
    /// https://stackoverflow.com/a/1781793/725492
    /// </summary>
    public class MockPrincipal : IPrincipal
    {
        private IIdentity _identity;
        private readonly List<string> _roles;

        public MockPrincipal(string username, params string[] roles)
        {
            _identity = new MockIdentity(username);
            _roles = roles.ToList();
        }

        public IIdentity Identity
        {
            get => _identity;
            set => _identity = value;
        }

        public bool IsInRole(string role)
        {
            if (_roles == null)
                return false;
            return _roles.Contains(role);
        }
    }
    public class MockIdentity : IIdentity
    {
        private readonly string _name;

        public MockIdentity(string username)    {
            _name = username;
        }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return !String.IsNullOrEmpty(_name); }
        }

        public string Name
        {
            get { return _name; }
        }
    }

    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<ApplicationUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<ApplicationUser>>().Object,
                  new IUserValidator<ApplicationUser>[0],
                  new IPasswordValidator<ApplicationUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        { }

}
}
