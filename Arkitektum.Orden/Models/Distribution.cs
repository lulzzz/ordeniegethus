using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class Distribution
    {
        public int Id { get; set; }

        /// <summary>
        /// En URL som gir tilgang til en distribusjon av datasettet. 
        /// Ressursen det pekes til kan gi informasjon om hvordan en 
        /// kan få tilgang til i datasettet
        /// </summary>
        public virtual List<ResourceLink> Resources {get; set;}

        ///<summary>
        /// Fritekstbeskrivelse av distribusjonen. 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Opplysing om til distribusjonens filformat. Kan gjentas
        /// for API-er og sluttbrukerapplikasjoner som leverer data 
        /// i flere formater.
        /// </summary>
        public virtual List<Format> Formats { get; set; }
     // TODO FIX THIS
       

        /// <summary>
        /// Referanse til lisensen distribusjonen er gjort tilgjengelig 
        /// under. Bør oppgis som URI
        /// </summary>
        public virtual LicenseDocument License { get; set; } //sjekke hvorsdan data ser ut for eventuelle endringer

        public virtual Dataset Dataset { get; set; }

    }
}
