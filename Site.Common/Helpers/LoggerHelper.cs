using NLog;

namespace Site.Common.Helpers
{
    public class LoggerHelper
    {
        public static ILogger GetLogger(string loggerName)
        {
            loggerName.NotNull(nameof(loggerName));
            return LogManager.GetLogger(loggerName);
        }
    }
}