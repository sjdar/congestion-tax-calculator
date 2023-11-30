using CongestionTaxCalculator.Dto.EFCoreModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.DataAccess
{
    public class TaxRepository : ITaxRepository
    {
        public TaxRepository()
        {
            using (var context = new ApiContext())
            {
                #region TaxPaymentPeriod Init
                var taxPaymentPeriod = new List<TaxPaymentPeriod>
                {
                new TaxPaymentPeriod
                {
                    Id=1,
                    Amount =8,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("06:00"),
                    EndTime=DateTime.Parse("06:29"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now

                },
                new TaxPaymentPeriod
                {
                    Id=2,
                    Amount =13,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("06:30"),
                    EndTime=DateTime.Parse("06:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },

                new TaxPaymentPeriod
                {
                    Id=3,
                    Amount =18,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("07:00"),
                    EndTime=DateTime.Parse("07:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new TaxPaymentPeriod
                {
                    Id=4,
                    Amount =13,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("08:00"),
                    EndTime=DateTime.Parse("08:29"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new TaxPaymentPeriod
                {
                    Id=5,
                    Amount =8,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("08:30"),
                    EndTime=DateTime.Parse("14:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new TaxPaymentPeriod
                {
                    Id=6,
                    Amount =13,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("15:00"),
                    EndTime=DateTime.Parse("15:29"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new TaxPaymentPeriod
                {
                    Id=7,
                    Amount =18,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("15:30"),
                    EndTime=DateTime.Parse("16:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new TaxPaymentPeriod
                {
                    Id=8,
                    Amount =13,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("17:00"),
                    EndTime=DateTime.Parse("17:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new TaxPaymentPeriod
                {
                    Id=9,
                    Amount =8,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("18:00"),
                    EndTime=DateTime.Parse("18:29"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new TaxPaymentPeriod
                {
                    Id=10,
                    Amount =0,
                    Currency ="SEK",
                    StartTime=DateTime.Parse("18:30"),
                    EndTime=DateTime.Parse("05:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                }
                };
                #endregion
                if (context.TaxPaymentPeriod == null)
                {
                    context.TaxPaymentPeriod.AddRange(taxPaymentPeriod);
                    context.SaveChanges();

                }
            }
        }

        public async Task<List<TaxPaymentPeriod>> GetAllTaxPaymentPeriodsAsync()
        {
            using var context = new ApiContext();

            return await context.TaxPaymentPeriod.ToListAsync();
        }

        public async Task<int> GetTaxPaymentWithTimeAsync(DateTime dateTime)
        {
            var currentTime = dateTime.TimeOfDay;
            using var context = new ApiContext();

            var taxPaymentPeriod = await context.TaxPaymentPeriod.Where(x => x.StartTime.TimeOfDay.Hours <= dateTime.TimeOfDay.Hours
                                                 && x.StartTime.TimeOfDay.Minutes <= dateTime.TimeOfDay.Minutes
                                                 && x.EndTime.TimeOfDay.Hours >= dateTime.TimeOfDay.Hours
                                                 && x.EndTime.TimeOfDay.Minutes >= dateTime.TimeOfDay.Minutes).FirstOrDefaultAsync();
            if (taxPaymentPeriod is null)
                throw new ArgumentNullException(nameof(TaxPaymentPeriod.Amount));

            return taxPaymentPeriod.Amount;
        }
    }
}
