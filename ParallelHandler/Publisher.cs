namespace ParallelHandler;

public class Publisher : IPublisher
{
    private readonly Random _random = new ();
    private readonly ILogger<Publisher> _logger = new Logger<Publisher>();
    
    public async Task<SendResult> SendData(Address address, Payload payload)
    {
        var randomNumber = _random.Next();
        await Task.Delay(TimeSpan.FromSeconds(10));
        var result = randomNumber % 10 == 0 ? SendResult.Accepted : SendResult.Rejected;
        _logger.Log($"Sent from {payload.Origin} to {address}. Result: {result}");
        return result;
    }
}