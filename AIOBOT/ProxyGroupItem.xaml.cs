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
    /// Interaction logic for ProxyGroupItem.xaml
    /// </summary>
    public partial class ProxyGroupItem : UserControl
    {
        MainWindow mainwindow;
        public ProxyGroup group = new ProxyGroup();
        public ProxyGroupItem(ProxyGroup proxyGroup, MainWindow parentwindow)
        {
            InitializeComponent();
            group_id_show.Text = proxyGroup.group_id;
            group_name_show.Text = proxyGroup.group_name;
            group_count_show.Text = proxyGroup.group_count.ToString();
            group = proxyGroup;
            mainwindow = parentwindow;
        }

        public ProxyGroup getProxy()
        {
            return group;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void group_delete_Click(object sender, RoutedEventArgs e)
        {
            DataProcess db = new DataProcess();
            db.CreateConnection("user.db");
            db.RemoveData_proxy_group(group_id_show.Text.Trim());
            db.Endprocess();
            mainwindow.Refresh_Proxy_Group_list();
        }

        private void group_edit_Click(object sender, RoutedEventArgs e)
        {
            //ProxyGroupSetting proxyGroupSetting = new ProxyGroupSetting(mainwindow, this);
            //proxyGroupSetting.Show();
        }
    }
}
