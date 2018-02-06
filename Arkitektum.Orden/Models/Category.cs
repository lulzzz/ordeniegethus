using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class Category
    {
        public static int Id { get; set; }

        ///<summary>
        /// Foretrukket tittel for kategorien. Kan gjentas 
        /// for parallelle språkversjoner av etiketten.
        /// </summary>
        public static List<String> PreferableLables { get; set; }
    }
}
