using CongestionTaxCalculator.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.WeApi.Dto
{
    public class TaxCalculatorDto
    {
        public DateTime[] DateTimes { get; set; }
        public VehicelTypes VehicelTypes { get; set; }
    }
}
