using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nest;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class DatasetMetadataViewModel : Mapper<Dataset, DatasetMetadataViewModel>
    {
        public int Id { get; set; }

        public string DatasetName { get; set; }
        public string Description { get; set; }
        public List<string> Concepts { get; set; } = new List<string>();
        public MultiSelectList AvailableConcepts { get; set; }
        public List<string> Identifiers { get; set; } = new List<string>();
        public List<string> ContactPoints { get; set; } = new List<string>();
        public List<string> Distributions { get; set; } = new List<string>();
        public List<string> Keywords { get; set; } = new List<string>();
        public List<string> AccessRightComments { get; set; } = new List<string>();
        public List<string> Subjects { get; set; } = new List<string>();


        public override IEnumerable<DatasetMetadataViewModel> MapToEnumerable(IEnumerable<Dataset> inputs)
        {
            List<DatasetMetadataViewModel> models = new List<DatasetMetadataViewModel>();

            foreach (var input in inputs)
            {
                models.Add(Map(input));
            }

            return models;
        }

        public override DatasetMetadataViewModel Map(Dataset input)
        {
            return new DatasetMetadataViewModel
            {
                DatasetName = input.Name,
                Description = input.Description,
                Keywords = input.KeywordsAsList,
                Identifiers = input.IdentifiersAsList,
                ContactPoints = input.ContactPointsAsList,
                Distributions = input.DistributionsAsList,
                AccessRightComments = input.AccessRightCommentsAsList,
                Subjects = input.SubjectsAsList,
                Concepts = input.ConceptsAsList
            };
        }

        public Dataset Map(DatasetMetadataViewModel input)
        {
            return new Dataset
            {
                Id = input.Id,
                Description = input.Description,
                KeywordsAsList = input.Keywords,
                ConceptsAsList = input.Concepts,
                IdentifiersAsList = input.Identifiers,
                ContactPointsAsList = input.ContactPoints,
                DistributionsAsList = input.Distributions,
                AccessRightCommentsAsList = input.AccessRightComments,
                SubjectsAsList = input.Subjects
            };
        }

    }
}
