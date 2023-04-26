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
        public List<ClientMatterDto> GetMattersByClient(int clientId)
        {
            return _context.Matters
                .Include(_ => _.BillingAttorney)
                .Include(_ => _.Jurisdiction)
                .Include(_ => _.Client)
                .Include(_ => _.ResponsibleAttorney)
                .Where(m => m.ClientId == clientId)
                .Select(c => new ClientMatterMapper().Map(c))
                .ToList();
        }

        public List<InvoiceWithDetailsDto> GetInvoicesByMatter(int matterId)
        {
            List<InvoiceWithDetailsDto> invoicesByMatter = _context.Invoices
                .Include(_ => _.Attorney)
                .Include(_ => _.Matter.Client)
                .Where(_ => _.MatterId == matterId)
                .Select(c => new InvoiceWithDetailsMapper().Map(c))
                .ToList();
            return invoicesByMatter;
        }

        public List<InvoiceWithDetailsDto> GetLastWeeksBillingByAttorney(int attorneyId)
        {
            DateTime lastMonday = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek);
            DateTime lastSunday = lastMonday.AddDays(-6);

            List<InvoiceWithDetailsDto> invoices = _context.Invoices
                .Include(_ => _.Attorney)
                .Include(_ => _.Matter.Client)
                .Where(i => i.AttorneyId == attorneyId && i.Date >= lastSunday && i.Date <= lastMonday)
                .Select(c => new InvoiceWithDetailsMapper().Map(c))
                .ToList();
            return invoices;
        }

        public List<IGrouping<int, ClientMatterDto>> GetAllMattersByClients()
        {

            List<IGrouping<int, ClientMatterDto>> matterList = _context.Matters
                .Include(_ => _.BillingAttorney)
                .Include(_ => _.Jurisdiction)
                .Include(_ => _.Client)
                .Include(_ => _.ResponsibleAttorney)
                .Select(c => new ClientMatterMapper().Map(c)).AsEnumerable().GroupBy(s => s.ClientId).ToList();
            return matterList;
        }

        public List<IGrouping<int, InvoiceWithDetailsDto>> GetAllInvoicesByMatters()
        {
            List<IGrouping<int, InvoiceWithDetailsDto>> invoiceList = _context.Invoices 
                .Include(_ => _.Attorney)
                .Include(_ => _.Matter.Client)
                .Select(c => new InvoiceWithDetailsMapper().Map(c)).AsEnumerable().GroupBy(s => s.MatterId).ToList();
            return invoiceList;
        }

        public List<IGrouping<int, InvoiceWithDetailsDto>> GetLastWeekBillingsByAttorney()
        {
            DateTime lastMonday = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek);
            DateTime lastSunday = lastMonday.AddDays(-6);

            List<IGrouping<int, InvoiceWithDetailsDto>> invoices= _context.Invoices
                            .Include(_ => _.Attorney)
                            .Include(_ => _.Matter.Client)
                            .Where(i => i.Date >= lastSunday && i.Date <= lastMonday).Select(c => new InvoiceWithDetailsMapper().Map(c)).AsEnumerable().GroupBy(s => s.AttorneyId).ToList();
            return invoices;
        }
    }
}
