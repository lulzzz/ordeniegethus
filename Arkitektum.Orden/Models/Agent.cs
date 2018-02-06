using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class Agent
    {
        public static int Id { get; set; }

        ///<summary>
        /// Navn på enheten. Denne egenskapen kan gjentas for
        ///  ulike versjoner av navnet (som navnet på forskjellige språk)
        /// </summary>
        public  static List<String> Names { get; set; }

        ///<summary>
        /// Refererer til type for enheten som gjør katalogen eller datasett 
        /// tilgjengelig
        /// </summary>
        public static String Type { get; set; }

        ///<summary>
        /// Egenskap som angir organisasjonens identifikasjonsnummer, for 
        /// eksempel i henhold til Enhetsregisterets organisasjonsnummer
        /// </summary>
        public static String OrganizationNumber { get; set; }///Identifier ????

        public static Dataset Dataset { get; set; }

        public static Catalog Catalog { get; set; }
    }
}
