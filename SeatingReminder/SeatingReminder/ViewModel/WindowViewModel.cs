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
            OnButtonClicked = new CommandHandler((param)=>ButtonClicked(param), true);
            Hours = m_Hours.ToString("00");
            Minutes = m_Minutes.ToString("00");
            Seconds = m_Seconds.ToString("00");
        }

        private void ButtonClicked(string param)
        {
            m_Seconds++;
            m_Minutes++;
            m_Hours++;

            Hours = m_Hours.ToString("00");
            Minutes = m_Minutes.ToString("00");
            Seconds = m_Seconds.ToString("00");
        }

        public ICommand OnButtonClicked
        {
            get;
            private set;
        }

        private int m_Hours;
        private int m_Minutes;
        private int m_Seconds;

        public string Hours   { get; private set; }
        public string Minutes { get; private set; }
        public string Seconds { get; private set; }

    }
}
