//using Amazon.SQS;
//using Amazon.SQS.Model;
//using Microsoft.Extensions.Hosting;
//using Newtonsoft.Json;
//using System.Threading;
//using System.Threading.Tasks;
//using Orleans;
//using DynamicNotification.Api.Models;
//using DynamicNotification.Api.Grains.Interfaces;
//using DynamicNotification.Api.Grains;

//namespace DynamicNotification.Api.Services
//{
//    public class SqsWorker : BackgroundService
//    {
//        private readonly IAmazonSQS _sqs;
//        private readonly string _queueUrl;
//        private readonly IGrainFactory _grainFactory;

//        public SqsWorker(IAmazonSQS sqs, IConfiguration config, IGrainFactory grainFactory)
//        {
//            _sqs = sqs;
//            _queueUrl = config["Sqs:QueueUrl"] ?? Environment.GetEnvironmentVariable("SQS_QUEUE_URL");
//            _grainFactory = grainFactory;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                var resp = await _sqs.ReceiveMessageAsync(new ReceiveMessageRequest
//                {
//                    QueueUrl = _queueUrl,
//                    MaxNumberOfMessages = 10,
//                    WaitTimeSeconds = 20,
//                    VisibilityTimeout = 60
//                }, stoppingToken);

//                if (resp.Messages?.Any() == true)
//                {
//                    foreach (var m in resp.Messages)
//                    {
//                        try
//                        {
//                            var payload = JsonConvert.DeserializeObject<DynamicGrainRequest>(m.Body);
//                            if (payload != null)
//                            {
//                                var grain = _grainFactory.GetGrain<IDynamicGrain>(payload.GrainKey ?? payload.GrainType);

//                                var grainRequest = new DynamicGrainRequest
//                                {
//                                    //MethodName = "SendNotification",
//                                    Parameters = new Dictionary<string, object>
//                                    {
//                                        { "SessionRef", payload.SessionRef },
//                                        { "GrainType", payload.GrainType },
//                                        { "GrainKey", payload.GrainKey },
//                                        { "MethodName", payload.MethodName }
//                                        //{ "Metadata", messageMetadata }
//                                    }
//                                };

//                                await grain.InvokeDynamicMethod(grainRequest);
//                            }

//                            await _sqs.DeleteMessageAsync(_queueUrl, m.ReceiptHandle, stoppingToken);
//                        }
//                        catch (Exception ex)
//                        {
//                            // log and optionally move to DLQ
//                            Console.WriteLine($"SQS processing error: {ex}");
//                        }
//                    }
//                }
//            }
//        }
//    }
//}


//new

using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class SqsWorker : BackgroundService
{
    private readonly ILogger<SqsWorker> _logger;
    private readonly IAmazonSQS _sqsClient;
    private readonly string _queueUrl = "https://sqs.us-east-1.amazonaws.com/123456789012/MyQueue";

    public SqsWorker(ILogger<SqsWorker> logger, IAmazonSQS sqsClient)
    {
        _logger = logger;
        _sqsClient = sqsClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SQS Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 5,
                WaitTimeSeconds = 10
            }, stoppingToken);

            foreach (var message in response.Messages)
            {
                _logger.LogInformation("Received SQS message: {Body}", message.Body);

                // TODO: Dispatch to a grain
                // var grain = grainFactory.GetGrain<IMyGrain>("some-key");
                // await grain.ProcessMessage(message.Body);

                // delete after processing
                await _sqsClient.DeleteMessageAsync(_queueUrl, message.ReceiptHandle, stoppingToken);
            }
        }
    }
}
