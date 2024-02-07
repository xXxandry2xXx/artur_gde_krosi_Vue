using Microsoft.Extensions.Logging;

namespace artur_gde_krosi_Vue.Server.Models.ProjecktSetings
{
    public class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public void Dispose()
        {
        }
    }
}
