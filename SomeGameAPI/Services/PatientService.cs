using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SomeGameAPI.Entities;
using SomeGameAPI.Helpers;
using SomeGameAPI.Models;

namespace SomeGameAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly DataContext context;

        private readonly AppSettings appSettings;

        public PatientService(DataContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        public void AddPatient(Patient patient)
        {
            patient.Id = this.context.Patients.Max(x => x.Id) + 1;
            this.context.Add(patient);
            this.context.SaveChanges();
        }

        public bool DeletePatient(int id)
        {
            var patient = this.context.Patients.FirstOrDefault(x => x.Id == id);
            if (patient != null)
            {
                this.context.Remove(patient);
                this.context.SaveChanges();
                return true;
            }

            return false;
        }

        public Patient GetPatient(int id)
        {
            var patient = this.context.Patients.FirstOrDefault(x => x.Id == id);
            return patient;
        }

        public List<Patient> GetPatientsList()
        {
            return this.context.Patients.ToList();
        }

        public bool UpdatePatient(Patient patient)
        {
            var exist = this.context.Patients.FirstOrDefault(x => x.Id == patient.Id);
            if (exist != null)
            {
                exist.FirstName = patient.FirstName;
                exist.LastName = patient.LastName;
                exist.Diagnosis = patient.Diagnosis;
                exist.DiagnosisDetails = patient.DiagnosisDetails;
                exist.Birthday = patient.Birthday;
                this.context.Update(exist);
                this.context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
