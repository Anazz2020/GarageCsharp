using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private const int k_StartIndex = 1;

        public enum eUserChoice
        {
            InsertVehicleToGarage = 1,
            DisplayLicensePlates,
            ChangeVehicleStatus,
            InflateWheels,
            FillEnergySource,
            DisplayFullVehicleData,
            Exit,
        }

        private void printBeforeOptionsHeaderMsg(string i_Variable)
        {
            Console.WriteLine("Please select {0} (insert the associated number)", i_Variable);
        }

        public void PrintToScreen(string i_StringToPrint)
        {
            Console.WriteLine(i_StringToPrint);
        }

        public void PrintLicensePlates(int i_UserChoice, Dictionary<string, Treatment> i_TreatmentList, bool i_DisplayAll)
        {
            Treatment.eStatus wantedStatus = (Treatment.eStatus)i_UserChoice;

            foreach (Treatment current in i_TreatmentList.Values)
            {
                if (current.Status == wantedStatus || i_DisplayAll)
                {
                    Console.WriteLine(current.Vehicle.LicensePlate);
                }
            }
        }

        public string GetWheelManufacturer()
        {
            string input;

            do
            {
                Console.WriteLine("->Insert wheel manufacturer model name");
                input = Console.ReadLine();
            }
            while(string.IsNullOrEmpty(input));
            return input;
        }

        public string GetVehicleModelName()
        {
            string input;
            do
            {
                Console.WriteLine("->Insert vehicle model name");
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));

            return input;
        }

        public void GetVehicleLicensePlate(Garage i_Garage, out string i_LicensePlate)
        {           
            do
            {
                Console.WriteLine("->Insert vehicle license plate");
                i_LicensePlate = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(i_LicensePlate));
            i_Garage.IsVehicleExists(i_LicensePlate);
        }

        public float GetAirPressureForWheels()
        {
            string input;
            float result = 0;
            bool isValidInput = false;

            Console.WriteLine("Please enter air pressure to add the vehicle");
            do
            {
                try
                {
                    input = Console.ReadLine();
                    result = float.Parse(input);
                    isValidInput = true;
                }
                catch (FormatException)
                {
                    InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return result;
        }

        internal void SomethingWentWrongMsg()
        {
            Console.WriteLine("Something went wrong, try a smaller input");
        }

        private int GetInput<T>(string i_Message, T i_Enum, string i_OptionsHeaderMsg)
        {
            string input;
            bool isValidInput = true;
            int userChoice = 0;

            printBeforeOptionsHeaderMsg(i_OptionsHeaderMsg);
            Console.WriteLine(i_Message);

            do
            {
                if (!isValidInput)
                {
                    InvalidInputTryAgainMsg();
                }

                try
                {
                    input = Console.ReadLine();
                    userChoice = int.Parse(input);
                    isValidInput = Enum.IsDefined(typeof(T), userChoice);
                }
                catch (FormatException)
                {
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return userChoice;
        }

        public string GetOwnerPhone()
        {
            string input = null;
            bool isValidInput = true;

            Console.WriteLine("->Insert owner phone number(no more then 10 digits)");
            do
            {
                try
                {
                    input = Console.ReadLine();
                    isValidInput = Treatment.IsValidOwnerPhone(input);
                }
                catch (FormatException)
                {
                    InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
                catch (ValueOutOfRangeException ofr)
                {
                    Console.WriteLine(ofr.Message);
                    Console.WriteLine("try again");
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return input;
        }

        public string GetOwnerName()
        {
            string input;

            do
            {
                Console.WriteLine("->Insert owner full name");
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));
            return input;
        }

        public eUserChoice PrintMenuAndGetChoice()
        {
            eUserChoice userChoiceOptions = new eUserChoice();
            string      header = string.Format("from Garage Menu:");
            int         userChoice;

            userChoice = GetSpecificEnumInput(header, userChoiceOptions);
            Console.Clear();

            return (eUserChoice)userChoice;
        }

        private string buildEnumList(string[] i_enumNames)
        {
            StringBuilder result = new StringBuilder();
            int           currentIndex = k_StartIndex;

            foreach (string currentEnum in i_enumNames)
            {
                result.Append(string.Format("{0}. {1} {2}", currentIndex++, currentEnum, Environment.NewLine));
            }

            return CreateSpaceIfNeeded(result.ToString());
        }

        public string CreateSpaceIfNeeded(string i_input)
        {
            StringBuilder result = new StringBuilder();
            char previous = i_input[0];

            foreach (char currentChar in i_input)
            {
                if (currentChar >= 'A' && currentChar <= 'Z' && (previous >= 'a' && previous <= 'z'))
                {
                    result.Append(' ');
                }

                result.Append(currentChar);
                previous = currentChar;
            }

            return result.ToString();
        }

        internal void VehicleIsAlreadyExsistsMsg()
        {
            Console.WriteLine("The vehicle exsits in the system, his status changed to in treatment"); 
        }

        public int GetEngineCapacity()
        {
            string input;
            bool isValidInput = false;
            int engineCapacity = 0;

            Console.WriteLine("Please insert capacity of engine");
            do
            {
                try
                {
                    input = Console.ReadLine();
                    engineCapacity = int.Parse(input);
                    isValidInput = true;
                }
                catch (FormatException)
                {
                    InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return engineCapacity;
        }

        public int GetVolumeOfCargo()
        {
            string input;
            bool isValidInput = false;
            int volumeOfCargo = 0;

            PrintToScreen("Please insert volume of cargo");
            do
            {
                try
                {
                    input = Console.ReadLine();
                    volumeOfCargo = int.Parse(input);
                    isValidInput = true;
                }
                catch (FormatException)
                {
                    InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return volumeOfCargo;
        }

        public int GetSpecificEnumInput<T>(string i_HeaderBeforeOptions, T i_EnumInput)
        {
            string[] listOfChoice;
            int userChoice;

            listOfChoice = (string[])Enum.GetNames(typeof(T));
            userChoice = GetInput(buildEnumList(listOfChoice), i_EnumInput, i_HeaderBeforeOptions);
            return userChoice;
        }

        public void InvalidInputTryAgainMsg()
        {
            Console.WriteLine("Invalid input, try again");
        }
    }
}
