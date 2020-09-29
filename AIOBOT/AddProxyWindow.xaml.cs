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
using System.Windows.Shapes;

namespace AIOBOT
{
    /// <summary>
    /// Interaction logic for AddProxyWindow.xaml
    /// </summary>
    public partial class AddProxyWindow : Window
    {
        MainWindow parent = null;
        public AddProxyWindow(MainWindow main)
        {
            InitializeComponent();
            parent = main;
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveproxy_btn_Click(object sender, RoutedEventArgs e)
        {
            string proxy_text = new TextRange(proxy_input.Document.ContentStart, proxy_input.Document.ContentEnd).Text;
            if (proxy_text != "")
            {
                var lines = proxy_text.Split('\n').ToList();
                parent.temp_proxy_lists = lines;
                parent.Refresh_Edit_Group_Proxy_List();
            }
            this.Close();
        }
    }
}
