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
                    StartTime=DateTime.Parse("06:00 AM"),
                    EndTime=DateTime.Parse("06:29 AM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now

                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=2,
                    Amount =13,
                    StartTime=DateTime.Parse("06:30 AM"),
                    EndTime=DateTime.Parse("06:59 AM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },

                new GothenburgTaxPaymentPeriod
                {
                    Id=3,
                    Amount =18,
                    StartTime=DateTime.Parse("07:00 AM"),
                    EndTime=DateTime.Parse("07:59 AM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=4,
                    Amount =13,
                    StartTime=DateTime.Parse("08:00 AM"),
                    EndTime=DateTime.Parse("08:29 AM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=5,
                    Amount =8,
                    StartTime=DateTime.Parse("08:30 AM"),
                    EndTime=DateTime.Parse("14:59 PM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=6,
                    Amount =13,
                    StartTime=DateTime.Parse("15:00 PM"),
                    EndTime=DateTime.Parse("15:29 PM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=7,
                    Amount =18,
                    StartTime=DateTime.Parse("15:30 PM"),
                    EndTime=DateTime.Parse("16:59 PM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=8,
                    Amount =13,
                    StartTime=DateTime.Parse("17:00 PM"),
                    EndTime=DateTime.Parse("17:59 PM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=9,
                    Amount =8,
                    StartTime=DateTime.Parse("18:00 PM"),
                    EndTime=DateTime.Parse("18:29 PM"),
                    CreatedBy="s.jahani",
                    CreatedOn=DateTime.Now
                },
                new GothenburgTaxPaymentPeriod
                {
                    Id=10,
                    Amount =0,
                    StartTime=DateTime.Parse("18:30 PM"),
                    EndTime=DateTime.Parse("05:59 AM"),
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

            if (dateTime.ToString("hh:mm tt").Contains("AM"))
            {
               var midNightRange=await context.GothenburgTaxPaymentPeriod.Where(x => x.StartTime.Hour > x.EndTime.Hour).FirstOrDefaultAsync();

                if(dateTime.Hour <=midNightRange.StartTime.Hour && dateTime.Hour <= midNightRange.EndTime.Hour) return midNightRange.Amount;

            }
            else
            {
                var convertedTime= dateTime.ToString("hh:mm", CultureInfo.InvariantCulture).Split(":");
                int hour = Convert.ToInt32(convertedTime[0]);   
                int minute = Convert.ToInt32(convertedTime[1]);
                 gothenburgTaxPaymentPeriod = await context.GothenburgTaxPaymentPeriod.Where(x => x.StartTime.Hour <= hour
                                     && x.StartTime.Minute <= minute
                                     && x.EndTime.Hour >= hour
                                     && x.EndTime.Minute >= minute).FirstOrDefaultAsync();
                if (gothenburgTaxPaymentPeriod is null)
                    throw new ArgumentNullException(nameof(GothenburgTaxPaymentPeriod.Amount));

                
            }

            return gothenburgTaxPaymentPeriod.Amount;

        }

        public async Task<int> GetTheHighestAmountAsync(DateTime dateTime)
        {
            var gothenburgTaxPaymentPeriod=new List<GothenburgTaxPaymentPeriod>();

            using var context = new ApiContext();

            if (dateTime.ToString("hh:mm tt").Contains("AM"))
            {
                var midNightRange = await context.GothenburgTaxPaymentPeriod.Where(x => x.StartTime.Hour > x.EndTime.Hour).FirstOrDefaultAsync();

                if (dateTime.Hour <= midNightRange.StartTime.Hour && dateTime.Hour <= midNightRange.EndTime.Hour) return midNightRange.Amount;

            }
            else
            {
                var convertedTime = dateTime.ToString("hh:mm", CultureInfo.InvariantCulture).Split(":");
                int hour = Convert.ToInt32(convertedTime[0]);
                int minute = Convert.ToInt32(convertedTime[1]);
                gothenburgTaxPaymentPeriod = await context.GothenburgTaxPaymentPeriod.Where(x => x.StartTime.Hour <= hour
                                    && x.EndTime.Hour >= hour).ToListAsync();

                if (!gothenburgTaxPaymentPeriod.Any())
                    throw new ArgumentNullException(nameof(GothenburgTaxPaymentPeriod.Amount));



            }
          

            return gothenburgTaxPaymentPeriod.Sum(x => x.Amount);   
        }
    }
}
