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
            var date = item.Select(x => x.Date).FirstOrDefault();

            if (item.Count() > 1)
            {
                var groupByHours = item.GroupBy(x => x.Hour).Select(g => g.ToList());

                {
                    foreach (var groupedHourse in groupByHours)
                    {
                        //for same hours rules say just pay once
                        
                        totalFee.Add(await CalculateTotalFeeAsync(date, vehicelTypes));

                    }

                }

            }
            else totalFee.Add(await CalculateTotalFeeAsync(date, vehicelTypes));
        }
        var finalSum = totalFee.Sum();
        if (finalSum > appSettings.MaximumTaxAmountPerDay) finalSum = appSettings.MaximumTaxAmountPerDay;
        return new TaxCalculatorResult() { IsTollFree = finalSum > 0 ? false : true, Currency = appSettings.Currency, Amount = finalSum };
    }

    private async Task<int> CalculateTotalFeeAsync(DateTime date, VehicelTypes vehicelTypes)
    {
        if (CheckTollFreeVehicles(date, vehicelTypes)) return 0;
        return await authorRepository.GetTaxPaymentWithTimeAsync(date);
    }

    private bool CheckTollFreeVehicles(DateTime date, VehicelTypes vehicelTypes)
    {
        if (date.IsTollFreeDate() || vehicelTypes.IsTollFreeVehicle())
            return true;
        return false;
    }


}