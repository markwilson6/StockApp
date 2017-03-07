using System;
using System.Windows.Input;

namespace Stock
{
    internal class RelayCommands : ICommand
    {
        private Predicate<object> _canCommandExecute;
        private Action<object> _executeCommand;

        public RelayCommands(Action<object> execute)
            :this(execute,null)
        {

        }

        public RelayCommands(Action<object> executeCommand,Predicate<object> canCommandExecute)
        {
            if(executeCommand != null)
            {
                this._executeCommand = executeCommand;
                this._canCommandExecute = canCommandExecute;
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this._canCommandExecute == null ? true : this._canCommandExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this._executeCommand(parameter);
        }

        public event EventHandler canExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}