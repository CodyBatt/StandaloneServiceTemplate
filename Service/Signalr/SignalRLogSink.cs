using System;
using DataTransfer;
using Microsoft.AspNet.SignalR;
using Serilog.Core;
using Serilog.Events;

namespace Service.Signalr
{
    public class SignalRLogSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly IHubContext _context;

        public SignalRLogSink(IHubContext context, IFormatProvider formatProvider)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _formatProvider = formatProvider;
            _context = context;
        }

        public void Emit(LogEvent logEvent)
        {
            LogEventPropertyValue contextValue;
            if (!logEvent.Properties.TryGetValue(Constants.SourceContextPropertyName, out contextValue))
            {
                return;
            }
            var scalar = contextValue as ScalarValue;
            if (scalar == null) return;
            var @group = (string)scalar.Value;
            _context.Clients.Group(@group).NewMessage(new StatusMessage { Message = logEvent.RenderMessage(_formatProvider) });
        }
    }
}
