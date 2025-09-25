using DynamicOrleansWebApi.Grains;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Models;

namespace DynamicOrleansWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DynamicGrainController : ControllerBase
    {
        private readonly IClusterClient _clusterClient;

        public DynamicGrainController(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        [HttpPost("invoke")]
        public async Task<IActionResult> Invoke([FromBody] DynamicGrainRequest request)
        {
            if (request == null) return BadRequest("Request cannot be null");

            try
            {
                var grain = _clusterClient.GetGrain<IDynamicGrain>("dynamic");
                var result = await grain.InvokeDynamicMethod(request);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
