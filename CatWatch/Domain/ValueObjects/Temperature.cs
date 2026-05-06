namespace CatWatch.Domain.ValueObjects;

public record Temperature
{
    public double Value { get; init; }

    public Temperature(double value)
    {
        Value = value;
    }
}