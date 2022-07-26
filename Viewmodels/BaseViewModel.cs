using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Viewmodels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            Connection.OnConnectionChanged += (sender, e) => { OnPropertyChanged(nameof(ConnectionStatus)); };
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string ConnectionStatus { get { return Connection.ConnectionStatus; } }

        public abstract Task OnLoaded();        
    }
}
