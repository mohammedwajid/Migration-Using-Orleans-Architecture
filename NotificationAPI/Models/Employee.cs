using Orleans;
using Orleans.Serialization;

namespace EmployeeService.Models
{
    [GenerateSerializer]
    public class Employee
    {
        [Id(0)]
        public string code { get; set; } = string.Empty;

        [Id(1)]
        public string firstname { get; set; } = string.Empty;

        [Id(2)]
        public string lastname { get; set; } = string.Empty;
    }
}
