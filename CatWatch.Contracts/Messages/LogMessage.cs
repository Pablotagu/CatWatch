public enum LogPriority
{
    Trace,
    Debug,
    Info,
    Warning,
    Error,
    Critical
}

public static class ServiceNames
{
    public const string CatWatch = "CatWatch";
    public const string CatWatchLog = "CatWatch.Log";
}

public record LogMessage(LogPriority Level, string Message, DateTime Timestamp, string? Source);
