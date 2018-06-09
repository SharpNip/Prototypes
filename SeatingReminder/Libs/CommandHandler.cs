using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Libs
{
    public class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<string> m_Action;
        private readonly bool m_CanExecute;

        public CommandHandler(Action<string> action, bool canExecute)
        {
            m_Action = action;
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object sender)
        {
            var sn = sender;
            return true;
        }

        public void Execute(object parameter)
        {
            m_Action.Invoke((string)parameter);
        }
    }
}
