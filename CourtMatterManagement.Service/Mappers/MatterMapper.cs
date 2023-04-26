using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Mappers
{
    public class MatterMapper
    {
        public MatterDto Map(Matter entity)
        {
            return new MatterDto
            {
                Id = entity.Id,
                Name = entity.Name,
                BillingAttorneyId = entity.BillingAttorneyId,
                ResponsibleAttorneyId = entity.ResponsibleAttorneyId,
                ClientId = entity.ClientId,
                JurisdictionId = entity.JurisdictionId
            };
        }
    }
}
