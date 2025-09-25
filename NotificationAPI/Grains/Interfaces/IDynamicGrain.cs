using Orleans;
using System.Threading.Tasks;
using EmployeeService.Models;

namespace DynamicOrleansWebApi.Grains
{
    public interface IDynamicGrain : IGrainWithStringKey
    {
        public Task<DynamicGrainResponse> InvokeDynamicMethod(DynamicGrainRequest request);
    }
}
