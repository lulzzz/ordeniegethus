using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class Agent
    {
        public int Id { get; set; }

        ///<summary>
        /// Navn på enheten. Denne egenskapen kan gjentas for
        ///  ulike versjoner av navnet (som navnet på forskjellige språk)
        /// </summary>
        public  List<String> Names { get; set; }

        ///<summary>
        /// Refererer til type for enheten som gjør katalogen eller datasett 
        /// tilgjengelig
        /// </summary>
        public String Type { get; set; }

        ///<summary>
        /// Egenskap som angir organisasjonens identifikasjonsnummer, for 
        /// eksempel i henhold til Enhetsregisterets organisasjonsnummer
        /// </summary>
        public String OrganizationNumber { get; set; }///Identifier ????

        public Dataset Dataset { get; set; }

        public Catalog Catalog { get; set; }
    }
}
