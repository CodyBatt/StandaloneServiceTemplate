using System;
using Microsoft.AspNet.SignalR;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace Service.Signalr
{
    public static class SignalRLogSinkExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as documents to a SignalR hub.
        /// 
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param><param name="context">The hub context.</param><param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param><param name="batchPostingLimit">The maximum number of events to post in a single batch.</param><param name="period">The time to wait between checking for event batches.</param><param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration SignalR(this LoggerSinkConfiguration loggerConfiguration, IHubContext context, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose, int batchPostingLimit = 5, TimeSpan? period = null, IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return loggerConfiguration.Sink(new SignalRLogSink(context, formatProvider), restrictedToMinimumLevel);
        }
    }
}
