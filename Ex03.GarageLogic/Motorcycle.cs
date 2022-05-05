using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int   k_AmountOfWheels = 2;
        private const float k_MaxBatteryRunningTime = 1.8f;

        public enum eLisenceType
        {
            A = 1,
            A1,
            B1,
            B2,
        }

        private eLisenceType m_LicenseType;
        private float        m_EngineCapacity;

        public Motorcycle(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, EnergySource.eSource i_Source)
            : base(i_LicensePlate, i_ModelName, i_Source)
        {
            for (int i = 0; i < k_AmountOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturer, (float)Wheel.eMaxAirPressure.Motorcycle));
            }

            InitializeEnergySource();
        }

        public eLisenceType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        public float EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }

            set
            {
                m_EngineCapacity = value;
            }
        }

        public override void InitializeEnergySource()
        {
            if(EnergySource is GasTank)
            {
                ((GasTank)EnergySource).FuelType = GasTank.eFuelType.Octan96;
                EnergySource.MaxAmountOfEnergy = (float)GasTank.eFuelCapacity.Motorcycle;
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
Motorcycle's license type: {1}
Motorcycle's engine cpacity: {2}",
VehicleDetails(),
m_LicenseType.ToString(),
m_EngineCapacity);
            return result;
        }
    }
}
