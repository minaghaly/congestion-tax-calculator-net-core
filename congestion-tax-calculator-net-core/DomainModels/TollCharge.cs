using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator.DomainModels
{
    public class TollCharge
    {
        public DateTime PassDateTime { get; set; }
        public decimal Amount { get; set; }
    }
}
