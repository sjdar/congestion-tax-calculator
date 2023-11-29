using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CongestionTaxCalculator.WeApi
{
    [ApiController]
    [Route("api/v1/")]
    public class ApiControllerBase:ControllerBase
    {
        private readonly IConfiguration _configuration;

        protected ApiControllerBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }


    }
}
