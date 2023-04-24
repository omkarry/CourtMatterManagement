using AutoMapper;
using CourtMatterManagement.Data.DbContexts;
using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using CourtMatterManagement.Service.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CourtMatterManagement.Service.Services
{
    public class ClientService : IClientRepository
    {
        private readonly CourtMatterDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(CourtMatterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ClientDto> GetAllClients()
        {
            List<Client> clients = _context.Clients.Include(c => c.Matters).ToList();
            return clients.Select(c => new ClientMapper().Map(c)).ToList();
        }

        public ClientDto? GetClientById(int id)
        {
            Client? clientById = _context.Clients.Include(c => c.Matters).ToList().Where(_ => _.Id == id).FirstOrDefault();
            if (clientById == null)
                return null;
            else
                return (new ClientMapper().Map(clientById));
        }

        public void CreateClient(ClientDto client)
        {
            Client? newClient = new()
            {
                Name = client.Name,
                Address = client.Address,
                PhoneNumber = client.Phone,
                Email = client.Email
            };
            _context.Clients.Add(newClient);
            _context.SaveChanges();
        }

        public ClientDto? UpdateClient(int id, ClientDto clientDto)
        {
            Client? client = _context.Clients.Include(c => c.Matters).ToList().FirstOrDefault(_ => _.Id == id);
            if (client == null)
                return null;
            else
            {
                client.Name = clientDto.Name;
                client.Address = clientDto.Address;
                client.PhoneNumber = clientDto.Phone;
                client.Email = clientDto.Email;
                _context.Clients.Update(client);
                _context.SaveChanges();
                return (new ClientMapper().Map(client));
            }
        }

        public bool DeleteClient(int id)
        {
            Client? client= _context.Clients.Include(c => c.Matters).ToList().FirstOrDefault(_ => _.Id == id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
