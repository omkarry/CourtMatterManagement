using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Mappers
{
    public class AttorneyMapper
    {
        public AttorneyDto Map(Attorney entity)
        {
            return new AttorneyDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                JurisdictionId = entity.JurisdictionId
            };
        }
    }
}
