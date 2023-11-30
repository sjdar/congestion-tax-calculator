using CongestionTaxCalculator.DataAccess;
using CongestionTaxCalculator.Dto;
using CongestionTaxCalculator.Dto.Enums;
using CongestionTaxCalculator.Dto.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
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
    public async Task<int> GetTax(VehicelTypes vehicelTypes, DateTime[] dates)
    {
        var dgdgdf= dates.GroupBy(x=>x.Month);
        DateTime intervalStart = dates.First();
        int totalFee = 0;

        foreach (DateTime date in dates)
        {
            totalFee = await CalculateTotalFeeAsync(intervalStart, vehicelTypes, date);
        }
        if (totalFee > appSettings.MaximumTaxAmountPerDay) totalFee = appSettings.MaximumTaxAmountPerDay;
        return totalFee;
    }

    private async Task<int> CalculateTotalFeeAsync(DateTime intervalStart, VehicelTypes vehicelTypes, DateTime date)
    {
        int totalFee = 0;
        int nextFee = await GetTollFeeAsync(date, vehicelTypes);
        int tempFee = await GetTollFeeAsync(intervalStart, vehicelTypes);

        long diffInMillies = date.Millisecond - intervalStart.Millisecond;
        long minutes = diffInMillies / 1000 / 60;
        //int Time Limit 
        if (minutes <= 60)
        {
            if (totalFee > 0) totalFee -= tempFee;
            if (nextFee >= tempFee) tempFee = nextFee;
            totalFee += tempFee;
        }

        totalFee += nextFee;


        return totalFee;
    }

    private async Task<int> GetTollFeeAsync(DateTime date, VehicelTypes vehicelTypes)
    {
        if (date.IsTollFreeDate() || vehicelTypes.IsTollFreeVehicle())
            return 0;

        return await authorRepository.GetTaxPaymentWithTimeAsync(date);
    }


    private bool PassesSeveralTollingStations()
    {
        return false;
    }


}