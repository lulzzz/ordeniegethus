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

        public SimpleOrganization(List<OrganizationApplicationUser> organizationMemberships, int? organizationId = null)
        {
            var organizations = GetDistinctOrganizations(organizationMemberships);

            Init(organizations, organizationId);
        }

        public SimpleOrganization(List<Organization> organizations, int? organizationId)
        {
            Init(organizations, organizationId);
        }

        private List<Organization> GetDistinctOrganizations(List<OrganizationApplicationUser> organizationMemberships)
        {
            return organizationMemberships
                .GroupBy(o => o.OrganizationId)
                .Select(o => o.First().Organization)
                .ToList();
        }

        private void Init(List<Organization> organizations, int? organizationId)
        {
            Organization selectedOrganization;

            if (organizationId.HasValue)
                selectedOrganization = organizations.First(o => o.Id == organizationId.Value);
            else
                selectedOrganization = organizations.First();

            Id = selectedOrganization.Id;
            Name = selectedOrganization.Name;

            organizations.Remove(selectedOrganization);

            foreach (var org in organizations)
                AvailableOrganizations.Add(new SimpleOrganization
                {
                    Id = org.Id,
                    Name = org.Name
                });
        }

        public bool HasOnlyOneOrganization() => !AvailableOrganizations.Any();
    }
}