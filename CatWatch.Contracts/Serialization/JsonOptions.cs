using System.Text.Json.Serialization;
using System.Text.Json;

public static class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };
}
