using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Treatment> m_TreatmentList = new Dictionary<string, Treatment>();

        public Dictionary<string, Treatment> TreatmentList
        {
            get
            {
                return m_TreatmentList;
            }
        }

        public void ChangeStatus(string i_LicensePlate, Treatment.eStatus i_newStatus)
        {
            m_TreatmentList[i_LicensePlate].Status = i_newStatus;
        }

        public void InsertNewVehicleToTreatment(Vehicle i_NewVehicle, string i_OwnerName, string i_OwnerPhone)
        {
            Treatment newTreatmentToAdd = new Treatment(i_NewVehicle, i_OwnerName, i_OwnerPhone);

            m_TreatmentList.Add(i_NewVehicle.LicensePlate, newTreatmentToAdd);
        }

        public void IsVehicleExists(string input)
        {
            if(m_TreatmentList.ContainsKey(input) == false)
            {
                throw new ArgumentException(
                    string.Format(
                    "the vehicle with the license plate {0} doesnt exists in the garage",
                    input));
            }
        }
    }
}
