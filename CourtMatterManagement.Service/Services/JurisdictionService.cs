using AutoMapper;
using CourtMatterManagement.Data.DbContexts;
using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using CourtMatterManagement.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Services
{
    public class JurisdictionService : IJurisdictionRepository
    {
        private readonly CourtMatterDbContext _context;
        private readonly IMapper _mapper;

        public JurisdictionService(CourtMatterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateJurisdiction(JurisdictionDto jurisdictionDto)
        {
            var newJurisdiction = new Jurisdiction
            {
                Name = jurisdictionDto.Name
            };
            _context.Jurisdictions.Add(newJurisdiction);
            _context.SaveChanges();
        }

        public List<JurisdictionDto> GetAllJurisdictions()
        {
            List<Jurisdiction>  jurisdictions = _context.Jurisdictions.ToList();
            return jurisdictions.Select(c => new JurisdictionMapper().Map(c)).ToList();
        }
        public JurisdictionDto? GetJurisdictionById(int id)
        {
            Jurisdiction? jurisdictionById = _context.Jurisdictions.Where(_ => _.Id == id).FirstOrDefault();
            if (jurisdictionById == null)
                return null;
            else
                return (new JurisdictionMapper().Map(jurisdictionById));
        }
    }
}
