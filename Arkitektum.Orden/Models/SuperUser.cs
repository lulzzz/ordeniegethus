namespace Arkitektum.Orden.Models
{
    public class SuperUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OrganizationId { get; set; }
    }
}