using SomeGameAPI.Entities;
using SomeGameAPI.Models;
using System.Collections.Generic;

namespace SomeGameAPI.Services
{
    public interface IProcedureService
    {
        int StartProcedure(StartingProcedure procedure);

        bool EndProcedure(int procedureId);

        List<Procedure> GetAllPatientProcedures(int UserId);
    }
}
