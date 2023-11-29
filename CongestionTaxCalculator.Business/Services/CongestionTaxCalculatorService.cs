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

    public CongestionTaxCalculatorService(IOptions<AppSettings> appSettings, ITaxRepository authorRepository )
    {
        this.appSettings = appSettings.Value;
        this.authorRepository = authorRepository;
    }
    public async Task<int> GetTax(VehicelTypes vehicelTypes,DateTime[] dates)
    {
        DateTime intervalStart = dates.First();
        int totalFee = 0;
        
        foreach (DateTime date in dates)
        {
            totalFee =await CalculateTotalFee(intervalStart, vehicelTypes, date);
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private async Task<int> CalculateTotalFee(DateTime intervalStart, VehicelTypes vehicelTypes, DateTime date)
    {
        int totalFee = 0;
        int nextFee =await GetTollFee(date, vehicelTypes);
        int tempFee = await GetTollFee(intervalStart, vehicelTypes);

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

    private async Task<int> GetTollFee(DateTime date, VehicelTypes vehicelTypes)
    {
        if (date.IsTollFreeDate() || vehicelTypes.IsTollFreeVehicle()) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        // it can comes from db select query
        authorRepository.GetTaxPaymentWithTime(date);
        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }


    private bool PassesSeveralTollingStations()
    {
        return false;
    }


}