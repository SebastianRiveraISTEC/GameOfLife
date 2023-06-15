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
}
