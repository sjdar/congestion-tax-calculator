using CongestionTaxCalculator.DataAccess;
using CongestionTaxCalculator.Dto;
using CongestionTaxCalculator.Dto.ApiResponse;
using CongestionTaxCalculator.Dto.Enums;
using CongestionTaxCalculator.Dto.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public class CongestionTaxCalculatorService : ICongestionTaxCalculatorService
{
    private readonly AppSettings appSettings;
    private readonly ITaxRepository authorRepository;

    public CongestionTaxCalculatorService(IOptions<AppSettings> appSettings, ITaxRepository authorRepository)
    {
        this.appSettings = appSettings.Value;
        this.authorRepository = authorRepository;
    }

    public async Task<TaxCalculatorResult> GetTax(VehicelTypes vehicelTypes, DateTime[] dates)
    {
        var totalFee = new List<int>();
        var trafficInOneDay = dates.GroupBy(x => x.Day).Select(g => g.ToList());

        foreach (var item in trafficInOneDay)
        {
            var date = item.FirstOrDefault();

            if (item.Count() > 1)
            {
                var groupByHours = item.GroupBy(x => x.Hour).Select(g => g.ToList());

                {
                    foreach (var groupedHourse in groupByHours)
                    {
                        //for same hours rules say just pay once
                        var result = await GetTheHighestAmount(date, vehicelTypes);
                        if (result > appSettings.MaximumTaxAmountPerDay) result = appSettings.MaximumTaxAmountPerDay;
                        totalFee.Add(result);

                    }

                }

            }
            else
            {
                var result = await CalculateTotalFeeAsync(date, vehicelTypes);
                if (result > appSettings.MaximumTaxAmountPerDay) result = appSettings.MaximumTaxAmountPerDay;
                totalFee.Add(result);
            }
                
             
        }
        var finalSum = totalFee.Sum();
        if (finalSum > appSettings.MaximumTaxAmountPerDay) finalSum = appSettings.MaximumTaxAmountPerDay;
        return new TaxCalculatorResult() { IsTollFree = finalSum > 0 ? false : true, Currency = appSettings.Currency, Amount = finalSum };
    }

    private async Task<int> CalculateTotalFeeAsync(DateTime date, VehicelTypes vehicelType)
    {
        if (CheckTollFreeVehicles(date, vehicelType)) return 0;
        return await authorRepository.GetTaxPaymentWithTimeAsync(date);
    }
    private async Task<int> GetTheHighestAmount(DateTime date,VehicelTypes vehicelType)
    {
        if (CheckTollFreeVehicles(date, vehicelType)) return 0;
        return await authorRepository.GetTheHighestAmountAsync(date);
    }

    private bool CheckTollFreeVehicles(DateTime date, VehicelTypes vehicelTypes)
    {
        //if (date.IsTollFreeDate() || vehicelTypes.IsTollFreeVehicle())
            //return true;
        return false;
    }


}