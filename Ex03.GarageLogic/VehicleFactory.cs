using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public static Vehicle CreateVehicle(
            Vehicle.eType i_TypeOfVehicle,
            EnergySource.eSource i_SourceOfEnergy,
            string i_LicensePlate,
            string i_ModelName,
            string i_WheelManufacturer)
        {
            Vehicle result = null;

            switch(i_TypeOfVehicle)
            {
                case Vehicle.eType.Motorcycle:
                    result = new Motorcycle(i_LicensePlate, i_ModelName, i_WheelManufacturer, i_SourceOfEnergy);
                    break;

                case Vehicle.eType.Car:
                    result = new Car(i_LicensePlate, i_ModelName, i_WheelManufacturer, i_SourceOfEnergy);
                    break;

                case Vehicle.eType.Truck:
                    result = new Truck(i_LicensePlate, i_ModelName, i_WheelManufacturer, i_SourceOfEnergy);
                    break;
            }

            return result;
        }
    }
}
