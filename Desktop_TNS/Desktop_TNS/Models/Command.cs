using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_TNS.Models
{
    public class Command : ICommand
    {
        private Action action;
        public event EventHandler CanExecuteChanged;
        public Command(Action act)
        {
            this.action = act;
        }
        public bool CanExecute(object parameter)
        {
            return action != null;
        }

        public void Execute(object parameter)
        {
            action.Invoke();
        }
    }
}
