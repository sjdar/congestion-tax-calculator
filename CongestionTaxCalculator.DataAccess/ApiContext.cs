﻿using CongestionTaxCalculator.Dto.EFCoreModels;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.DataAccess
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CongestionTax");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GothenburgTaxPaymentPeriod>().HasKey(x => x.Id);
           
        }
        public DbSet<GothenburgTaxPaymentPeriod> GothenburgTaxPaymentPeriod { get; set; }


    }
}
