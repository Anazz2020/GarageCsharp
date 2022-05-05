using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eType
        {
            Motorcycle = 1,
            Car,
            Truck,            
        }

        private readonly string       m_ModelName;
        private readonly string       m_LicensePlate;
        private readonly EnergySource m_EnergySource;
        private float                 m_EnergyPercent;
        private List<Wheel>           m_Wheels;

        public Vehicle(string i_LicensePlate, string i_ModelName, EnergySource.eSource i_Source)
        {
            m_ModelName = i_ModelName;
            m_LicensePlate = i_LicensePlate;
            m_Wheels = new List<Wheel>();

            if(i_Source == EnergySource.eSource.Battery)
            {
                m_EnergySource = new Battery();
            }
            else
            {
                m_EnergySource = new GasTank();
            }
        }
        
        public string LicensePlate
        {
            get
            {
                return m_LicensePlate;
            }
        }

        public float EnergyPercent
        {
            get
            {
                return m_EnergyPercent;
            }

            set
            {
                m_EnergyPercent = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }

            set
            {
                m_Wheels = value;
            }
        }
        
        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }

        public abstract void InitializeEnergySource();

        public string VehicleDetails()
        {
            string result;

            result = string.Format(
@"Vehicel license plate: {0}
Vehicel model name: {1}
Wheels information: 
{2}
Energy meter: {3}%
{4}",
m_LicensePlate,
m_ModelName,
m_Wheels[0].ToString(),
m_EnergyPercent,
m_EnergySource.ToString());
            return result;         
        }

        public void UpdateEnergyPercent()
        {
            EnergyPercent = (EnergySource.CurrentAmountOfEnergy / EnergySource.MaxAmountOfEnergy) * 100;
        }

        public abstract override string ToString();      
    }
}
