using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtMatterManagement.Data.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Matter")]
        public int MatterId { get; set; }
        public Matter Matter { get; set; }

        [Required]
        [ForeignKey("Attorney")]
        public int AttorneyId{ get; set; }
        public Attorney Attorney { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        [Required]
        public decimal TimeSpent { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
