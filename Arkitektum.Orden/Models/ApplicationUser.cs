using Microsoft.AspNetCore.Identity;

namespace Arkitektum.Orden.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Person Person { get; set; }
    }
}