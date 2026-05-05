public record Reading
{
    public DateTimeOffset Timestamp { get; init; }
    public double Temperature { get; init; }
    public double Humidity { get; init; }
}