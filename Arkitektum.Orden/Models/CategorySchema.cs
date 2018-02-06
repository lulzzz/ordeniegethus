using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class CategorySchema
    {
        public static int Id { get; set; }

        /// <summary>
        /// Navn på kategori-skjemaet. Kan gjentas for forskjellige 
        /// versjoner av navnet
        /// </summary>
        public List<String> Title { get; set; }

    }
}
