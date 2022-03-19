using NLog;
using System;
using System.IO;

namespace Employee.Utilities.logManager
{
    public class LoggerManager : ILoggerManager
    {

        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        public void WriteErrorMsg(string ControllerName, string actionName, string ExceptionMessage)
        {

            string CurrentDirectory= Environment.CurrentDirectory;
            if (!Directory.Exists(Path.Combine(CurrentDirectory+"\\Logs")))
            {
                Directory.CreateDirectory(Path.Combine("\\Logs"));
            }
            string logFilename = "Log_" + DateTime.Now.ToLongDateString() + ".txt";
            string logFilePath = CurrentDirectory+"\\Logs\\" + logFilename;
            string _path = Path.Combine(logFilePath);
            if (!File.Exists(_path))
            {
                File.Create(_path).Close();
               
            }
            ExceptionMessage = "Controller Name"+" "+ControllerName+ " : "+ "Action Name" + " " +actionName+ " : " + DateTime.Now.ToString() + " : " + ExceptionMessage;
            using (StreamWriter fileWriter = new StreamWriter(_path, true))
            {
                fileWriter.WriteLine(ExceptionMessage);
                fileWriter.Flush();
                fileWriter.Close();
            }
            
        }
    }
}
