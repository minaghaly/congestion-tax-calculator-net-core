using congestion.calculator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace congestion.calculator.Repositories
{
    public class VehicleTypeRepository
    {
        public List<TollFreeVehicleType> GetTollFreeVehicleType()
        {
            return Enum.GetValues(typeof(TollFreeVehicleType)).Cast<TollFreeVehicleType>().ToList();
        }
    }
}
