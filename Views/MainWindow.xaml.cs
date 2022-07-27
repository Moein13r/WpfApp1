using Chat.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Services;
using WpfApp1.Viewmodels;
using WpfApp1.Views;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainPageViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel(this, App.serviceProvider.GetService<RealTimeMessaging>());
            DataContext = viewModel;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            Loaded += WindowLoaled;
        }
        public void OnMessageReceived(object? sender, EventArgs e)
        {
            var message = sender as Message;
            if (viewModel.SelectedRoom.Id == message.RoomId)
            {
                ((Container.Content as ChatWindow).DataContext as ChatWindowViewModel).Messages.Add(message);
            }
            else
            {
                var room = viewModel.Rooms.Where(r => r.Id == message.RoomId).FirstOrDefault();
                room.UnSeenedMessages += 1;
            }
        }

        public async void WindowLoaled(object? sender, RoutedEventArgs e)
        {
            await viewModel.OnLoaded();
            viewModel.SelectedRoom = viewModel.Rooms?.FirstOrDefault();
            Container.Content = new ChatWindow(viewModel.SelectedRoom);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.SelectedRoom = (sender as Grid).Tag as Room;
            Container.Content = new ChatWindow(viewModel.SelectedRoom);
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CreateRoomWindow createRoomWindow = new CreateRoomWindow();
            createRoomWindow.Owner = App.Current.MainWindow;
            createRoomWindow.ShowDialog();
        }
    }
}