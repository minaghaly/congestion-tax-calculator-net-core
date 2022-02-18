using congestion_tax_calculator_net_core.web_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace congestion_tax_calculator_net_core.web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CongestionTaxController : ControllerBase
    {
        private readonly ILogger<CongestionTaxController> _logger;
        private CongestionTaxCalculator _congestionTaxCalculator;

        public CongestionTaxController(ILogger<CongestionTaxController> logger, CongestionTaxCalculator congestionTaxCalculator)
        {
            _logger = logger;
            _congestionTaxCalculator = congestionTaxCalculator;
        }

        [HttpPost("CalculateTax")]
        public int CalculateTax(CalculateCongestionTaxRequestDto model)
        {
            return _congestionTaxCalculator.GetTax(model.VehicleType, model.Dates);
        }
    }
}
