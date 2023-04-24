using AutoMapper;
using CourtMatterManagement.Data.DbContexts;
using CourtMatterManagement.Data.Models;
using CourtMatterManagement.Service.DTOs;
using CourtMatterManagement.Service.Interfaces;
using CourtMatterManagement.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Services
{
    public class InvoiceService : IInvoiceRepository
    {
        private readonly CourtMatterDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceService(CourtMatterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<InvoiceDto> GetAllInvoices()
        {
            List<Invoice> invoice = _context.Invoices.ToList();
            return invoice.Select(c => new InvoiceMapper().Map(c)).ToList();
        }

        public InvoiceDto? GetInvoiceById(int id)
        {
            Invoice? invoiceById = _context.Invoices.ToList().Where(_ => _.Id == id).FirstOrDefault();
            if (invoiceById == null)
                return null;
            else
                return (new InvoiceMapper().Map(invoiceById));
        }

        public void CreateInvoice(InvoiceDto invoice)
        {
            Matter? matter = _context.Matters.Where(_ => _.Id == invoice.MatterId).FirstOrDefault();
            if(matter?.ResponsibleAttorneyId == invoice.AttorneyId || matter?.BillingAttorneyId == invoice.AttorneyId)
            {
                Invoice? newInvoice = new()
                {
                    AttorneyId = invoice.AttorneyId,
                    MatterId = invoice.MatterId,
                    HourlyRate = invoice.HourlyRate,
                    TimeSpent = invoice.TimeSpent,
                    Date = invoice.Date
                };
                _context.Invoices.Add(newInvoice);
                _context.SaveChanges();
            }
        }

        public InvoiceDto? UpdateInvoice(int id, InvoiceDto invoiceDto)
        {
            Invoice? invoice = _context.Invoices.ToList().FirstOrDefault(_ => _.Id == id);
            if (invoice == null)
                return null;
            else
            {
                invoice.AttorneyId = invoiceDto.AttorneyId;
                invoice.MatterId = invoiceDto.MatterId;
                invoice.HourlyRate = invoiceDto.HourlyRate;
                invoice.TimeSpent= invoiceDto.TimeSpent;
                invoice.Date = invoiceDto.Date;
                _context.Invoices.Update(invoice);
                _context.SaveChanges();
                return (new InvoiceMapper().Map(invoice));
            }
        }

        public bool DeleteInvoice(int id)
        {
            Invoice? invoice = _context.Invoices.ToList().Where(_ => _.Id == id).FirstOrDefault();
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
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
