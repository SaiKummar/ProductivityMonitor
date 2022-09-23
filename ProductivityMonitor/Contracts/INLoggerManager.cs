namespace ProductivityMonitor.Contracts
{
    public interface INLoggerManager
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogInfo(string message);
        void LogWarn(string message);
    }
}