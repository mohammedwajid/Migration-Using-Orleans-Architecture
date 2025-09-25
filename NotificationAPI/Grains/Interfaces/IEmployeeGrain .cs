using EmployeeService.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeService.Grains
{
    public interface IEmployeeGrain : IGrainWithStringKey
    {
        Task<List<Employee>> GetEmployeesForSessionAsync(Dictionary<string, string> parameters);
    }
}
