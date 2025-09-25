using Microsoft.AspNetCore.Mvc;
using Amazon.SQS;
using System.Text.Json;
using EmployeeService.Models;

[ApiController]
[Route("api/[controller]")]
public class SqsController : ControllerBase
{
    private readonly IAmazonSQS _sqs;
    private readonly string _queueUrl;

    public SqsController(IAmazonSQS sqs, IConfiguration config)
    {
        _sqs = sqs;
        _queueUrl = config["Sqs:QueueUrl"] ?? Environment.GetEnvironmentVariable("SQS_QUEUE_URL") ?? throw new Exception("SQS queue required");
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] DynamicGrainRequest request)
    {
        var body = JsonSerializer.Serialize(request);
        await _sqs.SendMessageAsync(_queueUrl, body);
        return Ok(new { status = "queued" });
    }
}
