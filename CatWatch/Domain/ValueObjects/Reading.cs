public record Reading
{
    public DateTimeOffset Timestamp { get; init; }
    public required Temperature Temperature { get; init; }
    public double Humidity { get; init; }
}