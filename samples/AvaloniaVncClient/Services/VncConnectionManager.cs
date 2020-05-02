using System;
using System.Threading;
using System.Threading.Tasks;
using MarcusW.VncClient;
using MarcusW.VncClient.Avalonia.Adapters.Logging;
using Microsoft.Extensions.Logging;
using Splat;

namespace AvaloniaVncClient.Services
{
    public class VncConnectionManager
    {
        private readonly InteractiveAuthenticationHandler _interactiveAuthenticationHandler;

        private readonly VncClient _vncClient;

        public VncConnectionManager(InteractiveAuthenticationHandler? interactiveAuthenticationHandler = null)
        {
            _interactiveAuthenticationHandler = interactiveAuthenticationHandler
                ?? Locator.Current.GetService<InteractiveAuthenticationHandler>()
                ?? throw new ArgumentNullException(nameof(interactiveAuthenticationHandler));

            // Create and populate default logger factory for logging to Avalonia logging sinks
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new AvaloniaLoggerProvider());

            _vncClient = new VncClient(loggerFactory, VncDefaults.GetEncodingsCollection());
        }

        public Task<VncConnection> ConnectAsync(CancellationToken cancellationToken = default)
        {
            return _vncClient.ConnectAsync(_interactiveAuthenticationHandler, null, cancellationToken);
        }
    }
}
