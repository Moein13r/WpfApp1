using Chat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Api;
using WpfApp1.eventArgs;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.Viewmodels
{
    public class MainPageViewModel : BaseViewModel, IDisposable
    {
        public readonly RealTimeMessaging MessageHub;
        public MainWindow ParenView;
        public MessageApi messageApi;
        public MainPageViewModel(MainWindow parentView, RealTimeMessaging messageHub)
        {
            MessageHub = messageHub;            
            ParenView = parentView;
            messageApi = new MessageApi();
            MessageHub.ReceiveMessage += ParenView.OnMessageReceived;
            MessageHub.ReConnecting += ServerHub_ReConnecting; ;
            MessageHub.ReConnected += ServerHub_ReConnected;
        }
        private Task ServerHub_ReConnecting(Exception e)
        {
            Connection.ConnectionStatus = "Connecting To Server ...";
            return Task.CompletedTask;
        }
        private Task ServerHub_ReConnected(string connectionId)
        {
            Connection.ConnectionStatus = "Connected!";
            return Task.CompletedTask;
        }


        private Room _selectedRoom;
        public Room SelectedRoom { get { return _selectedRoom; } set { _selectedRoom = value; OnPropertyChanged(nameof(SelectedRoom)); } }

        public override async Task OnLoaded()
        {
            try
            {
                var result = await messageApi.GetRoomsByUserId(User.Id);
                Rooms = new ObservableCollection<Room>(result);
            }
            catch (Exception e)
            {
                _ = e;
            }
        }

        private ObservableCollection<Room> _rooms;
        public ObservableCollection<Room> Rooms
        {
            get
            {
                return _rooms;
            }
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        public void Dispose()
        {
            Rooms.Clear();

        }
    }
}
