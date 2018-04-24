namespace Arkitektum.Orden.Models
{
    public class CommonApplicationVersionStandard
    {
        public int CommonApplicationVersionId { get;set; }
        public int StandardId { get;set; }
        public virtual Standard Standard { get; set; }
    }
}