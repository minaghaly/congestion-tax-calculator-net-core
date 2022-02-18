using congestion.calculator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace congestion_tax_calculator_net_core.web_api.Models
{
    public class CalculateCongestionTaxRequestDto
    {
        public VehicleType VehicleType { get; set; }
        public DateTime[] Dates { get; set; }
    }
}
