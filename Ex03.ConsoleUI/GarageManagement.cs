using System;
using System.Collections.Generic;
using System.Text;

using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageManagement
    {
        public enum eDisplayOption
        {
            AllVehicles = 1,
            SpecificStatus,
        }

        // $G$ DSN-999 (-3) m_Garage should be readonly. 
        private Garage         m_Garage = new Garage();
        private UserInterface  m_UI = new UserInterface();

        public Garage Garage
        {
            get
            {
                return m_Garage;
            }

            set
            {
                m_Garage = value;
            }
        }

        public UserInterface UI
        {
            get
            {
                return m_UI;
            }

            set
            {
                m_UI = value;
            }
        }

        public void Run()
        {
            UserInterface.eUserChoice userChoice;
            bool                      exitProgram = false;

            while (!exitProgram)
            {
                UI.PrintToScreen(Environment.NewLine);
                try
                {
                    userChoice = UI.PrintMenuAndGetChoice();
                    switch (userChoice)
                    {
                        case UserInterface.eUserChoice.InsertVehicleToGarage:
                            insertVehicleToGarage();
                            break;

                        case UserInterface.eUserChoice.DisplayLicensePlates:
                            displayVehiclesInGarageLicensePlates();
                            break;

                        case UserInterface.eUserChoice.ChangeVehicleStatus:
                            changeVehicleTreatmentStatus();
                            break;

                        case UserInterface.eUserChoice.InflateWheels:
                            inflateWheelsToMax();
                            break;

                        case UserInterface.eUserChoice.FillEnergySource:
                            fillEnergySource();
                            break;

                        case UserInterface.eUserChoice.DisplayFullVehicleData:
                            displayFullVehicleData();
                            break;

                        case UserInterface.eUserChoice.Exit:
                            exitProgram = true;
                            break;

                        default:
                            UI.InvalidInputTryAgainMsg();
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    UI.PrintToScreen(ae.Message);
                }
                catch (FormatException)
                {
                    UI.InvalidInputTryAgainMsg();
                }
                catch (OverflowException)
                {
                    UI.SomethingWentWrongMsg();
                }
            }
        }

        private void displayFullVehicleData()
        {
            string licensePlate;

            UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            UI.PrintToScreen(UI.CreateSpaceIfNeeded(m_Garage.TreatmentList[licensePlate].ToString()));           
        }

        private void fillEnergySource()
        {
            string            licensePlate;
            GasTank.eFuelType fuelOptions = new GasTank.eFuelType();
            string            PartOfOptionsHeaderMsg = string.Format("fuel type");
            GasTank.eFuelType fuelType;

            UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            GasTank gasTank = m_Garage.TreatmentList[licensePlate].Vehicle.EnergySource as GasTank;
            if (gasTank != null)
            {
                fuelType = (GasTank.eFuelType)UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, fuelOptions);
                gasTank.CheckFuelType(fuelType);
            }

            insertAmountOfEnergyToAdd(m_Garage.TreatmentList[licensePlate].Vehicle);
        }

        private void inflateWheelsToMax()
        {
            string licensePlate;

            UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            foreach (Wheel currentWheel in m_Garage.TreatmentList[licensePlate].Vehicle.Wheels)
            {
                currentWheel.UpdateAirPressure(currentWheel.MaxAirPressure - currentWheel.CurrentAirPressure);
            }
        }

        private void changeVehicleTreatmentStatus()
        {
            int               userChoice;
            string            licensePlate;
            Treatment.eStatus statusOptions = new Treatment.eStatus();
            string            PartOfOptionsHeaderMsg = string.Format("to which treatment status you want to change");

            UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            userChoice = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, statusOptions);

            m_Garage.TreatmentList[licensePlate].CheckEqualStatus((Treatment.eStatus)userChoice);
            m_Garage.TreatmentList[licensePlate].Status = (Treatment.eStatus)userChoice;
        }

        private void displayVehiclesInGarageLicensePlates()
        {
            Treatment.eStatus statusOptions = new Treatment.eStatus();
            eDisplayOption    displayOption = new eDisplayOption();
            int               displayChoice;
            int               userChoice = 0;
            string            displayOptionMsg = string.Format("how to want to filter your search");
            string            PartOfOptionsHeaderMsg = string.Format("which vehicels you want to see");
            bool              displayAll = false;

            displayChoice = UI.GetSpecificEnumInput(displayOptionMsg, displayOption);
            if ((eDisplayOption)displayChoice == eDisplayOption.AllVehicles)
            {
                displayAll = true;
            }
            else
            {
                userChoice = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, statusOptions);
            }

            UI.PrintLicensePlates(userChoice, m_Garage.TreatmentList, displayAll);          
        }
     
        private void insertVehicleToGarage()
        {
            bool    isVehicleExists = true;
            string  licensePlate = null, modelName, wheelManufacturer, ownerName, ownerPhone;
            Vehicle newVehicle;

            try
            {
                UI.GetVehicleLicensePlate(m_Garage, out licensePlate);
            }
            catch (ArgumentException)
            {
                isVehicleExists = false;
            }

            if(isVehicleExists)
            {
                UI.VehicleIsAlreadyExsistsMsg();
                m_Garage.ChangeStatus(licensePlate, Treatment.eStatus.InTreatment);
            }
            else
            {
                modelName = UI.GetVehicleModelName();
                wheelManufacturer = UI.GetWheelManufacturer();
                newVehicle = createNewVehicle(licensePlate, modelName, wheelManufacturer, out ownerName, out ownerPhone);
                m_Garage.InsertNewVehicleToTreatment(newVehicle, ownerName, ownerPhone);
            } 
        }
                 
        private Vehicle createNewVehicle(
            string i_LicensePlate,
            string i_ModelName,
            string i_WheelManufacturer,
            out string o_OwnerName,
            out string o_OwnerPhone)
        {
            Vehicle              newVehicle;
            Vehicle.eType        vehicleType, vehicleOptions = new Vehicle.eType();
            EnergySource.eSource vehicleEnergySource;
            EnergySource.eSource energySourceOptions = new EnergySource.eSource();
            string               PartOfOptionsHeaderMsg = string.Format("vehicle type");

            o_OwnerName = UI.GetOwnerName();
            o_OwnerPhone = UI.GetOwnerPhone();
            
            vehicleType = (Vehicle.eType)UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, vehicleOptions);
            PartOfOptionsHeaderMsg = string.Format("energy source");
            if (vehicleType != Vehicle.eType.Truck)
            {
                vehicleEnergySource = (EnergySource.eSource)UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, energySourceOptions);
            }
            else
            {
                vehicleEnergySource = EnergySource.eSource.GasTank;
            }

            newVehicle = VehicleFactory.CreateVehicle(vehicleType, vehicleEnergySource, i_LicensePlate, i_ModelName, i_WheelManufacturer);

            insertVehicleDetails(newVehicle);

            return newVehicle;           
        }

        private void insertVehicleDetails(Vehicle i_NewVehicle)
        {
            if (i_NewVehicle is Motorcycle)
            {
                insertLicenseType((Motorcycle)i_NewVehicle);
                insertEngineCapacity((Motorcycle)i_NewVehicle);
            }
            else if (i_NewVehicle is Car)
            {
                insertColor((Car)i_NewVehicle);
                insertAmountOfDoors((Car)i_NewVehicle);
            }
            else
            {
                insertIfCooledCargo((Truck)i_NewVehicle);
                insertVolumeOfCargo((Truck)i_NewVehicle);
            }

            insertCurrrentAirPressureOfWheels(i_NewVehicle);
            insertAmountOfEnergyToAdd(i_NewVehicle);
        }

        private void insertAmountOfEnergyToAdd(Vehicle i_NewVehicle)
        {
            string input;
            float AmountOfEnergyToEnter;
            bool isValidInput = true;

            UI.PrintToScreen(i_NewVehicle.EnergySource.CreateGetEnergyMsg());
            do
            {
                try
                {
                    input = Console.ReadLine();
                    AmountOfEnergyToEnter = float.Parse(input);
                    i_NewVehicle.EnergySource.UpdateEnergy(AmountOfEnergyToEnter);
                    isValidInput = true;
                }
                catch (FormatException)
                {
                    UI.InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
                catch (ValueOutOfRangeException)
                {
                    UI.PrintToScreen(string.Format(
@"{0}
please enter the amount to add again",
i_NewVehicle.EnergySource.CreateOutOfRangMsg()));
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            i_NewVehicle.UpdateEnergyPercent();
        } 

        private void insertIfCooledCargo(Truck i_NewTruck)
        {
            int userChoice;
            Truck.eCargo cargoOptions = new Truck.eCargo();
            string       PartOfOptionsHeaderMsg = string.Format("cargo type");

            userChoice = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, cargoOptions);
            if(userChoice == (int)Truck.eCargo.Cooled)
            {
                i_NewTruck.isCooled = true;
            }
            else
            {
                i_NewTruck.isCooled = false;
            }
        }

        private void insertCurrrentAirPressureOfWheels(Vehicle i_NewVehicle)
        {
            bool  isValid = true;
            float airPressureToAdd;

            do
            {
                airPressureToAdd = UI.GetAirPressureForWheels();
                try
                {
                    foreach (Wheel currentWheel in i_NewVehicle.Wheels)
                    {
                        currentWheel.UpdateAirPressure(airPressureToAdd);                      
                    }

                    isValid = true;
                }
                catch (ValueOutOfRangeException)
                {
                    UI.PrintToScreen(string.Format(
@"Air perssure isn't in correct range,
now the pressure is {0} and at most for this vehcile is {1}",
    i_NewVehicle.Wheels[0].CurrentAirPressure,
    i_NewVehicle.Wheels[0].MaxAirPressure));
                    isValid = false;
                }
            }
            while (!isValid);
        }
      
        private void insertVolumeOfCargo(Truck i_NewTruck)
        {
            int volumeOfCargo;

            volumeOfCargo = UI.GetVolumeOfCargo();
            i_NewTruck.VolumeOfCargo = volumeOfCargo; 
        }

        private void insertAmountOfDoors(Car i_NewCar)
        {
            int                userChoice;
            Car.eAmountOfDoors amountOfDoorsOptions = new Car.eAmountOfDoors();
            string             PartOfOptionsHeaderMsg = string.Format("amount of doors");

            userChoice = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, amountOfDoorsOptions);
            i_NewCar.AmountOfDoors = (Car.eAmountOfDoors)userChoice;
        }

        private void insertColor(Car i_NewCar)
        {
            int        color;
            Car.eColor colorOptions = new Car.eColor();
            string     PartOfOptionsHeaderMsg = string.Format("car's color");

            color = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, colorOptions);
            i_NewCar.Color = (Car.eColor)color;
        }

        private void insertEngineCapacity(Motorcycle i_NewMotorcycle)
        {
            int engineCapacity;

            engineCapacity = UI.GetEngineCapacity();
            i_NewMotorcycle.EngineCapacity = engineCapacity;
        }

        private void insertLicenseType(Motorcycle i_NewMotorcycle)
        {
            int                     licenseType;
            Motorcycle.eLisenceType licenseOptions = new Motorcycle.eLisenceType();
            string                  PartOfOptionsHeaderMsg = string.Format("license type");

            licenseType = UI.GetSpecificEnumInput(PartOfOptionsHeaderMsg, licenseOptions);
            i_NewMotorcycle.LicenseType = (Motorcycle.eLisenceType)licenseType;
        }                        
    }
}
