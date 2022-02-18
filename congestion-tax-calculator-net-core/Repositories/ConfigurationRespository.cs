using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator.Repositories
{
    public class ConfigurationRespository
    {
        public int PeriodInMinutesBetweenDatesInSingleChargeRule { get; set; }
        public decimal MaximumChargePerDay { get; set; }

        public ConfigurationRespository()
        {
            PeriodInMinutesBetweenDatesInSingleChargeRule = 60;
            MaximumChargePerDay = 60;
        }
    }
}
