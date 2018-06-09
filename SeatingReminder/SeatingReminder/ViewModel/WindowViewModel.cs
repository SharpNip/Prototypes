using Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SeatingReminder
{
    public sealed class WindowViewModel
    {
        public WindowViewModel()
        {
            InitCommands();
        }

        private void InitCommands()
        {
            OnButtonClicked = new CommandHandler((param) => ButtonClicked(param), true);
            
        }

        private void ButtonClicked(string param)
        {
            Hours = "Test";
        }

        public ICommand OnButtonClicked
        {
            get;
            private set;
        }

        public string Hours   { get; private set; }
        public string Minutes { get; private set; }
        public string Seconds { get; private set; }

    }
}
