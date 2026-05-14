namespace CatWatch.Contracts.Messages;


public record LogMessage(LogPriority Level, string Message, DateTime Timestamp, string? Source);
