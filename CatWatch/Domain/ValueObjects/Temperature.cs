public record Temperature
{
    public decimal Value { get; init; }

    public Temperature(decimal value)
    {
        Value = value;
    }
}