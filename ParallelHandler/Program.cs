using ParallelHandler;

var handler = new Handler(
    TimeSpan.FromSeconds(10),
    new Consumer(),
    new Publisher(),
    new Logger<Handler>());

var ctSource = new CancellationTokenSource(); 

Console.WriteLine("Infinity Processing started. Press ctrl+c to exit.");
await Task.Run(async () => await handler.PerformOperation(ctSource.Token));
await Task.Run(async () =>
{
    
});

Console.CancelKeyPress += (sender, args) => CancelHandler(sender, args, ctSource);

void CancelHandler(object sender, ConsoleCancelEventArgs args, CancellationTokenSource cancellationToken)
{
    cancellationToken.Cancel();
}
