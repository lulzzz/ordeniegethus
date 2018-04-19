using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationListViewModel
    {
        public IEnumerable<ApplicationListDetailViewModel> Applications { get; set; }
        public List<SelectListItem> Sectors { get; set; } 

    }
}
