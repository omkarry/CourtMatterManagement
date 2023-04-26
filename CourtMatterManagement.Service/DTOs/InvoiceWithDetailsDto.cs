using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.DTOs
{
    public class InvoiceWithDetailsDto
    {
        public int Id { get; set; }
        public int MatterId { get; set; }
        public string MatterName { get; set; }
        public string ClientName { get; set; }
        public int AttorneyId { get; set; }
        public string AttorneyName { get; set; }
        public decimal RatePerHour { get; set; }
        public decimal TimeSpent { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
