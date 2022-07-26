using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using WpfApp1.Dtos;
using WpfApp1.eventArgs;
using WpfApp1.Models;
using WpfApp1.Viewmodels;

namespace WpfApp1.Services
{
    public class RealTimeMessaging : IDisposable
    {
        private HubConnection hubConnection;
        public EventHandler ReceiveMessage;
        /// <summary>
        /// Occurs When Reconncting To Server Hub
        /// </summary>
        public Func<Exception, Task> ReConnecting;
        public Func<string, Task> ReConnected;
        private async void StartMessageHub()
        {
            try
            {
                hubConnection = new HubConnectionBuilder().WithAutomaticReconnect().WithUrl("https://localhost:7033/chatHub", t => t.Headers.Add("UserId", User.Id.ToString())).Build();
                hubConnection.On<Message>("ReceiveMessage", OnReceive);
                hubConnection.Reconnecting += ReConnecting;
                hubConnection.Reconnected += ReConnected;                
                await hubConnection.StartAsync();
                Connection.ConnectionStatus = "Connected!";
            }
            catch (Exception e)
            {
                Connection.ConnectionStatus = "Failed To Connect To Server Hub!";
                Debug.WriteLine(" --> Exception Through The Hub Connection Occured: {0}", e.Message);
            }
        }
        public RealTimeMessaging()
        {
            StartMessageHub();
        }
        public async void Dispose()
        {
            await hubConnection.StopAsync();
            await hubConnection.DisposeAsync();
        }
        private void OnReceive(Message message)
        {
            ReceiveMessage?.Invoke(message, new EventArgs());
        }
        public async Task SendMessageAsync(MessageCreateDto message)
        {
            await hubConnection.InvokeAsync("sendmessage", message);
        }
    }
}