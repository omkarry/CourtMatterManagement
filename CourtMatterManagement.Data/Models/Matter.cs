using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Data.Models
{
    public class Matter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        [ForeignKey("Jurisdiction")]
        public int JurisdictionId { get; set; }
        public Jurisdiction Jurisdiction { get; set;}

        [Required]
        [ForeignKey("Attorney")]
        public int BillingAttorneyId { get; set; }
        public Attorney BillingAttorney { get; set; }

        [Required]
        [ForeignKey("Attorney")]
        public int ResponsibleAttorneyId { get; set; }
        public Attorney ResponsibleAttorney { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
