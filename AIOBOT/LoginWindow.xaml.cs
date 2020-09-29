using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            txtEmail.Text = ConfigurationManager.AppSettings["username"];
            txtPassword.Text = ConfigurationManager.AppSettings["password"];
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["username"].Value = txtEmail.Text;
            config.AppSettings.Settings["password"].Value = txtPassword.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            string strErrMsg = "";
            if (txtEmail.Text == "")
            {
                strErrMsg = EN_Constants.ERROR_INVALID_LOGIN_EMAIL;
                CustomMessageBox.Show(EN_Constants.Waring, strErrMsg, MessageBoxButton.OK, MessageBoxImage.None);
                return;
            }

            if (txtPassword.Text == "")
            {
                strErrMsg = EN_Constants.ERROR_INVALID_LOGIN_PASSWORD;
                CustomMessageBox.Show(EN_Constants.Waring, strErrMsg, MessageBoxButton.OK, MessageBoxImage.None);
                return;
            }
            doLogin();
        }

        private string GetMacAddress()
        {
            string macAddresses = string.Empty;
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddresses += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
            }
            catch
            {

            }
            return macAddresses;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static string GetGlobalIPAddress()
        {
            HttpCommon http_request = new HttpCommon();
            http_request.setURL("https://search.naver.com/search.naver?sm=top_hty&fbm=1&ie=utf8&query=%EB%82%B4+ip");
            http_request.setSendMode(HTTP_SEND_MODE.HTTP_GET);
            if (!http_request.sendRequest(false, ""))
            {
                return GetLocalIPAddress();
            }

            string response = http_request.getResponseString();
            int nStartPos = response.IndexOf("ip_chk_box");
            if (nStartPos < 0)
            {             
                return GetLocalIPAddress();
            }
            nStartPos = response.IndexOf("<em>", nStartPos);
            nStartPos += 4;
            int nEndPos = response.IndexOf("</em>", nStartPos);
            string strIp = response.Substring(nStartPos, nEndPos - nStartPos);
            return strIp;
        }

        private void doLogin()
        {
            string strEmail = txtEmail.Text;
            string strPass = txtPassword.Text;
            string srtMac = GetMacAddress();
            string strIP = GetGlobalIPAddress();

            string response = API.apiLogin(strEmail, strPass, srtMac,strIP);
            bool user_MCT = false;
            bool user_AS = false;
            bool user_Mortar = false;
            bool user_FTC = false;
            bool user_ARK = false;
            bool user_Zingaro = false;


            TaskManagement.user_AS = false;
            TaskManagement.user_MCT = false;
            TaskManagement.user_MORTAR = false;
            TaskManagement.user_FTC = false;
            TaskManagement.user_ARK = false;

            if (response == "error")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Wrong Request!", MessageBoxButton.OK, MessageBoxImage.None);
                return;
            }

            try
            {
                JObject result = JObject.Parse(response);               
                
                if (result["success"].ToString() == "false")
                {
                    CustomMessageBox.Show(EN_Constants.Waring, result["message"].ToString(), MessageBoxButton.OK, MessageBoxImage.None);
                    return;
                }

                try
                {
                    if (result["role"].ToString() == "admin")
                    {
                        user_AS = true;
                        user_FTC = true;
                        user_ARK = true;
                        user_MCT = true;
                        user_Mortar = true;
                        user_Zingaro = true;
                    }                    
                    else if(result["role"].ToString() == "staff")
                    {
                        user_AS = true;
                        user_FTC = true;
                        user_ARK = true;
                        user_MCT = true;
                        user_Mortar = true;
                        user_Zingaro = true;
                    }
                    else
                    {
                        JArray subscriptions = JArray.Parse(result["subscriptions"].ToString());
                        try
                        {
                            if (subscriptions.Count == 0)
                            {
                                CustomMessageBox.Show(EN_Constants.Waring, "Not found Avaliable Product", MessageBoxButton.OK, MessageBoxImage.None);
                                return;
                            }
                        }
                        catch
                        {

                        }
                        foreach (var subscription in subscriptions)
                        {
                            try
                            {
                                if (subscription["site"].ToString() == "FTC")
                                {
                                    var site_activate = subscription["status"].ToString();
                                    if (site_activate == "activated")
                                    {
                                        user_FTC = true;
                                    }
                                }
                            }
                            catch
                            {

                            }
                            try
                            {
                                if (subscription["site"].ToString() == "Mortar")
                                {
                                    var site_activate = subscription["status"].ToString();
                                    if (site_activate == "activated")
                                    {

                                        user_Mortar = true;

                                    }
                                }
                            }
                            catch
                            {

                            }
                            try
                            {
                                if (subscription["site"].ToString() == "Arktz")
                                {
                                    var site_activate = subscription["status"].ToString();
                                    if (site_activate == "activated")
                                    {

                                        user_ARK = true;

                                    }
                                }
                            }
                            catch
                            {

                            }
                            try
                            {
                                if (subscription["site"].ToString() == "A+S")
                                {
                                    var site_activate = subscription["status"].ToString();
                                    if (site_activate == "activated")
                                    {
                                        user_AS = true;

                                    }
                                }
                            }
                            catch
                            {

                            }
                            try
                            {
                                if (subscription["site"].ToString() == "Zingaro")
                                {
                                    var site_activate = subscription["status"].ToString();
                                    if (site_activate == "activated")
                                    {
                                        user_AS = true;

                                    }
                                }
                            }
                            catch
                            {

                            }
                        }

                    }
                }
                catch
                {
                    CustomMessageBox.Show(EN_Constants.Waring, "Request Failed", MessageBoxButton.OK, MessageBoxImage.None);
                    return;
                }
            }
            catch
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Request Failed", MessageBoxButton.OK, MessageBoxImage.None);
                return;
            }

            if (user_AS == true)
            {
                labASLogin.Content = "Login Success";
                labASLogin.Foreground = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            }
            if (user_MCT == true)
            {
                labMCTLogin.Content = "Login Success";
                labMCTLogin.Foreground = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            }
            if (user_Mortar == true)
            {
                labMortarLogin.Content = "Login Success";
                labMortarLogin.Foreground = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            }

            if (user_FTC == true)
            {
                labLoginFTC.Content = "Login Success";
                labLoginFTC.Foreground = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            }
            if (user_ARK == true)
            {
                labarktzLogin.Content = "Login Success";
                labarktzLogin.Foreground = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            }

            if (user_Zingaro == true)
            {
                labZiLogin.Content = "Login Success";
                labZiLogin.Foreground = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            }

            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Waring, "Current App is running", MessageBoxButton.OK, MessageBoxImage.None)));
                return;
            }

            TaskManagement.user_AS = user_AS;
            TaskManagement.user_MCT = user_MCT;
            TaskManagement.user_MORTAR = user_Mortar;
            TaskManagement.user_FTC = user_FTC;
            TaskManagement.user_ARK = user_ARK;
            TaskManagement.user_ZI = user_Zingaro;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnTwitter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("chrome.exe", "https://twitter.com/aiobotjp1?lang=en");
            }
            catch
            {
                try
                {
                    Process.Start("firefox", "https://twitter.com/aiobotjp1?lang=en");
                }
                catch
                {

                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            TaskManagement.user_AS = true;
            TaskManagement.user_MCT = true;
            TaskManagement.user_MORTAR = true;
            TaskManagement.user_ZI = true;
            TaskManagement.user_Zozo = true;
            TaskManagement.user_FTC = true;
            TaskManagement.user_ARK = true;
            if (TaskManagement.user_AS == false && TaskManagement.user_ZI == false && TaskManagement.user_MORTAR == false && TaskManagement.user_FTC == false && TaskManagement.user_ARK == false)
            {
                CustomMessageBox.Show("Error", "No authenticated user", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
