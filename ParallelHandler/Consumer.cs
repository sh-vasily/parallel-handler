namespace ParallelHandler;

public class Consumer : IConsumer
{
    private readonly Random _random = new ();
    
    public Task<Event> ReadData()
    {
        var recipientsCount =_random.Next(100);
        var recipients = Enumerable
            .Range(0, recipientsCount)
            .Select(recipient => new Address(Guid.NewGuid().ToString(), recipient.ToString()))
            .ToList();

        return Task.FromResult(new Event(recipients, new Payload("origin", Array.Empty<byte>())));
    }
}