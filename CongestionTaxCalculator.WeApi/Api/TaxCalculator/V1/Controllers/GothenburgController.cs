using CongestionTaxCalculator.WeApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.WeApi.Api.V1.Controllers
{
    public class GothenburgController : ApiControllerBase
    {
        private readonly ILogger<GothenburgController> logger;
        private readonly ICongestionTaxCalculatorService congestionTax;
        public GothenburgController(IConfiguration configuration, ILogger<GothenburgController> logger, ICongestionTaxCalculatorService icongestionTax) : base(configuration)
        {
            this.logger = logger;
            this.congestionTax = icongestionTax;
        }


        [HttpPost("CalculateTax")]
        public async Task<IActionResult> CalculateTaxAsync(TaxCalculatorDto taxCalculator)
        {
            logger.LogInformation($"received request {taxCalculator}");
            var result = await congestionTax.GetTax(taxCalculator.VehicelTypes , taxCalculator.DateTimes);
            logger.LogInformation("response to request {@GetTax}", result);
            return Ok(result);
        }


    }
}
