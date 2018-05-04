namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Join table between application and super user
    /// </summary>
    public class ApplicationSuperUser
    {
        public int ApplicationId { get; set; }
        public int SuperUserId { get; set; }

        public virtual SuperUser SuperUser { get; set; }
    }
}