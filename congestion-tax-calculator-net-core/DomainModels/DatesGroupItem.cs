using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator.DomainModels
{
    public class DatesGroupItem
    {
        public DatesGroupItem()
        {
            Series = new List<TollCharge>();
        }
        public List<TollCharge> Series { get; set; }
        public decimal AppliedFees { get; set; }

    }
}
