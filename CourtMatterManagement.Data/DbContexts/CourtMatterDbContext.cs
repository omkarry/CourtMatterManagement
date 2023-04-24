using CourtMatterManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CourtMatterManagement.Data.DbContexts
{
    public class CourtMatterDbContext : DbContext
    {
        public CourtMatterDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Matter> Matters { get; set; }
        public DbSet<Attorney> Attorneys { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Jurisdiction> Jurisdictions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attorney>()
                .HasOne(a => a.Jurisdiction)
                .WithMany(j => j.Attorneys)
                .HasForeignKey(j => j.JurisdictionId);

            modelBuilder.Entity<Matter>()
                .HasOne(a => a.Jurisdiction)
                .WithMany(j => j.Matters)
                .HasForeignKey(j => j.JurisdictionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Matter>()
                .HasOne(m => m.Client)
                .WithMany(c => c.Matters)
                .HasForeignKey(m => m.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Matter>()
                .HasOne(m => m.BillingAttorney)
                .WithMany(e => e.BilingAttorneyMatters)
                .HasForeignKey(m => m.BillingAttorneyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Matter>()
                .HasOne(m => m.ResponsibleAttorney)
                .WithMany(e => e.ResponsibleAttorneyMatters)
                .HasForeignKey(m => m.ResponsibleAttorneyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Matter)
                .WithMany(m => m.Invoices)
                .HasForeignKey(i => i.MatterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Attorney)
                .WithMany(e => e.Invoices)
                .HasForeignKey(i => i.AttorneyId);
        }
    }
}
