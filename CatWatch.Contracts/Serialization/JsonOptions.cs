using System.Text.Json.Serialization;
using System.Text.Json;

namespace CatWatch.Contracts.Serialization;

public static class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };
}
