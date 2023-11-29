using CongestionTaxCalculator.Dto.EFCoreModels;
using System;
using System.Collections.Generic;

namespace CongestionTaxCalculator.DataAccess
{
    public interface ITaxRepository
    {
        List<TaxPaymentPeriod> GetAllTaxPaymentPeriods();
        int GetTaxPaymentWithTime(DateTime dateTime);

    }
}