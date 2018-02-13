namespace Arkitektum.Orden.Models
{
    public class Agent
    {
        public int Id { get; set; }

        ///<summary>
        /// Navn på enheten.
        /// </summary>
        public string Name { get; set; }

        ///<summary>
        /// Refererer til type for enheten som gjør katalogen eller datasett 
        /// tilgjengelig
        /// </summary>
        public string Type { get; set; }

        ///<summary>
        /// Egenskap som angir organisasjonens identifikasjonsnummer, for 
        /// eksempel i henhold til Enhetsregisterets organisasjonsnummer
        /// </summary>
        public string OrganizationNumber { get; set; }///Identifier ????

        public Dataset Dataset { get; set; }

        public Catalog Catalog { get; set; }
    }
}
