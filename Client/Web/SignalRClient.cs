using System.Net;
using System.Threading.Tasks;
using DataTransfer;
using Microsoft.AspNet.SignalR.Client;
using Serilog;

namespace Client.Web
{
    class SignalRClient
    {
        string ServerUri { get; }

        ILogger Logger { get; }

        public SignalRClient(IWebClient webClient, ILogger logger)
        {
            ServerUri = webClient.ServerUri;
            Logger = logger;
        }

        public async Task ConnectSignalR(string @group = "default")
        {
            var hubConnection = new HubConnection(ServerUri);
            hubConnection.Credentials = CredentialCache.DefaultCredentials;

            var logHubProxy = hubConnection.CreateHubProxy("StatusMessageHub");
            await hubConnection.Start();

            logHubProxy.On<StatusMessage>("NewMessage", status => Logger.Debug(status.Message));
            await logHubProxy.Invoke("JoinGroup", @group);
        }
    }
}
