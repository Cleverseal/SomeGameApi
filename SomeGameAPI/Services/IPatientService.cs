using SomeGameAPI.Entities;
using System.Collections.Generic;

namespace SomeGameAPI.Services
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);

        Patient GetPatient(int id);

        List<Patient> GetPatientsList();

        bool UpdatePatient(Patient patient);

        bool DeletePatient(int id);
    }
}
