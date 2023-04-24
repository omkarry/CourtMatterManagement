using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int MatterId { get; set; }
        public int AttorneyId { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TimeSpent { get; set; }
        public DateTime Date { get; set; }
    }
}
