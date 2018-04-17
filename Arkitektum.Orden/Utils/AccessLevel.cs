using Arkitektum.Orden.Models;
using System.Collections.Generic;
using System.Linq;

namespace Arkitektum.Orden.Utils
{
    public class AccessLevel
    {
        public static AccessLevel Read = new AccessLevel("Read", Roles.Reader, Roles.User, Roles.OrganizationAdmin, Roles.Admin);
        public static AccessLevel Write = new AccessLevel("Write", Roles.User, Roles.OrganizationAdmin, Roles.Admin);
        public static AccessLevel OrganizationAdmin = new AccessLevel("OrganizationAdmin", Roles.OrganizationAdmin, Roles.Admin);

        public string Name {get; }
        public IEnumerable<string> RequiredRoles {get; private set;}
        private AccessLevel(string name, params string[] roles)
        {
            Name = name;
            RequiredRoles = roles.ToList();
        }
    }
}
