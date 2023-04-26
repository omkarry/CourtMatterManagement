using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;

namespace CourtMatterManagement.Service.Mappers
{
    public class ClientMatterMapper
    {
        public ClientMatterDto Map(Matter entity)
        {
            return new ClientMatterDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ClientId = entity.ClientId,
                BillingAttorneyName= entity.BillingAttorney.Name,
                ResponsibleAttorneyName= entity.ResponsibleAttorney.Name,
                ClientName= entity.Client.Name,
                JurisdictionName= entity.Jurisdiction.Name
            };
        }
    }
}
