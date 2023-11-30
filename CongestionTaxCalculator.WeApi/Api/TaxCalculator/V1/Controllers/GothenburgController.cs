using CongestionTaxCalculator.WeApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;
using CongestionTaxCalculator.Dto.Enums;
using System;
using System.Linq;

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
            if(taxCalculator.DateTimes.Where(x=>x.Year<2013 || x.Year>2013).Any()) return BadRequest("Only Insert 2013 Date");    
            if (!Enum.IsDefined(typeof(VehicelTypes), taxCalculator.VehicelTypes))
               return BadRequest("VehicelType Is Out Of Range");
            var result = await congestionTax.GetTax(taxCalculator.VehicelTypes , taxCalculator.DateTimes);
            logger.LogInformation("response to request {@GetTax}", result);
            return Ok(result);
        }


    }
}
