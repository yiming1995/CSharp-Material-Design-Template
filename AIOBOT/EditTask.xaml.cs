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
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        BotTask old_task;
        TaskControl parent;
        public EditTask(BotTask botTask, TaskControl mainControl)
        {

            InitializeComponent();
            old_task = botTask;
            parent = mainControl;
            txtEditProductURL.Text = botTask.product_url;
            txtEditSize.Text = botTask.size;
            DataProcess db = new DataProcess();
            db.CreateConnection("user.db");
            List<string> proxy_names = db.Read_proxy_group_name();
            db.Endprocess();
            List<Profile> profiles = TaskManagement.User_Profiles;
            List<ProfileMCT> profiles_MCT = TaskManagement.User_Profiles_MCT;
            List<Profile> profiles_Mortar = TaskManagement.User_Profiles;
            cmbEditProxy.ItemsSource = proxy_names;
            cmbEditProxy.SelectedIndex = proxy_names.IndexOf(botTask.activeProxy);
            List<string> profile_names = new List<string>();

            List<string> AS_payment = new List<string>() { "Credit Card", "Cash on Delivery" };

            switch (botTask.store)
            {
                case "A+S":
                    foreach (var profile in profiles)
                    {
                        if (profile.store == "A+S")
                        {
                            cmbEditProfile.Items.Add(profile.profilename);
                            profile_names.Add(profile.profilename);
                        }
                    }
                    cmbPayment.ItemsSource = AS_payment;
                    break;
                case "MORTAR":
                    foreach (var profile in profiles)
                    {
                        if (profile.store == "MORTAR")
                        {
                            cmbEditProfile.Items.Add(profile.profilename);
                            profile_names.Add(profile.profilename);
                        }
                    }
                    cmbPayment.Items.Add("Credit Card");
                    break;
                case "MCT":
                    foreach (var profile in profiles_MCT)
                    {
                        cmbEditProfile.Items.Add(profile.profilename);
                        profile_names.Add(profile.profilename);
                    }
                    cmbPayment.Items.Add("Credit Card");
                    break;
                case "zozo":
                    break;
                case "arktz":
                    foreach (var profile in profiles)
                    {
                        if (profile.store == "ARK")
                        {
                            cmbEditProfile.Items.Add(profile.profilename);
                            profile_names.Add(profile.profilename);
                        }
                    }
                    cmbPayment.ItemsSource = AS_payment;
                    break;
                case "ZINGARO":
                    foreach (var profile in profiles)
                    {
                        if (profile.store == "Zingaro")
                        {
                            cmbEditProfile.Items.Add(profile.profilename);
                            profile_names.Add(profile.profilename);
                        }
                    }
                    cmbPayment.Items.Add("Credit Card");
                    break;
            }
            cmbEditProfile.SelectedIndex = profile_names.IndexOf(botTask.profilename);
            cmbPayment.SelectedIndex = botTask.cardtype;
        }

        private void btnUpdateCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEditUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                parent.setProductURL(txtEditProductURL.Text);
                parent.setProductSize(txtEditSize.Text);
                parent.setProfileName(cmbEditProfile.Text);
                parent.setCardType(cmbPayment.SelectedIndex);
                parent.setProxy(cmbEditProxy.Text);
            }
            catch (Exception ee)
            {
                CustomMessageBox.Show(ee.Message);
            }
            this.Close();
        }
    }
}
