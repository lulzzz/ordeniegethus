using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class Catalog
    {
        public static int Id { get; set; }

        ///<summary>
        /// Inneholder navnet på katalogen. Egenskapen kan bli gjentatt
        ///  for parallelle språkversjoner av navnet.
        /// </summary>
        public static List<String> Title { get; set; }

        /// <summary>
        /// Fritekst-beskrivelse av innholdet i katalogen. Egenskapen
        ///  kan bli gjentatt for parallelle språkversjoner av beskrivelsen.
        /// </summary>
        public static List<String> Descriptions { get; set; }

        ///<summary>
        /// Refererer til en enhet (organisasjon) som er ansvarlig for 
        /// å gjøre katalogen tilgjengelig. Bør være autoritativ URI 
        /// for enhet, sekundært organisasjonsnummer.
        /// </summary>
        public static Publisher Publisher { get; set; }

        /// <summary>
        /// Kobler katalogen til datasett som er en del av katalogen
        /// </summary>
        public static List<Dataset> Datasets { get; set; }

        /// <summary>
        /// Nettside som fungerer som hovedside for katalogen
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        /// Viser til et språk som brukes i tekstlige metadata som beskriver
        /// titler, beskrivelser, osv av datasettene i katalogen. Egenskapen
        /// kan gjentas hvis metadata er gitt i flere språk.
        /// </summary>
        public List<String> Language { get; set; }

        ///<summary>
        /// Viser til lisens for datakatalogen som beskriver hvordan den 
        /// kan viderebrukes.
        /// </summary>
        public static String License { get; set; }

        /// <summary>
        /// Dato for formell utgivelse (publisering) av katalogen.
        /// </summary>
        public static DateTime ? Issued { get; set; }

        ///<summary>
        /// Refererer til et kunnskapsorganiseringssystem (KOS) som er brukt
        /// for å klassifisere katalogens datasett
        /// </summary>
        public static List<String> ThemeTaxonomy { get; set; }

        ///<summary>
        /// Dato for siste oppdatering/endring av katalogen
        /// </summary>
        public static DateTime ? Modified { get; set; } 
        
        
    }
}
