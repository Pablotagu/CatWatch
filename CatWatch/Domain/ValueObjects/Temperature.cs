namespace CatWatch.Domain.ValueObjects;

public record Temperature
{
    public decimal Value { get; init; }

    public Temperature(decimal value)
    {
        Value = value;
    }
}