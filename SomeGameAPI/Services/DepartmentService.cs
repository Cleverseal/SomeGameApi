using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SomeGameAPI.Entities;
using SomeGameAPI.Helpers;

namespace SomeGameAPI.Services
{
    class DepartmentService : IDepartmentService
    {
        private readonly DataContext context;

        private readonly AppSettings appSettings;

        public DepartmentService(DataContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        public Department GetDepartment(int id)
        {
            return context.Departments.FirstOrDefault(x => x.Id == id);
        }
    }
}
