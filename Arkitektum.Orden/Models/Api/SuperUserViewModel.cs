using Arkitektum.Orden.Models.ViewModels;

namespace Arkitektum.Orden.Models.Api
{
    public class SuperUserViewModel : ViewModel<SuperUser, SuperUserViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OrganizationId { get; set; }
     
        public override SuperUserViewModel MapToViewModel(SuperUser input)
        {
            return new SuperUserViewModel
            {
                Id = input.Id,
                Name = input.Name,
                Email = input.Email,
                OrganizationId = input.OrganizationId
            };
        }

        public override SuperUser MapToModel(SuperUserViewModel input)
        {
            return new SuperUser
            {
                Id = input.Id,
                Name = input.Name,
                Email = input.Email,
                OrganizationId = input.OrganizationId
            };
        }
    }
}