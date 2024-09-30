//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

namespace LMS.Web.Brokers.Loggings
{
    public class LoggingBroker: ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger)
        {
            this.logger = logger;
        }
        public void LogError(Exception exception) =>
            this.logger.LogError(exception, exception.Message);

        public void LogCritical(Exception exception)=>
            this.logger.LogCritical(exception,exception.Message);
    }
}
