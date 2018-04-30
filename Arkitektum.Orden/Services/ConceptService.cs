using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;

namespace Arkitektum.Orden.Services
{
    public interface IConceptService
    {
        List<DcatConcept> GetConcepts();
    }
    public class ConceptService : IConceptService
    {
  
        public List<DcatConcept> GetConcepts()
        {
            List<DcatConcept> concepts = new List<DcatConcept>
            {
                new DcatConcept("AGRI", "Jordbruk, fiskeri, skogbruk og mat"),
                new DcatConcept("EDUC", "Utdanning, kultur og sport"),
                new DcatConcept("ENVI","Miljø"),
                new DcatConcept("ENER","Energi"),
                new DcatConcept("TRAN","Transport"),
                new DcatConcept("TECH","Vitenskap og teknologi"),
                new DcatConcept("ECON","Økonomi og finans"),
                new DcatConcept("SOCI","Befolkning og samfunn"),
                new DcatConcept("HEAL","Helse"),
                new DcatConcept("GOVE","Forvaltning og offentlig sektor"),
                new DcatConcept("REGI","Regioner og byer"),
                new DcatConcept("JUST","Justis, rettssystem og allmenn sikkerhet"),
                new DcatConcept("INTR","Internasjonale temaer"),
            };

            return concepts;
        }
    }

    public class DcatConcept
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public DcatConcept(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }


    }

