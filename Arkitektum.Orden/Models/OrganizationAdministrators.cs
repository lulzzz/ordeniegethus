namespace Arkitektum.Orden.Models
{
    public class OrganizationAdministrators
    {
        public int OrganizationId { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}