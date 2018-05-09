using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Models
{
    public class Agreement
    {
        [DataType(DataType.Date)]
        public DateTime? DateStart { get; set; }
        public string Description { get; set; }
        public string TerminationClauses { get; set; }
        public string ResponsibleRole { get; set; }
        public string DocumentUrl { get; set; }
    }
}