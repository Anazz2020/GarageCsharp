using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    // $G$ DSN-999 (-5) The "tank refill" function sinature is not
    // as instructed in the exercise , the middle of page 2 
    // should get fueltype and you marge that function with Battery class
    public class GasTank : EnergySource
    {
        public enum eFuelType
        {
            Octan98 = 1,
            Octan96,
            Octan95,
            Soler,
        }

        public enum eFuelCapacity
        {
            Motorcycle = 6,
            Car = 45,
            Truck = 115,
        }

        private eFuelType m_FuelType;

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public override string CreateGetEnergyMsg()
        {
            return "Please enter the amount of fuel you want to add";
        }

        public override string CreateOutOfRangMsg()
        {
            string result;

            result = string.Format(
@"Amount of fuel in the gas tank was about to go out of range
you have {0} liters of gas in your gas tank at this moment 
and at most you can fill up to {1} liters.",
CurrentAmountOfEnergy,
MaxAmountOfEnergy);
            return result;
        }
        
        public override string ToString()
        {
            return string.Format(
@"Current amount of fuel : {0}
Max amount of fuel : {1}
Fuel Type: {2}",
CurrentAmountOfEnergy,
MaxAmountOfEnergy,
m_FuelType);                                                            
        }

        public void CheckFuelType(eFuelType fuelType)
        {
            if (fuelType != m_FuelType)
            {
                throw new ArgumentException(
                    string.Format(
                    "You enterd an improper fuel type, {0} insted of {1}",
                    fuelType,
                    m_FuelType));
            }
        }
    }
}
