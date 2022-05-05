using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public enum eCargo
        {
            Cooled = 1,
            NotCooled,
        }

        private const int k_AmountOfWheels = 12;

        private bool      m_isCooled;
        private float     m_VolumeOfCargo;

        public Truck(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, EnergySource.eSource i_Source)
            : base(i_LicensePlate, i_ModelName, i_Source)
        {
            for (int i = 0; i < k_AmountOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturer, (float)Wheel.eMaxAirPressure.Truck));
            }

            InitializeEnergySource();
        }

        public bool isCooled
        {
            get
            {
                return m_isCooled;
            }

            set
            {
                m_isCooled = value;
            }
        }

        public float VolumeOfCargo
        {
            get
            {
                return m_VolumeOfCargo;
            }

            set
            {
                m_VolumeOfCargo = value;
            }
        }

        public override void InitializeEnergySource()
        {
            ((GasTank)EnergySource).FuelType = GasTank.eFuelType.Soler;
            EnergySource.MaxAmountOfEnergy = (float)GasTank.eFuelCapacity.Truck;
        }

        public override string ToString()
        {
            string result;

            result = string.Format(
@"{0}
Is the truck cargo cooled: {1}
Truck's volume of cargo is: {2}",
VehicleDetails(),
m_isCooled,
m_VolumeOfCargo);
            return result;
        }
    }
}
