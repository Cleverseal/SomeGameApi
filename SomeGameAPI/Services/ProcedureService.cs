using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SomeGameAPI.Entities;
using SomeGameAPI.Helpers;
using SomeGameAPI.Models;

namespace SomeGameAPI.Services
{
    public class ProcedureService : IProcedureService
    {
        private readonly DataContext context;

        private readonly AppSettings appSettings;

        public ProcedureService(DataContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        public int StartProcedure(StartingProcedure startingProcedure)
        {
            var procedure = new Procedure
            {
                Id = context.TestingProcedures.Max(x => x.Id) + 1,
                DateTime = DateTime.Now,
                Comments = startingProcedure.Comments,
                PatientId = startingProcedure.PatientId,
                UserId = startingProcedure.UserId,
                Status = "Active"
            };

            this.context.Add(procedure);
            this.context.SaveChanges();
            return procedure.Id;
        }

        public bool EndProcedure(int procedureId)
        {
            var procedure = this.context.TestingProcedures.FirstOrDefault(x => x.Id == procedureId);
            return procedure != null;
        }

        public List<Procedure> GetAllPatientProcedures(int PatientId)
        {
            return this.context.TestingProcedures.Where(x => x.PatientId == PatientId).ToList();
        }
    }
}
