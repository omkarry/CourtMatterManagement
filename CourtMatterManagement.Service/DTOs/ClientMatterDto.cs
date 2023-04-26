using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.DTOs
{
    public class ClientMatterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string JurisdictionName { get; set; }
        public string BillingAttorneyName { get; set; }
        public string ResponsibleAttorneyName { get; set; }
    }
}
