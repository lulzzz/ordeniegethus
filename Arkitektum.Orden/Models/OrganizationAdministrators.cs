namespace Arkitektum.Orden.Models
{
    public class OrganizationAdministrators
    {
        public int OrganizationId { get; set; }

        public string ApplicationUserId { get; set; }

        public Organization Organization { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}