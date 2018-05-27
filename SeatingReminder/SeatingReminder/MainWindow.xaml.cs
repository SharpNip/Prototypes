using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeatingReminder
{
    public sealed partial class MainWindow : Window
    {
        private App m_Controller;
        public WindowViewModel ViewModel { get; private set; }

        public MainWindow(App controller, WindowViewModel viewModel)
        {
            m_Controller = controller;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
