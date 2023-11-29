namespace CongestionTaxCalculator.Dto
{
    public class AppSettings
    {
        public int MaximumTaxAmountPerDay { get; set; } = 60;
        public string CountryCodeLetter { get; set; } = "de";
    }
}
