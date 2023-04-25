using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;

namespace CourtMatterManagement.Service.Interfaces
{
    public interface IMatterRepository
    {
        public List<MatterDto> GetAllMatters();
        public MatterDto? GetMatterById(int id);
        public void CreateMatter(MatterDto matterDto);
        public MatterDto? UpdateMatter(int id, MatterDto matterDto);
        public bool DeleteMatter(int id);
        public List<MatterDto> GetMattersByClient(int clientId);
        public List<InvoiceDto> GetInvoicesByMatter(int matterId);
        public List<InvoiceDto> GetLastWeeksBillingByAttorney(int attorneyId);
        public List<IGrouping<int, InvoiceDto>> GetAllInvoices();
        public List<IGrouping<int, InvoiceDto>> GetLastWeekBillingsByAttorney();
        public List<IGrouping<int, MatterDto>> GetAllMattersByClients();

    }
}
