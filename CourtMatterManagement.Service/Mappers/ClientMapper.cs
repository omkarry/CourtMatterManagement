using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Mappers
{
    public class ClientMapper
    {
        public ClientDto Map(Client entity)
        {
            return new ClientDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Phone = entity.PhoneNumber,
                Email = entity.Email
            };
        }
    }
}
