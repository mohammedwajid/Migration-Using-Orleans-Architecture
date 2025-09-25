using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using EmployeeService.Models;

namespace EmployeeService.Grains
{
    public class EmployeeGrain : Grain, IEmployeeGrain
    {
        private static readonly List<Employee> Employees = new()
        {
            new Employee { code = "E101", firstname = "John", lastname = "Doe" },
            new Employee { code = "E102", firstname = "Jane", lastname = "Smith" },
            new Employee { code = "E103", firstname = "Michael", lastname = "Johnson" },
            new Employee { code = "E104", firstname = "Sara", lastname = "Williams" },
        };

        public Task<List<Employee>> GetEmployeesForSessionAsync(Dictionary<string, string> parameters)
        {
            var result = Employees.AsQueryable();

            if (parameters != null)
            {
                if (parameters.TryGetValue("firstname", out var first) && !string.IsNullOrWhiteSpace(first))
                {
                    var firstLower = first.ToLowerInvariant();
                    result = result.Where(e => !string.IsNullOrEmpty(e.firstname) &&
                                               e.firstname.ToLowerInvariant().Contains(firstLower));
                }

                if (parameters.TryGetValue("lastname", out var last) && !string.IsNullOrWhiteSpace(last))
                {
                    var lastLower = last.ToLowerInvariant();
                    result = result.Where(e => !string.IsNullOrEmpty(e.lastname) &&
                                               e.lastname.ToLowerInvariant().Contains(lastLower));
                }

                if (parameters.TryGetValue("code", out var code) && !string.IsNullOrWhiteSpace(code))
                {
                    var codeLower = code.ToLowerInvariant();
                    result = result.Where(e => !string.IsNullOrEmpty(e.code) &&
                                               e.code.ToLowerInvariant().Contains(codeLower));
                }
            }

            return Task.FromResult(result.ToList());
        }
    }
}
