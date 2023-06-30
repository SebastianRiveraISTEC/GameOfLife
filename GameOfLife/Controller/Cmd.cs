using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameOfLife.Controller
{
    public class Cmd : ICommand
    {
        private Action _execute;
        private Predicate<Object> _canexecute;
        public Cmd(Action execute, Predicate<Object> canexecute)
        {
            _execute = execute;
            _canexecute = canexecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return _canexecute?.Invoke(parameter) ?? true;
        }
        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }

    public class CmdArg : ICommand
    {
        private Action<Object> _execute;
        private Predicate<Object> _canexecute;

        public CmdArg(Action<Object> execute, Predicate<Object> canexecute)
        {
            this._execute = execute;
            this._canexecute = canexecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canexecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
