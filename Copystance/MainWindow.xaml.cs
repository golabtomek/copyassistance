using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
            SearchBar.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(SearchBar_MouseLeftButtonDown), true);
            SearchBar.AddHandler(FrameworkElement.LostFocusEvent, new RoutedEventHandler(SearchBar_LostFocus), true);
        }

        private void shutdownTray_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "")
            {
                SearchBar.Text = "Search";
                SearchBar.Foreground = Brushes.Gray;
            };
                
        }

        private void SearchBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SearchBar.Text == "Search")
            {
                SearchBar.Text = "";
                SearchBar.Foreground = Brushes.White;
            }
        }
    }
}
