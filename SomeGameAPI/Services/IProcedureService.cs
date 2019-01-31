using SomeGameAPI.Entities;
using SomeGameAPI.Models;
using System.Collections.Generic;

namespace SomeGameAPI.Services
{
    public interface IProcedureService
    {
        int StartProcedure(StartingProcedure procedure);

        bool EndProcedure(int procedureId);

        Procedure GetActivePatientProcedure(int UserId);

        void SetHeartrate(int procedureId, int heartrate);

        void SetTemperature(int procedureId, float temperature);
    }
}
