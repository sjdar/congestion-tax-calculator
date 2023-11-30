
using CongestionTaxCalculator.Dto.ApiResponse;
using CongestionTaxCalculator.Dto.Enums;
using System;
using System.Threading.Tasks;

public interface ICongestionTaxCalculatorService
{
    Task<TaxCalculatorResult> GetTax(VehicelTypes vehicelTypes, DateTime[] dates);
}