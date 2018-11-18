using Libs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SeatingReminder
{
    public sealed class WindowViewModel : ViewModelBase
    {
        private System.Timers.Timer timer;

        private long timeElapsed = 0;
        private long startTime = 0;
        private long stoppedTime;

        private long NotificationTickTime;

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
            switch (param)
            {
                case "Start":
                {
                    StartClock();
                    break;
                }

                case "Pause":
                {
                    TogglePauseClock();
                    break;
                }

                case "Reset":
                {
                    ResetClock();
                    break;
                }

                default:
                {
                    break;
                }
            }
        }

        private void TogglePauseClock()
        {
            if (timer != null)
            {
                timer.Enabled = !timer.Enabled;

                if (timer.Enabled)
                {
                    startTime += (DateTime.UtcNow.Ticks - stoppedTime);
                }
                else
                {
                    stoppedTime = DateTime.UtcNow.Ticks;
                }
            }
        }

        private void StartClock()
        {
            if (timer == null)
            {
                timeElapsed = 0;

                startTime = DateTime.UtcNow.Ticks;

                timer = new System.Timers.Timer(1d)
                {
                    AutoReset = true,
                    Enabled = true,
                };

                timer.Elapsed += (a, b) =>
                {
                    timeElapsed = DateTime.UtcNow.Ticks - startTime;
                    MilliSeconds = (timeElapsed % 10000000).ToString("000");
                    Seconds      = ((timeElapsed / 10000000) % 60).ToString("00");
                    Minutes      = ((timeElapsed / 600000000) % 60).ToString("00");
                    Hours        = ((timeElapsed / 36000000000) % 24).ToString("00");


                };
            }
            else
            {
                TogglePauseClock();
            }
        }

        private void ResetClock()
        {
            if (timer != null)
            {
                timer.Close();
                timer.Dispose();
                timer = null;

                MilliSeconds = "000";
                Seconds      = "00";
                Minutes      = "00";
                Hours        = "00";
            }
        }

        private long GetTimeFromString(string value)
        {
            long analyzedNotifTick = 0;

            if (!string.IsNullOrWhiteSpace(value))
            {
                
            }

            return analyzedNotifTick;
        }

        public ICommand OnButtonClicked
        {
            get;
            private set;
        }

        public string NotificationTime
        {
            get { return Get<string>(); }
            set
            {
                Set(value);                
                NotificationTickTime = GetTimeFromString(value);               
            }
        }

        public string Hours
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string Minutes
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string Seconds
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string MilliSeconds
        {
            get { return Get<string>(); }
            set { Set(value); }
        }
    }
}