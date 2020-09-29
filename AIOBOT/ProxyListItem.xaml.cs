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

namespace AIOBOT
{
    /// <summary>
    /// Interaction logic for ProxyListItem.xaml
    /// </summary>
    public partial class ProxyListItem : UserControl
    {
        MainWindow parent;
        public ProxyListItem(int index, string proxy, MainWindow main)
        {
            InitializeComponent();
            proxy_no.Text = index.ToString();
            proxy_show.Text = proxy.Split(':')[0];
            proxy_state.Text = "IDLE";
            parent = main;
        }

        private void proxy_delete_Click(object sender, RoutedEventArgs e)
        {
            parent.Remove_Temp_Proxy(Convert.ToInt32(proxy_no.Text) - 1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void set_status(string status)
        {
            Dispatcher.Invoke(new Action(() => proxy_state.Text = status));
            try
            {
                int speed = Convert.ToInt32(status.Replace("ms", "").Trim());
                if (speed < 500)
                {
                    Dispatcher.Invoke(new Action(() => proxy_state.Foreground = new SolidColorBrush(Color.FromRgb(0, 220, 0))));
                }
                else if (speed > 500 && speed < 1000)
                {
                    Dispatcher.Invoke(new Action(() => proxy_state.Foreground = new SolidColorBrush(Color.FromRgb(30, 200, 0))));
                }
                else if (speed > 1000 && speed < 1500)
                {
                    Dispatcher.Invoke(new Action(() => proxy_state.Foreground = new SolidColorBrush(Color.FromRgb(80, 100, 0))));
                }
                else if (speed < 2000 && speed > 1500)
                {
                    Dispatcher.Invoke(new Action(() => proxy_state.Foreground = new SolidColorBrush(Color.FromRgb(150, 50, 50))));
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => proxy_state.Foreground = new SolidColorBrush(Color.FromRgb(220, 40, 40))));
                }
            }
            catch
            {
                Dispatcher.Invoke(new Action(() => proxy_state.Foreground = new SolidColorBrush(Color.FromRgb(220, 0, 0))));
            }
        }
    }
}
