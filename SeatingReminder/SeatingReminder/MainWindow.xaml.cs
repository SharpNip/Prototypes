using System;
using System.Windows;

namespace SeatingReminder
{
    public sealed partial class MainWindow : Window
    {
        private App mController;

        public MainWindow(App controller, WindowViewModel viewModel)
        {
            mController = controller;
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            WindowViewModel winVM = this.DataContext as WindowViewModel;
            winVM.OnClosed(this, null);
        }
    }
}