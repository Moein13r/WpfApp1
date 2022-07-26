using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Connection
    {
        public static EventHandler OnConnectionChanged;
        public static string ConnectionStatus { get { return _connectionStatus; } set { _connectionStatus = value; OnConnectionChanged.Invoke(null, EventArgs.Empty); } }
        private static string _connectionStatus= "Connecting To Server ...";
    }
}