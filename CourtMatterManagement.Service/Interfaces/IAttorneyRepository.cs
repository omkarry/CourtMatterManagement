using CourtMatterManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Interfaces
{
    public interface IAttorneyRepository
    {
        public List<AttorneyDto> GetAllAttorneys();
        AttorneyDto? GetAttorneyById(int id);
        void CreateAttorney(AttorneyDto attorneyDto);
        AttorneyDto? UpdateAttorney(int id, AttorneyDto attorneyDto);
        bool DeleteAttorney(int id);
    }
}
