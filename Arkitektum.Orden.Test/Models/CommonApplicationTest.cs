using System.Collections.Generic;
using System.Linq;
using Arkitektum.Orden.Models;
using FluentAssertions;
using Xunit;

namespace Arkitektum.Orden.Test.Models
{
    public class CommonApplicationTest
    {
        private const int OrganizationId = 10;
        private const string VersionNumber = "1.0";
        private const string Name = "testapplikasjon";
        private const int VendorId = 2;
        private const int CommonApplicationId = 42;

        [Fact]
        public void ShouldCreateApplicationWithoutDataset()
        {
            var common = new CommonApplication
            {
                Name = Name,
                VendorId = VendorId,
                Id = CommonApplicationId
            };

            var app = common.CreateApplicationForOrganization(OrganizationId, VersionNumber);
            app.Name.Should().Be(Name);
            app.VendorId.Should().Be(VendorId);
            app.OrganizationId.Should().Be(OrganizationId);
            app.CreatedFromCommonApplicationId.Should().Be(CommonApplicationId);
        }

        [Fact]
        public void ShouldCreateApplicationWithDatasetWithoutFields()
        {
            var datasetName = "DatasetName";
            var datasetDescription = "DatasetDescription";
            var datasetPurpose = "DatasetPurpose";

            var common = new CommonApplication
            {
                Name = Name,
                VendorId = VendorId,
                CommonDatasets = new List<CommonDataset>
                {
                    new CommonDataset()
                    {
                        Name = datasetName,
                        Description =datasetDescription,
                        Purpose = datasetPurpose,
                        HasPersonalData = true,
                        HasSensitivePersonalData = true,
                        HasMasterData = true
                    }
                }
            };

            var app = common.CreateApplicationForOrganization(OrganizationId, VersionNumber);
            app.Name.Should().Be(Name);
            app.VendorId.Should().Be(VendorId);
            app.ApplicationDatasets.Count.Should().Be(1);
            app.ApplicationDatasets[0].Dataset.Name.Should().Be(datasetName);
            app.ApplicationDatasets[0].Dataset.Description.Should().Be(datasetDescription);
            app.ApplicationDatasets[0].Dataset.Purpose.Should().Be(datasetPurpose);
            app.ApplicationDatasets[0].Dataset.HasPersonalData.Should().BeTrue();
            app.ApplicationDatasets[0].Dataset.HasSensitivePersonalData.Should().BeTrue();
            app.ApplicationDatasets[0].Dataset.HasMasterData.Should().BeTrue();
            app.ApplicationDatasets[0].Dataset.OrganizationId.Should().Be(OrganizationId);
        }

        [Fact]
        public void ShouldCreateApplicationWithNationalComponentsFromCorrectVersion()
        {
            int nationalComponentMatrikkel = 42;
            int nationalComponentIdporten = 47;

            var nationalComponentOther = 21;

            var common = new CommonApplication
            {
                Versions = new List<CommonApplicationVersion>()
                {
                    new CommonApplicationVersion()
                    {
                        VersionNumber = VersionNumber,
                        SupportedNationalComponents = new List<CommonApplicationVersionNationalComponent>()
                        {
                            new CommonApplicationVersionNationalComponent()
                            {
                                NationalComponentId = nationalComponentMatrikkel
                            },
                            new CommonApplicationVersionNationalComponent()
                            {
                                NationalComponentId = nationalComponentIdporten
                            }
                        }
                    },
                    new CommonApplicationVersion()
                    {
                        VersionNumber = "0.9-beta",
                        SupportedNationalComponents = new List<CommonApplicationVersionNationalComponent>()
                        {
                            new CommonApplicationVersionNationalComponent()
                            {
                                NationalComponentId = nationalComponentOther
                            },
                        }
                    }
                }

                
            };

            var app = common.CreateApplicationForOrganization(OrganizationId, VersionNumber);

            app.Version.Should().Be(VersionNumber);

            app.ApplicationNationalComponent
                .Any(anc => anc.NationalComponentId == nationalComponentIdporten)
                .Should().BeTrue();

            app.ApplicationNationalComponent
                .Any(anc => anc.NationalComponentId == nationalComponentMatrikkel)
                .Should().BeTrue();

            app.ApplicationNationalComponent
                .Any(anc => anc.NationalComponentId == nationalComponentOther)
                .Should().BeFalse();
        }
    }
}