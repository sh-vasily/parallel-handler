namespace ParallelHandler;

interface IHandler 
{
    TimeSpan Timeout { get; }
   
    Task PerformOperation(CancellationToken cancellationToken);
}