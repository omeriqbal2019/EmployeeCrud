namespace Employee.Utilities.logManager
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
        void WriteErrorMsg(string ControllerName,string actionName,string ExceptionMessage);
    }
}
