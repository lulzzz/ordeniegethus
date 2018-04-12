using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class DatasetFieldViewModel
    {
        public int Id { get;set;}
        public string Name { get;set;}
        public string Description { get;set;}
        public bool IsPersonalData { get; set; }
        public bool IsSensitivePersonalData { get; set; }
    }
}
