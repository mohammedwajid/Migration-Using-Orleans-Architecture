using System.Collections.Generic;
using Orleans;
using Orleans.Serialization;

namespace EmployeeService.Models
{
    [GenerateSerializer]
    public sealed class DynamicGrainRequest
    {
        [Id(0)]
        public string GrainType { get; set; } = string.Empty;

        [Id(1)]
        public string GrainKey { get; set; } = string.Empty;

        // Keeping as object is flexible, but Orleans may need type info
        // You can alternatively use JsonElement for safe serialization
        [Id(2)]
        public Dictionary<string, string> Parameters { get; set; } = new();
    }
}
