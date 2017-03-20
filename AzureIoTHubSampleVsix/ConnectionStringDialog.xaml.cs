using Microsoft.VisualStudio.PlatformUI;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Navigation;

namespace AzureIoTHubSampleVsix
{
    /// <summary>
    /// Interaction logic for ConnectionStringDialog.xaml
    /// </summary>
    public partial class ConnectionStringDialog:DialogWindow
    {
        public ConnectionStringDialog()
        {
            InitializeComponent();
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            var devConnectionString = this.DeviceConnectionString.Text;
            var ioTConnectionString = this.IoTHubConnectionString.Text;

            if (string.IsNullOrEmpty(devConnectionString) || string.IsNullOrEmpty(ioTConnectionString))
            {
                MessageBox.Show("Device connection string or IoT Hub connection string cannot be empty.");
                return;
            }

            var ioTRegex = new Regex("^\\s*HostName=.*?;SharedAccessKeyName=.*?;SharedAccessKey=.*?");
            var devRegex = new Regex("^\\s*HostName=.*?;DeviceId=.*?;SharedAccessKey=.*?");

            if (!ioTRegex.IsMatch(ioTConnectionString))
            {
                MessageBox.Show("IoT Hub Connection String must be in format HostName=xxx;SharedAccessKeyName=xxx;SharedAccessKey=xxx.");
                return;
            }

            if (!devRegex.IsMatch(devConnectionString))
            {
                MessageBox.Show("Device Connection String must be in format HostName=xxx;DeviceId=xxx;SharedAccessKey=xxx.");
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
