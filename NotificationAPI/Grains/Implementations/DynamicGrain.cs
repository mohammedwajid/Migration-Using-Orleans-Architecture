using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Orleans;
using EmployeeService.Models;
using EmployeeService.Grains;


namespace DynamicOrleansWebApi.Grains
{
    public class DynamicGrain : Grain, IDynamicGrain
    {
        public async Task<DynamicGrainResponse> InvokeDynamicMethod(DynamicGrainRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.GrainType)) throw new ArgumentException("GrainType is required");
            if (string.IsNullOrWhiteSpace(request.GrainKey)) throw new ArgumentException("GrainKey (method name) is required");

            try
            {
                switch (request.GrainType.ToLowerInvariant())
                {
                    case "employeegrain":
                    case "employee":
                        return await EmployeeMethods(request.GrainKey, request.Parameters);

                    default:
                        return new DynamicGrainResponse
                        {
                            Success = false,
                            ErrorMessage = $"GrainType '{request.GrainType}' not implemented"
                        };
                }
            }
            catch (Exception ex)
            {
                return new DynamicGrainResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private async Task<DynamicGrainResponse> EmployeeMethods(string methodName, Dictionary<string, string>? parameters)
        {
            var employeeGrain = GrainFactory.GetGrain<IEmployeeGrain>("employee-service");

            switch (methodName.ToLowerInvariant())
            {
                case "getemployeesforsessionasync":
                case "search":
                    var employees = await employeeGrain.GetEmployeesForSessionAsync(parameters ?? new());
                    return new DynamicGrainResponse
                    {
                        Success = true,
                        ResultJson = JsonSerializer.Serialize(employees)
                    };



                default:
                    return new DynamicGrainResponse
                    {
                        Success = false,
                        ErrorMessage = $"Employee method '{methodName}' not implemented"
                    };
            }
        }
}
}
