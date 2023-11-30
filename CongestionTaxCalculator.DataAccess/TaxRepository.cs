using CongestionTaxCalculator.Dto.EFCoreModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                #region GothenburgTaxPaymentPeriod Init
                var GothenburgTaxPaymentPeriod = new List<GothenburgTaxPaymentPeriod>
                {
                new GothenburgTaxPaymentPeriod
                {
                    Id=1,
                    Amount =8,
                    StartTime=DateTime.Parse("06:00"),
                    EndTime=DateTime.Parse("06:29"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now

                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=2,
                    Amount =13,
                    StartTime=DateTime.Parse("06:30"),
                    EndTime=DateTime.Parse("06:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },

                new GothenburgTaxPaymentPeriod
                {
                    Id=3,
                    Amount =18,
                    StartTime=DateTime.Parse("07:00"),
                    EndTime=DateTime.Parse("07:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=4,
                    Amount =13,
                    StartTime=DateTime.Parse("08:00"),
                    EndTime=DateTime.Parse("08:29"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=5,
                    Amount =8,
                    StartTime=DateTime.Parse("08:30"),
                    EndTime=DateTime.Parse("14:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=6,
                    Amount =13,
                    StartTime=DateTime.Parse("15:00"),
                    EndTime=DateTime.Parse("15:29"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=7,
                    Amount =18,
                    StartTime=DateTime.Parse("15:30"),
                    EndTime=DateTime.Parse("16:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=8,
                    Amount =13,
                    StartTime=DateTime.Parse("17:00"),
                    EndTime=DateTime.Parse("17:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=9,
                    Amount =8,
                    StartTime=DateTime.Parse("18:00"),
                    EndTime=DateTime.Parse("18:29"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=10,
                    Amount =0,
                    StartTime=DateTime.Parse("18:30"),
                    EndTime=DateTime.Parse("05:59"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                }
                };
                #endregion
                if (context.GothenburgTaxPaymentPeriod == null)
                {
                    context.GothenburgTaxPaymentPeriod.AddRange(GothenburgTaxPaymentPeriod);
                    context.SaveChanges();

                }
            }
        }

        public async Task<List<GothenburgTaxPaymentPeriod>> GetAllGothenburgTaxPaymentPeriodsAsync()
        {
            using var context = new ApiContext();

            return await context.GothenburgTaxPaymentPeriod.ToListAsync();
        }

        public async Task<int> GetTaxPaymentWithTimeAsync(DateTime dateTime)
        {
            string timeString24Hour = dateTime.ToString("HH:mm", CultureInfo.CurrentCulture);
            using var context = new ApiContext();

            var GothenburgTaxPaymentPeriod = await context.GothenburgTaxPaymentPeriod.Where(x => x.StartTime.Hour <= dateTime.Hour
                                                 && x.StartTime.Minute <= dateTime.Minute
                                                 && x.EndTime.Hour >= dateTime.Hour
                                                 && x.EndTime.Minute >= dateTime.Minute).FirstOrDefaultAsync();
            if (GothenburgTaxPaymentPeriod is null)
                throw new ArgumentNullException(nameof(GothenburgTaxPaymentPeriod.Amount));

            return GothenburgTaxPaymentPeriod.Amount;
        }
    }
}
