using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    internal class ActionCommand : ICommand
    {
        private Action<object> _action;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public ActionCommand(Action<object> action)
        {
            _action = action;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        public event EventHandler? Action;
        public void Execute(object? parameter)
        {
            _action?.Invoke(parameter);
        }
    }
}
