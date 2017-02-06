using System.Windows;
using System.Windows.Input;
using WpfClipboardMonitor;

namespace Copystance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ClipboardMonitorWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void shutdownTray_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
