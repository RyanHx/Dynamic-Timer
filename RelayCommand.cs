using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DynamicTimer
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;
        private Action<object> actionOne;

        public RelayCommand(Action method)
        {
            action = method;
        }

        public RelayCommand(Action<object> action)
        {
            this.actionOne = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(action != null)
            {
                action();
                return;
            }
            if(actionOne != null)
            {
                actionOne(parameter);                
            }
        } 
    }
}
