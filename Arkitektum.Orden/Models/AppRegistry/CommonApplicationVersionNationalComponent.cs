namespace Arkitektum.Orden.Models
{
    public class CommonApplicationVersionNationalComponent
    {
        public int CommonApplicationVersionId { get;set; }
        public int NationalComponentId { get; set; }
        public virtual NationalComponent NationalComponent { get; set; }
    }
}