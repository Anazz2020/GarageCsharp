using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Treatment
    {
        private const int k_MaxPhoneNumberLength = 10;
        private const int k_MinPhoneNumberLength = 1;

        public enum eStatus
        {
            InTreatment = 1,
            Fixed,
            Paid,
        }

        private string  m_OwnerName;
        private string  m_OwnerPhone;
        private eStatus m_Status;
        private Vehicle m_Vehicle;

        public Treatment(Vehicle i_NewVehcile, string i_OwnerName, string i_OwnerPhone)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_Vehicle = i_NewVehcile;
            m_Status = eStatus.InTreatment;
        }

        public static bool IsValidOwnerPhone(string i_Input)
        {
            long.Parse(i_Input);
            if (i_Input.Length > 10)
            {
                throw new ValueOutOfRangeException(k_MinPhoneNumberLength, k_MaxPhoneNumberLength);
            }

            return true;
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }

            set
            {
                m_OwnerPhone = value;
            }
        }

        public eStatus Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                m_Status = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }

            set
            {
                m_Vehicle = value;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string        TreatmentDetails;

            TreatmentDetails = string.Format(
@"Owner name: {0}
Owner phone: {1}
Vehicle status: {2}
",
m_OwnerName,
m_OwnerPhone,
m_Status.ToString());
            result.Append(TreatmentDetails);
            result.Append(m_Vehicle.ToString());
            return result.ToString();
        }

        public void CheckEqualStatus(eStatus userChoice)
        {
            if ((Treatment.eStatus)userChoice == m_Status)
            {
                throw new ArgumentException(
                    string.Format(
                    "The vehicle is already in {0} status",
                    m_Status));
            }
        }
    }     
}
