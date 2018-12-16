using SomeGameAPI.Entities;
using System.Collections.Generic;

namespace SomeGameAPI.Services
{
    public interface IDepartmentService
    {
        Department GetDepartment(int id);
    }
}
