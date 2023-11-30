using CongestionTaxCalculator.Dto.Enums;
using System;

namespace CongestionTaxCalculator.WeApi.Dto
{
    public class TaxCalculatorDto
    {
        public DateTime[] DateTimes { get; set; }
        public VehicelTypes VehicelTypes { get; set; }

    }
}
