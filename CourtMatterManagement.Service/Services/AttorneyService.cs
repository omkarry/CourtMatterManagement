using AutoMapper;
using CourtMatterManagement.Data.DbContexts;
using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using CourtMatterManagement.Service.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CourtMatterManagement.Service.Services
{
    public class AttorneyService : IAttorneyRepository
    {
        private readonly CourtMatterDbContext _context;
        private readonly IMapper _mapper;

        public AttorneyService(CourtMatterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AttorneyDto> GetAllAttorneys()
        {
            List<Attorney> attorneys = _context.Attorneys.Include(c => c.Jurisdiction).ToList();
            return attorneys.Select(c => new AttorneyMapper().Map(c)).ToList();
        }

        public AttorneyDto? GetAttorneyById(int id)
        {
            Attorney? attorneyById = _context.Attorneys.Include(c => c.Jurisdiction).ToList().Where(_ => _.Id == id).FirstOrDefault();
            if (attorneyById == null)
                return null;
            else
                return (new AttorneyMapper().Map(attorneyById));
        }

        public void CreateAttorney(AttorneyDto attorney)
        {
            Attorney? newAttorney = new ()
            {
                Name = attorney.Name,
                Address = attorney.Address,
                PhoneNumber = attorney.PhoneNumber,
                Email = attorney.Email,
                JurisdictionId = attorney.JurisdictionId
            };
            _context.Attorneys.Add(newAttorney);
            _context.SaveChanges();
        }

        public AttorneyDto? UpdateAttorney(int id, AttorneyDto attorneyDto)
        {
            Attorney? attorneyToUpdate = _context.Attorneys.Include(c => c.Jurisdiction).ToList().Where(_ => _.Id == id).FirstOrDefault();
            if (attorneyToUpdate == null)
                return null;
            else
            {
                attorneyToUpdate.Name = attorneyDto.Name;
                attorneyToUpdate.Address = attorneyDto.Address;
                attorneyToUpdate.PhoneNumber = attorneyDto.PhoneNumber;
                attorneyToUpdate.Email = attorneyDto.Email;
                attorneyToUpdate.JurisdictionId = attorneyDto.JurisdictionId;
                _context.Attorneys.Update(attorneyToUpdate);
                _context.SaveChanges();
                return (new AttorneyMapper().Map(attorneyToUpdate));
            }
        }

        public bool DeleteAttorney(int id)
        {
            Attorney attorneyToDelete = _context.Attorneys.Include(c => c.Jurisdiction).ToList().FirstOrDefault(_ => _.Id == id);
            if (attorneyToDelete != null)
            {
                _context.Attorneys.Remove(attorneyToDelete);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<AttorneyDto> GetAttorneysByJurisdictionId(int jurisdictionId)
        {
            List<Attorney> attorneys = _context.Attorneys.Include(c => c.Jurisdiction).Where(_ => _.JurisdictionId == jurisdictionId).ToList();
            return attorneys.Select(c => new AttorneyMapper().Map(c)).ToList();
        }
    }
}
