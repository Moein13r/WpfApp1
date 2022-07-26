using Chat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Api;
using WpfApp1.Commands;
using WpfApp1.Dtos;
using WpfApp1.eventArgs;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.Viewmodels
{
    public class ChatWindowViewModel : BaseViewModel, IDisposable
    {
        public Room? _room;
        RealTimeMessaging MessageHub;
        public Room Room { get { return _room; } set { _room = value; OnPropertyChanged(nameof(Room)); } }
        MessageApi messageApi;
        private readonly ChatWindow ParentView;
        public ICommand SendCommand { get; set; }
        public ChatWindowViewModel(Room? room, ChatWindow parentView, RealTimeMessaging messageHub)
        {
            ParentView = parentView;
            MessageHub = messageHub;
            SendCommand = new ActionCommand(Message_SendCommand);
            CurrentMessage = new MessageCreateDto();
            messageApi = new MessageApi();
            Room = room;
            OnLoaded();
        }
        private MessageCreateDto _currentMessage;
        public MessageCreateDto CurrentMessage
        {
            get
            {
                return _currentMessage;
            }
            set
            {
                _currentMessage = value;
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }


        private ObservableCollection<Message> _messages;

        public ObservableCollection<Message> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }
        private async void Message_SendCommand(object sender)
        {
            CurrentMessage.UserId = User.Id;
            CurrentMessage.RoomId = Room.Id;
            CurrentMessage.ParentMessageId = 0;
            CurrentMessage.UpdatedAt = DateTime.Now;
            await MessageHub.SendMessageAsync(CurrentMessage);
            CurrentMessage = new MessageCreateDto();
        }
        public override async Task OnLoaded()
        {
            if (Room != null)
            {
                var result = await messageApi.GetRoomMessages(Room.Id);
                Messages = new ObservableCollection<Message>(result);
            }
        }

        public void Dispose()
        {
            Messages.Clear();
            //MessageHub.Dispose();
        }
    }
}
