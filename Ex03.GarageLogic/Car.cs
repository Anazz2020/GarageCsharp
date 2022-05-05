using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_AmountOfWheels = 4;
        private const float k_MaxBatteryRunningTime = 3.2f;

        public enum eColor
        {
            Gray = 1,
            Blue,
            White,
            Black
        }

        public enum eAmountOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private eColor         m_Color;
        private eAmountOfDoors m_AmountOfDoors;

        public Car(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, EnergySource.eSource i_Source)
            : base(i_LicensePlate, i_ModelName, i_Source)
        {
            for(int i = 0; i < k_AmountOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturer, (float)Wheel.eMaxAirPressure.Car));
            }

            InitializeEnergySource();                 
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }

            set
            {
                m_Color = value;
            }
        }

        public eAmountOfDoors AmountOfDoors
        {
            get
            {
                return m_AmountOfDoors;
            }

            set
            {
                m_AmountOfDoors = value;
            }
        }

        public override void InitializeEnergySource()
        {
            if (EnergySource is GasTank)
            {
                ((GasTank)EnergySource).FuelType = GasTank.eFuelType.Octan98;
                EnergySource.MaxAmountOfEnergy = (float)GasTank.eFuelCapacity.Car;
            }
            else
            {
                EnergySource.MaxAmountOfEnergy = k_MaxBatteryRunningTime;
            }
        }

        public override string ToString()
        {
            string result;

            result = string.Format(
@"{0}
Car's Color: {1}
Car's door quantity: {2}
",
VehicleDetails(),
m_Color.ToString(),
m_AmountOfDoors.ToString());
            return result;
        }
    }
}
