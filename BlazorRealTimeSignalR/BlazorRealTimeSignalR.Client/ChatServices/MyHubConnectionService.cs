using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorRealTimeSignalR.Client.ChatServices
{
    public class MyHubConnectionService
    {
        public bool IsConnected { get; set; }
        private readonly HubConnection _hubConnection;

        public MyHubConnectionService(NavigationManager navigationManager)
        {
            // Initialize the HubConnection ( connect to the SignalR Hub)
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
                .Build();

            // Start the connection
            _hubConnection.StartAsync();
            GetConnectionState();
        }

        public HubConnection GetHubConnection()
        {
            return _hubConnection;
        }

        public bool GetConnectionState()
        {
            var hubConnection = GetHubConnection();
            IsConnected = hubConnection != null;
            return IsConnected;
        }


    }
}
