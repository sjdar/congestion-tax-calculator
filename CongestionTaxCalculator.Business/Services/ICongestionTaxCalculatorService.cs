
using CongestionTaxCalculator.Dto.Enums;
using System;
using System.Threading.Tasks;

public interface ICongestionTaxCalculatorService
{
    Task<int> GetTax(VehicelTypes vehicelTypes, DateTime[] dates);
}