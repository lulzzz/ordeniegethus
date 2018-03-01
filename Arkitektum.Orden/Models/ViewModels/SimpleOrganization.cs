using System.Collections.Generic;
using System.Linq;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class SimpleOrganization
    {
        public List<SimpleOrganization> AvailableOrganizations = new List<SimpleOrganization>();
        public int Id { get; set; }
        public string Name { get; set; }

        public SimpleOrganization()
        {
        }

        public SimpleOrganization(List<OrganizationApplicationUser> organizations, int? organizationId = null)
        {
            Organization selectedOrganization;

            if (organizationId.HasValue)
            {
                selectedOrganization = organizations.First(o => o.OrganizationId == organizationId.Value).Organization;
            }
            else
            {
                selectedOrganization = organizations.First().Organization;
            }

            Id = selectedOrganization.Id;
            Name = selectedOrganization.Name;

            List<int> processedOrganizations = new List<int>();
            processedOrganizations.Add(Id);
            foreach (var org in organizations.Skip(1))
            {
                if (processedOrganizations.Contains(org.OrganizationId))
                    continue;

                AvailableOrganizations.Add(new SimpleOrganization
                {
                    Id = org.Organization.Id,
                    Name = org.Organization.Name
                });
                
                processedOrganizations.Add(org.OrganizationId);
            }
        }
    }
}