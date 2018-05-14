using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Arkitektum.Orden.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Nest;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class DatasetMetadataViewModel : Mapper<Dataset, DatasetMetadataViewModel>
    {
        public int Id { get; set; }

        public string DatasetName { get; set; }
        public List<string> Descriptions { get; set; }
        public List<string> Concepts { get; set; }
        public MultiSelectList AvailableConcepts { get; set; }
        public List<string> Identifiers { get; set; }
        public List<string> ContactPoints { get; set; }
        public List<string> Distributions { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> AccessRightComments { get; set; }
        public List<string> Subjects { get; set; }

        public string ChangedFieldName { get; set; }


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
                Descriptions = input.DescriptionsAsList,
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

                DescriptionsAsList = input.Descriptions,
                KeywordsAsList = input.Keywords,
                ConceptsAsList = input.Concepts,
                IdentifiersAsList = input.Identifiers,
                ContactPointsAsList = input.ContactPoints,
                DistributionsAsList = input.Distributions,
                AccessRightCommentsAsList = input.AccessRightComments,
                SubjectsAsList = input.Subjects
            };


            //    return new Dataset
            //    {
            //        DescriptionsAsList = HasFieldValueOrEmpty(input.Descriptions) ? input.Descriptions : null,
            //        KeywordsAsList = HasFieldValueOrEmpty(input.Keywords) ? input.Keywords : null,
            //        ConceptsAsList = HasFieldValueOrEmpty(input.Concepts) ? input.Concepts : null,
            //        IdentifiersAsList = HasFieldValueOrEmpty(input.Identifiers) ? input.Identifiers : null,
            //        DistributionsAsList = HasFieldValueOrEmpty(input.Distributions) ? input.Distributions : null,
            //        AccessRightCommentsAsList = HasFieldValueOrEmpty(input.AccessRightComments) ? input.AccessRightComments : null,
            //        SubjectsAsList = HasFieldValueOrEmpty(input.Subjects) ? input.Subjects : null,

            //    };

        }

        private bool HasFieldValueOrEmpty(List<string> input)
        {
            return input.Any();
        }


        public List<string> FindOutChangedFieldName(DatasetMetadataViewModel model)
        {
            List<string> fieldNames = new List<string>(); 

    
            if (model.Keywords != null)
            {
                fieldNames.Add(nameof(model.Keywords));
            }

            if (model.Descriptions != null)
            {
                fieldNames.Add(nameof(model.Descriptions));
            }

            if (model.Concepts != null)
            {
                fieldNames.Add(nameof(model.Concepts));
            }

            if (model.Identifiers != null)
            {
                fieldNames.Add(nameof(model.Identifiers));
            }

            if (model.AccessRightComments != null)
            {
                fieldNames.Add(nameof(model.AccessRightComments));
            }

            if (model.Subjects != null)
            {
                fieldNames.Add(nameof(model.Subjects));
            }

            if (model.Distributions != null)
            {
                fieldNames.Add(nameof(model.Distributions));
            }

            if (model.ContactPoints != null)
            {
                fieldNames.Add(nameof(model.ContactPoints));
            }

            return fieldNames;


        }
    }
}
