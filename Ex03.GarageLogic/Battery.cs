using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Battery : EnergySource
    {
        public override string CreateGetEnergyMsg()
        {
            return "Please enter the amount of battery time you want to add";
        }

        public override string CreateOutOfRangMsg()
        {
            string result;

            result = string.Format(
@"your battery was about to get charged inappropriately,
your battery has {0} hour's of runuing time left
and at most you can charge the battery up to {1} hour's.",
CurrentAmountOfEnergy,
MaxAmountOfEnergy);
            return result;
        }

        public override string ToString()
        {
            return string.Format(
@"Battery running time left : {0}
Max battery running time : {1}",
CurrentAmountOfEnergy,
MaxAmountOfEnergy);
        }
    }
}
