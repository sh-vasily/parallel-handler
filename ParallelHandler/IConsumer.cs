namespace ParallelHandler;

public interface IConsumer
{
    public Task<Event> ReadData();
}