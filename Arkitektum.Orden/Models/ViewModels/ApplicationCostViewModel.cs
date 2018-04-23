using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationCostViewModel : Mapper<Application, ApplicationCostViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InitialCost { get; set; }
        public string AnnualFee { get; set; }

        public override IEnumerable<ApplicationCostViewModel> MapToEnumerable(IEnumerable<Application> inputs)
        {
            List<ApplicationCostViewModel> models = new List<ApplicationCostViewModel>();

            foreach (var input in inputs)
            {
                models.Add(Map(input));
            }

            return models;
        }

        public override ApplicationCostViewModel Map(Application input)
        {
            return new ApplicationCostViewModel
            {
                Id = input.Id,
                Name = input.Name,
                InitialCost = input.DecimalToString(input.InitialCost),
                AnnualFee = input.DecimalToString(input.AnnualFee),

            };
        }
    }
}
