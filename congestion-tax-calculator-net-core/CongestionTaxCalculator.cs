using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;
using congestion.calculator.DomainModels;
using congestion.calculator.Enums;
using congestion.calculator.Repositories;

public class CongestionTaxCalculator
{
    /**
    * Calculate the total toll fee for one day
    *
    * @param vehicle - the vehicle
    * @param dates   - date and time of all passes on one day
    * @return - the total congestion tax for that day
    */

    private TollFreeDaysRepository _tollFreeDaysRepository;
    private CongestionTaxChargeRepository _congestionTaxChargeRepository;
    private VehicleTypeRepository _vehicleTypeRepository;
    private ConfigurationRespository _configurationRespository;

    public CongestionTaxCalculator()
    {
        _tollFreeDaysRepository = new TollFreeDaysRepository();
        _congestionTaxChargeRepository = new CongestionTaxChargeRepository();
        _vehicleTypeRepository = new VehicleTypeRepository();
        _configurationRespository = new ConfigurationRespository();
    }

    public int GetTax(VehicleType vehicleType, DateTime[] dates)
    {
        if (IsTollFreeVehicle(vehicleType))
        {
            return 0;
        }

        //Exclude toll free days (Weekends, holidays, days before holidays, july month) from working list 
        var workingListExcludedTollFreeDays = ExcludeTollFreeDays(dates);

        //Apply single charge rule on the working list
        var appliedSingleChargeList = ApplySingleChargeRule(workingListExcludedTollFreeDays);

        //apply max daily fees on the working list
        var appliedMaxDayLimit = ApplyMaxDayLimitRule(appliedSingleChargeList);

        return (int)appliedMaxDayLimit.Sum(l => l.AppliedFees);
    }

    private bool IsTollFreeVehicle(VehicleType vehicleType)
    {
        return _vehicleTypeRepository.GetTollFreeVehicleType().Select(vt => (VehicleType)vt).Contains(vehicleType);
    }

    private List<DateTime> ExcludeTollFreeDays(DateTime[] dates)
    {
        var workingList = new List<DateTime>();

        //Get all toll free days from database repository
        var excludedDays = _tollFreeDaysRepository.GetTollFreeDaysList(dates.Min().Date, dates.Max().Date);


        //iterate on given dates and exclude toll free days from them
        for (int i = 0; i < dates.Length; i++)
        {
            var tollDate = dates[i];
            if (excludedDays.Select(d=>d.Date).Contains(tollDate.Date))
            {
                continue;
            }
            else
            {
                workingList.Add(tollDate);
            }
        }
        return workingList;
    }

    private List<SingleChargeGroupItem> ApplySingleChargeRule(List<DateTime> dates)
    {
        dates = dates.OrderBy(d => d).ToList();
        var goupedDates = new List<SingleChargeGroupItem>();
        var congestionTaxChargeList = _congestionTaxChargeRepository.GetCongestionTaxChargeList();

        for (int i = 0; i < dates.Count; i++)
        {
            var tollDate = dates[i];

            //Get congestion charge by date range selection
            var selectedPeriodCharge = congestionTaxChargeList.Where(t => tollDate.TimeOfDay >= t.TimeFrom && tollDate.TimeOfDay < t.TimeTo).Single();

            //if there is no group created yet or the current iterated date after the previous one by more than 60 minutes then create new group
            if (!goupedDates.Any() || tollDate - goupedDates.Last().Series.Min(d => d.PassDateTime) > new TimeSpan(0, _configurationRespository.PeriodInMinutesBetweenDatesInSingleChargeRule, 0))
            {
                goupedDates.Add(new SingleChargeGroupItem { StartDateTime = tollDate });
                goupedDates.Last().Series.Add(new TollCharge { PassDateTime = tollDate, Amount = selectedPeriodCharge.Amount });
            }

            //else, add the current iterated value to the last created group 
            else
            {
                goupedDates.Last().Series.Add(new TollCharge { PassDateTime = tollDate, Amount = selectedPeriodCharge.Amount });
            }
        }

        foreach (var item in goupedDates)
        {
            //Calculate the applied fees be getting the max one in each series for all groups.
            item.AppliedFees = item.Series.Max(f => f.Amount);
        }

        return goupedDates;
    }

    private List<SingleDayGroupItem> ApplyMaxDayLimitRule(List<SingleChargeGroupItem> workingList)
    {
        //flatten the working list from single charge rule output.
        return workingList.Select(d => new TollCharge
        {
            PassDateTime = d.StartDateTime,
            Amount = d.AppliedFees
        })

        //group by Date, and calculate the applied fees by checking the sum of all toll in the same day, if it exceeds 60 then put 60 else put the sum value 
        .GroupBy(d => d.PassDateTime.Date).Select(l =>
        new SingleDayGroupItem
        {
            Series = l.ToList(),
            AppliedFees = l.Sum(s => s.Amount) >= _configurationRespository.MaximumChargePerDay ? _configurationRespository.MaximumChargePerDay : l.Sum(s => s.Amount),
            GroupingDate = l.Key
        }).ToList();
    }

}

