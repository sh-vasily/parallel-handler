namespace ParallelHandler;

public record Event(IReadOnlyCollection<Address> Recipients, Payload Payload);