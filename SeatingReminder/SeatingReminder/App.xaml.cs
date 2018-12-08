using System;
using System.Drawing;
using System.Threading;
using System.Windows;

using WF = System.Windows.Forms;

namespace SeatingReminder
{
    public partial class App : Application
    {
        private MainWindow m_MainWindow;

        private WindowViewModel m_VMMainWindow;
        public WindowViewModel VMMainWindow { get => m_VMMainWindow ?? (m_VMMainWindow = new WindowViewModel()); }
        private WF.NotifyIcon icon;
        private Mutex mutex;

        public App()
        {
            bool isOwned = false;
            mutex = new Mutex(false, "TimerApp", out isOwned);
            if (isOwned)
            {
                icon = new WF.NotifyIcon()
                {
                    Icon = Icon.ExtractAssociatedIcon(@"D:\Coding\Repositoried Projects\Prototypes\SeatingReminder\SeatingReminder\Stainless_6.ico"),
                    Visible = true,                    
                };

                icon.Click += OnNotifIconClicked;
                m_MainWindow = new MainWindow(this, VMMainWindow);

                VMMainWindow.OnClosed += (ob, ev) =>
                {
                    icon.Visible = false;
                    icon.Dispose();
                };

                DisplayMainWindow();
            }
        }

        private void DisplayMainWindow()
        {
            m_MainWindow.Show();
            Rect rect = SystemParameters.WorkArea;
            m_MainWindow.Left = rect.Right - m_MainWindow.Width;
            m_MainWindow.Top = rect.Bottom - m_MainWindow.Height;
        }

        private void OnNotifIconClicked(object sender, EventArgs e)
        {
            if (m_MainWindow != null)
            {
                if (m_MainWindow.IsVisible)
                {
                    m_MainWindow.Hide();
                }
                else
                {
                    DisplayMainWindow();
                }                
            }
        }
    }
}