using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Data.Models
{
    public class Attorney
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [ForeignKey("Jurisdiction")]
        public int JurisdictionId { get; set; }
        public Jurisdiction Jurisdiction { get; set; }
        public ICollection<Matter> BilingAttorneyMatters { get; set; }
        public ICollection<Matter> ResponsibleAttorneyMatters { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
