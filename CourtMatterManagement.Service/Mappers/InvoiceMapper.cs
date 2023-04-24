using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Mappers
{
    public class InvoiceMapper
    {
        public InvoiceDto Map(Invoice entity)
        {
            return new InvoiceDto
            {
                Id = entity.Id,
                MatterId = entity.MatterId,
                AttorneyId = entity.AttorneyId,
                HourlyRate = entity.HourlyRate,
                TimeSpent = entity.TimeSpent,
                Date = entity.Date
            };
        }
    }
}
