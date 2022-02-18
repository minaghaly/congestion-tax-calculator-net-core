using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator.DomainModels
{
    public class CongestionPerioedCharge
    {
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

        public decimal Amount { get; set; }
    }
}
