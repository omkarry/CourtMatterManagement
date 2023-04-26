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
        public List<ClientMatterDto> GetMattersByClient(int clientId);
        public List<InvoiceWithDetailsDto> GetInvoicesByMatter(int matterId);
        public List<InvoiceWithDetailsDto> GetLastWeeksBillingByAttorney(int attorneyId);
        public List<IGrouping<int, InvoiceWithDetailsDto>> GetAllInvoicesByMatters();
        public List<IGrouping<int, InvoiceWithDetailsDto>> GetLastWeekBillingsByAttorney();
        public List<IGrouping<int, ClientMatterDto>> GetAllMattersByClients();

    }
}
