//using System;
//using System.Text;
//using NLog;

//namespace Common.Loggers
//{
//    public class AppLogger
//    {
//        private static Logger logger = LogManager.GetCurrentClassLogger();

//        private AppLogger() { }

//        public static void Debug(string message, System.Exception ex, string controllerName, string actionName)
//        {
//            logger.Debug(FormatLogEntry(message, controllerName, actionName), ex);
//        }

//        public static void Info(string message, System.Exception ex, string controllerName, string actionName)
//        {
//            logger.Info(FormatLogEntry(message, controllerName, actionName), ex);
//        }

//        public static void Warn(string message, System.Exception ex, string controllerName, string actionName)
//        {
//            logger.Warn(FormatLogEntry(message, controllerName, actionName), ex);
//        }

//        public static void Error(string message, System.Exception ex, string controllerName, string actionName)
//        {
//            logger.Error(FormatLogEntry(message, controllerName, actionName), ex);
//        }

//        public static void Fatal(string message, System.Exception ex, string controllerName, string actionName)
//        {
//            logger.Fatal(FormatLogEntry(message, controllerName, actionName), ex);
//        }

//        private static string FormatLogEntry( string message,string controllerName, string actionName)      
//        {
//            StringBuilder logEntry = new StringBuilder();

//            string sessionId = String.Empty;

//            logEntry.Append("<LOGENTRY>");
//            logEntry.AppendFormat("<TIMESTAMP>{0}</TIMESTAMP>", DateTime.Now);
//            logEntry.AppendFormat("<Controller>{0}</Controller>", controllerName);
//            logEntry.AppendFormat("<Action>{0}</Action>", actionName);
//            logEntry.AppendFormat("<MSG>{0}</MSG></LOGENTRY>", message);
//            logEntry.AppendLine();

//            return logEntry.ToString();
//        }
//    }
//}
