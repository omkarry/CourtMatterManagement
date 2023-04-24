using CourtMatterManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Interfaces
{
    public interface IJurisdictionRepository
    {
        public List<JurisdictionDto> GetAllJurisdictions();
        public JurisdictionDto? GetJurisdictionById(int id);
        public void CreateJurisdiction(JurisdictionDto jurisdictionDto);
    }
}
