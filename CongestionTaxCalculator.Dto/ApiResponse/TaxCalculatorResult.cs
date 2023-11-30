namespace CongestionTaxCalculator.Dto.ApiResponse
{
    public class TaxCalculatorResult
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public bool IsTollFree { get; set; }
    }
}
