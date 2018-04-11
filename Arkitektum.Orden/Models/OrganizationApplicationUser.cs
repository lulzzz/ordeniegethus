namespace Arkitektum.Orden.Models
{
    public class OrganizationApplicationUser
    {
        public int OrganizationId { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Role { get; set; }
    }
}