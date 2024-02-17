namespace ParallelHandler;

class Handler: IHandler
{
    private readonly IConsumer _consumer;
    private readonly IPublisher _publisher;
    private readonly ILogger<Handler> _logger;
   
    public TimeSpan Timeout { get; }
   
    public Handler(
        TimeSpan timeout,
        IConsumer consumer,
        IPublisher publisher,
        ILogger<Handler> logger)
    {
        Timeout = timeout;
     
        _consumer = consumer;
        _publisher = publisher;   
        _logger = logger;
    }
   
    public async Task PerformOperation(CancellationToken cancellationToken)
    {
        await Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var (recipients, payload) = await _consumer.ReadData();
                var tasks = recipients
                    .Select(recipient => SendEvent(recipient, payload, cancellationToken));

                await Task.WhenAll(tasks);
            }
        }, cancellationToken);
    }

    private async Task SendEvent(Address recipient, Payload payload, CancellationToken ct)
    {
        var result = await _publisher.SendData(recipient, payload);

        while (result == SendResult.Rejected)
        {
            await Task.Delay(Timeout, ct);
            result = await _publisher.SendData(recipient, payload);
        }
        
        _logger.Log($"Event sent from {payload.Origin} to {recipient}");
    }
}