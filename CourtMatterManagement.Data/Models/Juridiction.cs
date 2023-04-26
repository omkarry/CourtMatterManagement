using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Data.Models
{
    public class Jurisdiction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Matter> Matters { get; set; }
        public ICollection<Attorney> Attorneys { get; set; }
    }
}
