﻿using Libs;

using Microsoft.Win32;

using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace NotificationIntervaller
{
    public sealed class WindowViewModel : ViewModelBase
    {
        private System.Timers.Timer timer;
        public EventHandler OnClosed;

        private long timeElapsed = 0;
        private long startTime = 0;
        private long stoppedTime;
        private MediaPlayer soundPlayer;

        private long NotificationTickTime;

        public WindowViewModel()
        {
            InitCommands();
            SoundFileName = "Select a Sound File";
            ResetClock();
        }

        private void InitCommands()
        {
            StartClicked       = new CommandHandler((param) => StartClock(),           true);                        
            PauseClicked       = new CommandHandler((param) => TogglePauseClock(),     true);            
            ResetClicked       = new CommandHandler((param) => ResetClock(),           true);            
            SelectAudioClicked = new CommandHandler((param) => SetSound(),             true);            
            KillAudioClicked   = new CommandHandler((param) => KillSound(),            true);
            CloseClicked       = new CommandHandler((param) => App.Current.Shutdown(), true);
        }

        private void KillSound()
        {
            if (soundPlayer != null)
            {
                soundPlayer.Stop();
            }
        }

        private void SetSound()
        {
            OpenFileDialog fileBrowser = new OpenFileDialog()
            {
                 Filter = "Sound files | *.wav; *.mp3; *.mid",
            };

            if ((bool)fileBrowser.ShowDialog())
            {
                if (soundPlayer != null)
                {
                    soundPlayer.Stop();
                    soundPlayer.Close();
                    soundPlayer = null;
                }

                soundPlayer = new MediaPlayer();
                soundPlayer.Open(new Uri(fileBrowser.FileName));
                soundPlayer.MediaEnded += (a,b) =>
                {
                    soundPlayer.Stop();
                };


                SoundFileName = Path.GetFileNameWithoutExtension(fileBrowser.FileName);
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

                    if (NotificationTickTime != 0 && timeElapsed >= NotificationTickTime)
                    {
                        Console.WriteLine("Time Reached!");
                        if (soundPlayer != null)
                        {
                            soundPlayer.Dispatcher.Invoke(() =>
                            {
                                soundPlayer.Volume = 0.5;
                                soundPlayer.Play();
                            });
                        }

                        ResetClock();
                        StartClock();
                    }
                };
            }
            else
            {
                timer.Enabled = true;
            }
        }

        private void ResetClock()
        {
            if (timer != null)
            {
                timer.Close();
                timer.Dispose();
                timer = null;

            }

            MilliSeconds = "000";
            Seconds      = "00";
            Minutes      = "00";
            Hours        = "00";
        }

        private long GetTimeFromString(string value)
        {
            long analyzedNotifTick = 0;

            if (!string.IsNullOrWhiteSpace(value) && TimeSpan.TryParse(value, out TimeSpan time))
            {
                analyzedNotifTick = time.Ticks;
            }

            return analyzedNotifTick;
        }

        public ICommand StartClicked       { get; private set; }
        public ICommand PauseClicked       { get; private set; }
        public ICommand ResetClicked       { get; private set; }
        public ICommand SelectAudioClicked { get; private set; }
        public ICommand KillAudioClicked   { get; private set; }
        public ICommand CloseClicked       { get; private set; }

        public string NotificationTime
        {
            get
            {
                return TimeSpan.FromTicks(GetTimeFromString(Get<string>())).ToString();
            }
            set
            {
                Set(value);                
                NotificationTickTime = GetTimeFromString(value);               
            }
        }

        public string SoundFileName
        {
            get { return Get<string>(); }
            set { Set(value); }
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