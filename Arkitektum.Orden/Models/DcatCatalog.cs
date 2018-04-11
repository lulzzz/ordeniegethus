using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Arkitektum.Orden.Models
{
    public class DcatCatalog
    {
        
        public int Id { get; set; }

        ///<summary>
        /// Inneholder navnet på katalogen. Egenskapen kan bli gjentatt
        ///  for parallelle språkversjoner av navnet.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Fritekst-beskrivelse av innholdet i katalogen. Egenskapen
        ///  kan bli gjentatt for parallelle språkversjoner av beskrivelsen.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Kobler katalogen til datasett som er en del av katalogen
        /// </summary>
        public virtual List<Dataset> Datasets { get; set; }

        /// <summary>
        /// Nettside som fungerer som hovedside for katalogen
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        ///     Viser til et språk som brukes i tekstlige metadata som beskriver
        ///     titler, beskrivelser, osv av datasettene i katalogen. Egenskapen
        ///     kan gjentas hvis metadata er gitt i flere språk.
        /// </summary>
        public string Language { get; set; }

        ///<summary>
        /// Viser til lisens for datakatalogen som beskriver hvordan den 
        /// kan viderebrukes.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Dato for formell utgivelse (publisering) av katalogen.
        /// </summary>
        public DateTime? Issued { get; set; }

        ///<summary>
        /// Refererer til et kunnskapsorganiseringssystem (KOS) som er brukt
        /// for å klassifisere katalogens datasett
        /// </summary>
        public string ThemesTaxonomy { get; set; }

        ///<summary>
        /// Dato for siste oppdatering/endring av katalogen
        /// </summary>
        public DateTime? Modified { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        
    }
}
