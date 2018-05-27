using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SeatingReminder
{
    public partial class App : Application
    {
        private MainWindow m_MainWindow;

        private WindowViewModel m_VMMainWindow;
        public WindowViewModel VMMainWindow { get => m_VMMainWindow ?? (m_VMMainWindow = new WindowViewModel()); }

        public App()
        {
            LoadResources();

            SetMainWindow(new MainWindow(this, VMMainWindow));
        }

        private void SetMainWindow(MainWindow mainWindow)
        {
            m_MainWindow = mainWindow;
            m_MainWindow.Show();
        }

        private void LoadResources()
        {
            
        }
    }
}
