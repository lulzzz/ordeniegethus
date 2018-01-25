namespace Arkitektum.Orden.Models
{
    public class LawReference
    {
        public int Id { get; set; }

        /// <summary>
        ///     Lovhjemmel
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Lovhjemmel webadresse
        /// </summary>
        public string Url { get; set; }
    }
}