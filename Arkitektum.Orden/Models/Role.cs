using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public static class Roles
    {
        private static readonly string[] ListOfRoles = {"Admin", "OrganizationAdmin", "User", "Reader"};

        public static string Admin => ListOfRoles[0];
        public static string OrganizationAdmin => ListOfRoles[1];
        public static string User => ListOfRoles[2];
        public static string Reader => ListOfRoles[3];

        public static IEnumerable<string> All => ListOfRoles;
    }
}