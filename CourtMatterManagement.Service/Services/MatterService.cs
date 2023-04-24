using AutoMapper;
using CourtMatterManagement.Data.DbContexts;
using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using CourtMatterManagement.Service.Mappers;
using Microsoft.EntityFrameworkCore;


namespace CourtMatterManagement.Service.Services
{
    public class MatterService : IMatterRepository
    {
        private readonly CourtMatterDbContext _context;
        private readonly IMapper _mapper;

        public MatterService(CourtMatterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MatterDto> GetAllMatters()
        {
            List<Matter> matters =_context.Matters.ToList();
            return matters.Select(c => new MatterMapper().Map(c)).ToList();
        }

        public MatterDto? GetMatterById(int id)
        {
            Matter? matterById = _context.Matters.Where(_ => _.Id == id).FirstOrDefault();
            if (matterById == null)
                return null;
            else
                return (new MatterMapper().Map(matterById));
        }

        public void CreateMatter(MatterDto matterDto)
        {
            Attorney? attorney = _context.Attorneys.Where(_ => _.Id == matterDto.BillingAttorneyId).FirstOrDefault();
            if(attorney?.JurisdictionId == matterDto.JurisdictionId)
            {
                Matter? newMatter = new()
                {
                    Name = matterDto.Name,
                    BillingAttorneyId = matterDto.BillingAttorneyId,
                    ResponsibleAttorneyId = matterDto.ResponsibleAttorneyId,
                    ClientId = matterDto.ClientId,
                    JurisdictionId = matterDto.JurisdictionId
                };
                _context.Matters.Add(newMatter);
                _context.SaveChanges();
            }
            
        }

        public MatterDto? UpdateMatter(int id, MatterDto matterDto)
        {
            Matter? matterToUpdate = _context.Matters.ToList().Where(_ => _.Id == id).FirstOrDefault();
            if (matterToUpdate == null)
                return null;
            else
            {
                matterToUpdate.Name = matterDto.Name;
                matterToUpdate.BillingAttorneyId = matterDto.BillingAttorneyId;
                matterToUpdate.ResponsibleAttorneyId = matterDto.ResponsibleAttorneyId;
                matterToUpdate.ClientId = matterDto.ClientId;
                matterToUpdate.JurisdictionId = matterDto.JurisdictionId;
                _context.Matters.Update(matterToUpdate);
                _context.SaveChanges();
                return (new MatterMapper().Map(matterToUpdate));
            }
        }

        public bool DeleteMatter(int id)
        {
            Matter? matterToDelete = _context.Matters.ToList().Where(_ => _.Id == id).FirstOrDefault();
            if (matterToDelete != null)
            {
                _context.Matters.Remove(matterToDelete);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<MatterDto> GetMattersByClient(int clientId)
        {
            return GetAllMatters()
                .Where(m => m.ClientId == clientId)
                .ToList();
        }

        public List<InvoiceDto> GetInvoicesByMatter(int matterId)
        {
            List<Invoice> invoicesByMatter = _context.Invoices.Where(_ => _.MatterId == matterId).ToList();
            return invoicesByMatter.Select(c => new InvoiceMapper().Map(c)).ToList();
        }

        public List<InvoiceDto> GetLastWeeksBillingByAttorney(int attorneyId)
        {
            DateTime lastMonday = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek);
            DateTime lastSunday = lastMonday.AddDays(-6);

            List<Invoice> invoices = _context.Invoices
                .Where(i => i.AttorneyId == attorneyId && i.Date >= lastSunday && i.Date <= lastMonday).ToList();
            return invoices.Select(c => new InvoiceMapper().Map(c)).ToList();
        }

        public List<IGrouping<int, Matter>> GetAllMattersByClients()
        {
            List<IGrouping<int, Matter>> matterList = _context.Matters.GroupBy(s => s.ClientId).ToList();
            return matterList;
        }

        public List<IGrouping<int, Invoice>> GetAllInvoices()
        {
            List<IGrouping<int, Invoice>> invoiceList = _context.Invoices.GroupBy(s => s.MatterId).ToList();
            return invoiceList;
        }

        public List<IGrouping<int, Invoice>> GetLastWeekBillingsByAttorney()
        {
            DateTime lastMonday = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek);
            DateTime lastSunday = lastMonday.AddDays(-6);

            List<IGrouping<int, Invoice>> invoices = _context.Invoices
                .Where(i => i.Date >= lastSunday && i.Date <= lastMonday).GroupBy(s => s.AttorneyId).ToList();
            return invoices;
        }
    }
}
