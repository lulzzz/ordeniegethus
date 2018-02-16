using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public static class Roles
    {
        private static readonly string[] ListOfRoles = {Admin, OrganizationAdmin, User, Reader};

        public const string Admin = "Admin";
        public const string OrganizationAdmin = "OrganizationAdmin";
        public const string User = "User";
        public const string Reader = "Reader";

        public static IEnumerable<string> All => ListOfRoles;

        }
}