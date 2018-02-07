using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class DcatCatalog
    {
        public static int Id { get; set; }

        ///<summary>
        /// Inneholder navnet på katalogen. Egenskapen kan bli gjentatt
        ///  for parallelle språkversjoner av navnet.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Fritekst-beskrivelse av innholdet i katalogen. Egenskapen
        ///  kan bli gjentatt for parallelle språkversjoner av beskrivelsen.
        /// </summary>
        public List<String> Descriptions { get; set; }
        
        /// <summary>
        /// Nettside som fungerer som hovedside for katalogen
        /// </summary>
        public String Homepage { get; set; }

        ///<summary>
        /// Viser til lisens for datakatalogen som beskriver hvordan den 
        /// kan viderebrukes.
        /// </summary>
        public String License { get; set; }

        /// <summary>
        /// Dato for formell utgivelse (publisering) av katalogen.
        /// </summary>
        public DateTime? Issued { get; set; }

        ///<summary>
        /// Refererer til et kunnskapsorganiseringssystem (KOS) som er brukt
        /// for å klassifisere katalogens datasett
        /// </summary>
        public List<String> ThemeTaxonomy { get; set; }

        ///<summary>
        /// Dato for siste oppdatering/endring av katalogen
        /// </summary>
        public DateTime? Modified { get; set; } 

        public Organization Organization { get; set; }
        
    }
}
