using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopApp
{
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(string message)
        {
            InitializeComponent();
            MessageLabel.Content = message;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
