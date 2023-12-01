using CongestionTaxCalculator.Dto.EFCoreModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.DataAccess
{
    public interface ITaxRepository
    {
        Task<List<GothenburgTaxPaymentPeriod>> GetAllGothenburgTaxPaymentPeriodsAsync();
        Task<int> GetTaxPaymentWithTimeAsync(DateTime dateTime);
       

    }
}