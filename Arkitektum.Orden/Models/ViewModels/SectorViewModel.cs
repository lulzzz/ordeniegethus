﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class SectorViewModel : Mapper<Sector, SectorViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public IEnumerable<ApplicationViewModel> Applications { get; set; }


        public override IEnumerable<SectorViewModel> MapToEnumerable(IEnumerable<Sector> sectors)
        {
            var output = new List<SectorViewModel>();
            if (sectors != null)
            {
                foreach (var sector in sectors)
                {
                    output.Add(Map(sector));
                }
            }

            return output;
        }

        public override SectorViewModel Map(Sector input)
        {
            return new SectorViewModel
            {
                Id = input.Id,
                Name = input.Name,
                Applications = new ApplicationViewModel().MapToEnumerable(input.ApplicationsAsEnumerable())
            };
        }

        public Sector Map(SectorViewModel sector)
        {
            return new Sector
            {
                Id = sector.Id,
                Name = sector.Name
            };
        }

        public List<SelectListItem> MapToSelectListItems(IEnumerable<Sector> sectors, int? sectorId)
        {
            var selectedId = sectorId ?? 0;
            
            var sectorItems = new List<SelectListItem>();

            foreach (var sector in sectors)
            {
                sectorItems.Add(new SelectListItem
                {
                    Text = sector.Name,
                    Value = sector.Id.ToString(),
                    Selected = sector.Id == selectedId
                });
            }

            return sectorItems;
        }
    }
}