namespace Arkitektum.Orden.Models
{
    public class LicenseDocument
    {
        public int Id { get; set; }

        /// <summary>
        ///     Refererer til en type lisens, f.eks "åpen lisens" eller "royalties kreves".
        /// </summary>
        public string LicenseType { get; set; }
    }
}