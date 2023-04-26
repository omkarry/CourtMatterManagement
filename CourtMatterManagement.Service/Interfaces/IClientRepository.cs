using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Interfaces
{
    public interface IClientRepository
    {
        public List<ClientDto> GetAllClients();
        ClientDto? GetClientById(int id);
        void CreateClient(ClientDto clientDto);
        ClientDto? UpdateClient(int id, ClientDto clientDto);
        bool DeleteClient(int id);
    }
}
