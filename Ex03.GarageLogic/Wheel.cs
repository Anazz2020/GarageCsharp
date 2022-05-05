using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public enum eMaxAirPressure
        {
            Motorcycle = 30,
            Car = 32,
            Truck = 28,
        }

        private readonly string m_Manufacturer;
        private float           m_CurrentAirPressure = 0;
        private float           m_MaxAirPressure;
        
        public Wheel(string i_Manufacturer, float i_MaxAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }

            set
            {
                m_MaxAirPressure = value;
            }
        }

        public void UpdateAirPressure(float airPressureToAdd)
        {
            if(CurrentAirPressure + airPressureToAdd > MaxAirPressure || CurrentAirPressure + airPressureToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure);
            }

            CurrentAirPressure += airPressureToAdd;
        }

        public override string ToString()
        {
            string result;

            result = string.Format(
@"Air pressure: {0}
Manufacturer: {1}",
m_CurrentAirPressure,
m_Manufacturer);

            return result;
        }
    }
}
