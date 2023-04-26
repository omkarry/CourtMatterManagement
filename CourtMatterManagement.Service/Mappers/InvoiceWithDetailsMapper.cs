using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;

namespace CourtMatterManagement.Service.Mappers
{
    public class InvoiceWithDetailsMapper
    {
        public InvoiceWithDetailsDto Map(Invoice entity) 
        {
            return new InvoiceWithDetailsDto
            {
                Id = entity.Id,
                MatterId = entity.MatterId,
                MatterName = entity.Matter.Name,
                ClientName = entity.Matter.Client.Name,
                AttorneyId = entity.AttorneyId,
                AttorneyName = entity.Attorney.Name,
                RatePerHour = entity.HourlyRate,
                TimeSpent = entity.TimeSpent,
                TotalAmount = entity.HourlyRate * entity.TimeSpent,
                Date = entity.Date
            };
        }
    }
}
