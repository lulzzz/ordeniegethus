using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services;
using FluentAssertions;
using Xunit;

namespace Arkitektum.Orden.Test.Services
{
    public class SecurityServiceTest
    {

        [Fact]
        public void AdminCanDelegateAllRoles()
        {
            var delegateableRoles = CreateServiceForUserWithRoles(Roles.Admin).GetDelegateableRoles();
            foreach (var role in Roles.All)
            {
                delegateableRoles.Contains(role).Should().BeTrue($"role [{role}] should be delegateable");
            }
        }

        [Fact(Skip="Disabled due to refactoring of organization membership - currently invalid tests")]
        public void OrganizationAdminCanDelegateOrganizationAdminRole()
        {
            var delegateableRoles = CreateServiceForUserWithRoles(Roles.OrganizationAdmin).GetDelegateableRoles();
            delegateableRoles.Contains(Roles.OrganizationAdmin).Should().BeTrue();
        }

        [Fact(Skip="Disabled due to refactoring of organization membership - currently invalid tests")]
        public void OrganizationAdminCanDelegateUserRole()
        {
            var delegateableRoles = CreateServiceForUserWithRoles(Roles.OrganizationAdmin).GetDelegateableRoles();
            delegateableRoles.Contains(Roles.User).Should().BeTrue();
        }

        [Fact(Skip="Disabled due to refactoring of organization membership - currently invalid tests")]
        public void OrganizationAdminCanDelegateReaderRole()
        {
            var delegateableRoles = CreateServiceForUserWithRoles(Roles.OrganizationAdmin).GetDelegateableRoles();
            delegateableRoles.Contains(Roles.Reader).Should().BeTrue();
        }

        [Fact]
        public void RegularUserCannotDelegateAnyRoles()
        {
            var delegateableRoles = CreateServiceForUserWithRoles(Roles.User).GetDelegateableRoles();
            delegateableRoles.Should().BeEmpty();
        }

        [Fact]
        public void ReaderUserCannotDelegateAnyRoles()
        {
            var delegateableRoles = CreateServiceForUserWithRoles(Roles.Reader).GetDelegateableRoles();
            delegateableRoles.Should().BeEmpty();
        }

        private static SecurityService CreateServiceForUserWithRoles(string role)
        {
            return new SecurityService(new MockPrincipal("johndoe", role), null, null, null);
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
            get { return _identity; }
            set { this._identity = value; }
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
            get { throw new System.NotImplementedException(); }
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
}
