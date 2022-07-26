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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Services;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatWindow : UserControl
    {
        public ChatWindow(Room room)
        {
            DataContext = new Viewmodels.ChatWindowViewModel(room, this, App.serviceProvider.GetService<RealTimeMessaging>());
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckPathExists = fileDialog.CheckFileExists = true;
            try
            {
                fileDialog.ShowDialog();
                if (!string.IsNullOrEmpty(fileDialog.FileName))
                {
                    var selectedFile = fileDialog.OpenFile();

                    if (selectedFile != null)
                    {
                        _ = selectedFile;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        private void FileDialog_FileOk(object? sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
