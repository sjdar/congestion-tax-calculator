using CongestionTaxCalculator.Dto.Enums;
using Nager.Holiday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CongestionTaxCalculator.Dto.Extensions
{
    public static class TollFreeValidations
    {
        public static bool IsTollFreeVehicle(this VehicelTypes vehicle)
        {

            var tollFreeVehicles = Enum.Parse(typeof(TollFreeVehicles), vehicle.ToString());
            if (tollFreeVehicles != null) return true;
            return false;
        }

        public static bool IsTollFreeDate(this DateTime date)
        {
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            using var holidayClient = new HolidayClient();
            var holidays = holidayClient.GetHolidaysAsync(date.Year, "de").Result.ToList();
            if (holidays is null && !holidays.Any()) return false;
            var timePeriod = holidays?.FindAll(x => x.Date.Month== month && (x.Date.Day == date.Day || x.Date.Day - 1 == date.Day));
            if (timePeriod != null && timePeriod.Any()) return true;
            return false;
        }
    }
}
