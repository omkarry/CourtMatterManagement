using CourtMatterManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Interfaces
{
    public interface IInvoiceRepository
    {
        public List<InvoiceDto> GetAllInvoices();
        public InvoiceDto? GetInvoiceById(int id);
        public void CreateInvoice(InvoiceDto invoiceDto);
        public InvoiceDto? UpdateInvoice(int id, InvoiceDto invoiceDto);
        public bool DeleteInvoice(int id);
    }
}
