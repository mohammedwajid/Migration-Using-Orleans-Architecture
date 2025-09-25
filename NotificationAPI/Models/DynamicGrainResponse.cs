using System.Collections.Generic;
using Orleans;
using Orleans.Serialization;

namespace EmployeeService.Models
{
    [GenerateSerializer]
    public class DynamicGrainResponse
    {
        /// <summary>
        /// Whether the request was successful.
        /// </summary>
        [Id(0)]
        public bool Success { get; set; }

        /// <summary>
        /// Human-readable status or error message.
        /// </summary>

        [Id(1)]
        public string ErrorMessage { get; set; }

        [Id(2)]
        public string? ResultJson { get; set; }

    }
}
