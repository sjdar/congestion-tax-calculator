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
                    StartTimeHour=06,
                    StartTimeMinute=0,
                    EndTimeHour=06,
                    EndTimeMinute=29,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now

                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=2,
                    Amount =13,
                    StartTimeHour=6,
                    StartTimeMinute=30,
                    EndTimeHour=6,
                    EndTimeMinute=59,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },

                new GothenburgTaxPaymentPeriod
                {
                    Id=3,
                    Amount =18,
                    StartTimeHour=7,
                    StartTimeMinute=0,
                    EndTimeHour=7,
                    EndTimeMinute=59,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=4,
                    Amount =13,
                    StartTimeHour=8,
                    StartTimeMinute=0,
                    EndTimeHour=8,
                    EndTimeMinute=29,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=5,
                    Amount =8,
                    StartTimeHour=8,
                    StartTimeMinute=30,
                    EndTimeHour=14,
                    EndTimeMinute=59,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=6,
                    Amount =13,
                    StartTimeHour=15,
                    StartTimeMinute=0,
                    EndTimeHour=15,
                    EndTimeMinute=29,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=7,
                    Amount =18,
                    StartTimeHour=15,
                    StartTimeMinute=30,
                    EndTimeHour=16,
                    EndTimeMinute=59,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=8,
                    Amount =13,
                    StartTimeHour=17,
                    StartTimeMinute=0,
                    EndTimeHour=17,
                    EndTimeMinute=59,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=9,
                    Amount =8,
                    StartTimeHour=18,
                    StartTimeMinute=0,
                    EndTimeHour=18,
                    EndTimeMinute=29,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=10,
                    Amount =0,
                    StartTimeHour=18,
                    StartTimeMinute=30,
                    EndTimeHour=05,
                    EndTimeMinute=59,
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                }
                };
                #endregion
                if (!context.GothenburgTaxPaymentPeriod.Any())
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

            var gothenburgTaxPaymentPeriod = new GothenburgTaxPaymentPeriod();
            using var context = new ApiContext();


            gothenburgTaxPaymentPeriod = await context.GothenburgTaxPaymentPeriod.Where(x => x.StartTimeHour <= dateTime.Hour
                     && x.StartTimeMinute <= dateTime.Minute
                     && x.EndTimeHour >= dateTime.Hour
                     && x.EndTimeMinute >= dateTime.Minute).FirstOrDefaultAsync();
            if (gothenburgTaxPaymentPeriod != null) return gothenburgTaxPaymentPeriod.Amount;

            gothenburgTaxPaymentPeriod = await context.GothenburgTaxPaymentPeriod.Where(x => x.StartTimeHour <= dateTime.Hour
                                             && x.EndTimeHour >= dateTime.Hour
                                           ).FirstOrDefaultAsync();
            if (gothenburgTaxPaymentPeriod != null) return gothenburgTaxPaymentPeriod.Amount;
            throw new ArgumentNullException(nameof(GothenburgTaxPaymentPeriod.Amount));



        }

    }
}
