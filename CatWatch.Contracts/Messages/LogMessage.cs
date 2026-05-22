using Microsoft.Extensions.Logging;

namespace CatWatch.Contracts.Messages;


public record LogMessage(LogLevel Level, string Message, DateTime Timestamp, string? Source);
