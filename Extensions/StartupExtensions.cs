using EmployeeServiceLayer.Interfaces;
using EmployeeServiceLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE6API.Extensions
{
    public static class StartupExtensions
    {
        public static void addInterfaces(this IServiceCollection service)
        {
            service.AddTransient<IEmployee, EmployeeService>();
        }
    }
}
