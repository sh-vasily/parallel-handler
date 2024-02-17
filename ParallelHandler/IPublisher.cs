namespace ParallelHandler;

public interface IPublisher
{
    Task<SendResult> SendData(Address address, Payload payload);   
}