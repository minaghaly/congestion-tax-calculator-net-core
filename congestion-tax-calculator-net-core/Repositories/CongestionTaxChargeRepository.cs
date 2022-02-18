using congestion.calculator.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator
{
    public class CongestionTaxChargeRepository
    {
        public List<CongestionPerioedCharge> GetCongestionTaxChargeList()
        {
            var congestionPerioedCharge = new List<CongestionPerioedCharge>();
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(0, 0, 0), TimeTo = new TimeSpan(5, 59, 59), Amount = 0 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(6, 0, 0), TimeTo = new TimeSpan(6, 29, 59), Amount = 8 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(6, 30, 0), TimeTo = new TimeSpan(6, 59, 59), Amount = 13 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(7, 0, 0), TimeTo = new TimeSpan(7, 59, 59), Amount = 18 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(8, 0, 0), TimeTo = new TimeSpan(8, 29, 59), Amount = 13 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(8, 30, 0), TimeTo = new TimeSpan(14, 59, 59), Amount = 8 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(15, 0, 0), TimeTo = new TimeSpan(15, 29, 59), Amount = 13 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(15, 30, 0), TimeTo = new TimeSpan(16, 59, 59), Amount = 18 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(17, 0, 0), TimeTo = new TimeSpan(17, 59, 59), Amount = 13 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(18, 0, 0), TimeTo = new TimeSpan(18, 29, 59), Amount = 8 });
            congestionPerioedCharge.Add(new CongestionPerioedCharge { TimeFrom = new TimeSpan(18, 30, 0), TimeTo = new TimeSpan(23, 59, 59), Amount = 0 });
            return congestionPerioedCharge;
        }
    }
}
