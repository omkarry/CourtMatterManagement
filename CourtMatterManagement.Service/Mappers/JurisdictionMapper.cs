using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;

namespace CourtMatterManagement.Service.Mappers
{
    public class JurisdictionMapper
    {
        public JurisdictionDto Map(Jurisdiction entity)
        {
            return new JurisdictionDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
