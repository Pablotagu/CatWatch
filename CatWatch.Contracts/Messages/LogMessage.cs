public enum LogLevel
{
    Trace,
    Debug,
    Info,
    Warning,
    Error,
    Critical
}

public record LogMessage(LogLevel Level, string Message, DateTime Timestamp, string? Source);
