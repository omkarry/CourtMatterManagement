using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.DTOs
{
    public class MatterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int JurisdictionId { get; set; }
        public int BillingAttorneyId { get; set; }
        public int ResponsibleAttorneyId { get; set; }
    }
}
