using HtmlAgilityPack;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace AIOBOT
{
    /// <summary>
    /// Interaction logic for TaskControl.xaml
    /// </summary>
    public partial class TaskControl : UserControl
    {

        Thread botthread;
        private BotTask bottask = new BotTask();
        private Profile botprofile = new Profile();
        private ProfileMCT botprofile_MCT = new ProfileMCT();
        private Profile botprofile_Mortar = new Profile();

        MainWindow parent;
        HttpClient botclient = new HttpClient();
        private List<MyProxy> botproxies = new List<MyProxy>();
        OrderStep botOrder = OrderStep.None;
        CookieContainer _cookiejar = new CookieContainer();

        Thread as_thread;
        Thread mct_thread;
        Thread zozo_thread;
        Thread mortar_thread;
        Thread ftc_thread;
        Thread ark_thread;
        Thread zingaro_thread;       

        public TaskControl(BotTask task, MainWindow main)
        {
            InitializeComponent();
            store_label.Content = task.store;
            id_label.Content = task.id;
            profile_label.Content = task.profilename;
            bottask = task;

            if (task.store.Contains("+"))
            {
                size_label.Content = task.size;
                if (task.product_url.Split(';').Length < 2)
                {
                    product_label.Text = "SKU CODE:" + task.product_url;
                    bottask.product_url = task.product_url;
                }
                else if (task.product_url.Split(';').Length == 2)
                {                 
                    product_label.Text = Constant.g_strBase_AS + task.product_url.Split(';')[1];
                    bottask.product_url = Constant.g_strBase_AS + task.product_url.Split(';')[1];
                }
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == bottask.profilename)
                    {
                        botprofile = temp_profile;
                        break;
                    }
                }
            }
            else if (task.store == "zozo")
            {
                size_label.Content = task.size;
                product_label.Text = task.product_url;
                profile_label.Content = task.profilename;
                bottask = task;               
            }
            else if (task.store == "FTC")
            {
                size_label.Content = task.size;
                if (task.product_url.Split(';').Length < 2)
                {
                    if (task.product_url.Contains("http"))
                    {
                        product_label.Text = task.product_url;
                    }
                    else
                    {
                        product_label.Text = "SKU CODE:" + task.product_url;
                    }

                    bottask.product_url = task.product_url;                  
                }
                else if (task.product_url.Split(';').Length == 2)
                {                 
                    product_label.Text = Constant.g_strBase_FTC + task.product_url.Split(';')[1];
                    bottask.product_url = Constant.g_strBase_FTC + task.product_url.Split(';')[1];
                }
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == bottask.profilename)
                    {
                        botprofile = temp_profile;
                        break;
                    }
                }
            }
            else if (task.store == "arktz")
            {
                size_label.Content = task.size;
                if (task.product_url.Split(';').Length < 2)
                {
                    if (task.product_url.Contains("http"))
                    {
                        product_label.Text = task.product_url;
                    }
                    else
                    {
                        product_label.Text = "Keyword:" + task.product_url;
                    }

                    bottask.product_url = task.product_url;                 
                }
                else if (task.product_url.Split(';').Length == 2)
                {                   
                    product_label.Text = Constant.g_strBase_ARK + task.product_url.Split(';')[1];
                    bottask.product_url = Constant.g_strBase_ARK + task.product_url.Split(';')[1];
                }
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == bottask.profilename)
                    {
                        botprofile = temp_profile;
                        break;
                    }
                }
            }
            else if (task.store == "ZINGARO")
            {                
                product_label.Text = bottask.product_url;
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == bottask.profilename)
                    {
                        botprofile = temp_profile;
                        break;
                    }
                }
                size_label.Content = bottask.size;
            }
            else if (task.store == "MORTAR")
            {
                size_label.Content = task.size;
                product_label.Text = task.product_url;
                profile_label.Content = task.profilename;
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == task.profilename)
                    {
                        botprofile_Mortar = temp_profile;
                        break;
                    }
                }
                bottask = task;
            }
            else
            {
                foreach (var temp_profile in TaskManagement.User_Profiles_MCT)
                {
                    if (temp_profile.profilename == bottask.profilename)
                    {
                        botprofile_MCT = temp_profile;
                        break;
                    }
                }
            }

            status_label.Content = "IDLE";
            DataProcess db = new DataProcess();
            db.CreateConnection("user.db");
            //read proxies
            List<ProxyGroup> proxies = db.ReadData_proxy_group();
            db.Endprocess();
            foreach (var proxygroup in proxies)
            {
                if (proxygroup.group_name == bottask.activeProxy)
                {
                    botproxies = proxygroup.proxylist;
                }
            }

            if (botproxies.Count > 0)
            {
                Random random = new Random();
                int rnd_index = random.Next() % botproxies.Count;
                MyProxy active_proxy = botproxies[rnd_index];
                CGlobalVar.g_bUseProxy = true;
                CGlobalVar.g_strProxyIP = active_proxy.url.Trim();
                CGlobalVar.g_nProxyPort = active_proxy.port.Trim();
                CGlobalVar.g_strProxyID = active_proxy.user.Trim();
                CGlobalVar.g_strProxyPass = active_proxy.pass.Trim();
                proxy_label.Content = CGlobalVar.g_strProxyIP;
            }
            parent = main;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            task_edit_checked.IsChecked = !task_edit_checked.IsChecked;
            if (task_edit_checked.IsChecked == true)
            {
                if (!TaskManagement.SeletedTasks.Contains(bottask.id))
                {
                    TaskManagement.SeletedTasks.Add(bottask.id);
                }
            }
            else
            {
                if (TaskManagement.SeletedTasks.Contains(bottask.id))
                {
                    TaskManagement.SeletedTasks.Remove(bottask.id);
                }
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (task_edit_checked.IsChecked == false)
            {
                this.Background = new SolidColorBrush(Color.FromRgb(20, 20, 20));
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (task_edit_checked.IsChecked == false)
            {
                this.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        private void bot_start_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Status.LanguageSetting == false)
            {
                status_label.Content = EN_Constants.TASKSTART;
            }
            else
            {
                status_label.Content = JP_Constants.TASKSTART;
            }

            botthread = new Thread(new ThreadStart(BotProcess));
            botthread.IsBackground = true;
            botthread.Start();
        }

        public void bot_Start()
        {
            if (Status.LanguageSetting == false)
            {
                status_label.Content = EN_Constants.TASKSTART;
            }
            else
            {
                status_label.Content = JP_Constants.TASKSTART;
            }
            botthread = new Thread(new ThreadStart(BotProcess));
            botthread.IsBackground = true;
            botthread.Start();
        }

        public void bot_Stop()
        {

            try
            {
                botthread.Abort();
            }
            catch
            {

            }
            switch (bottask.store)
            {
                case "A+S":
                    try
                    {
                        as_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "zozo":
                    try
                    {
                        zozo_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "MORTAR":
                    try
                    {
                        mortar_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "mct":
                    try
                    {
                        mct_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "FTC":
                    try
                    {
                        ftc_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "arktz":
                    try
                    {
                        ark_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "ZINGARO":
                    try
                    {
                        zingaro_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
            }

            botOrder = OrderStep.None;
            status_label.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            if (Status.LanguageSetting == false)
            {
                status_label.Content = EN_Constants.IDLE;
            }
            else
            {
                status_label.Content = JP_Constants.IDLE;
            }
        }

        private void bot_stop_btn_Click(object sender, RoutedEventArgs e)
        {
            status_label.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            try
            {
                botthread.Abort();
            }
            catch
            {

            }
            switch (bottask.store)
            {
                case "A+S":
                    try
                    {
                        as_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "zozo":
                    try
                    {
                        zozo_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "MORTAR":
                    try
                    {
                        mortar_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "mct":
                    try
                    {
                        mct_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "FTC":
                    try
                    {
                        ftc_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "arktz":
                    try
                    {
                        ark_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
                case "ZINGARO":
                    try
                    {
                        zingaro_thread.Abort();
                    }
                    catch
                    {

                    }
                    break;
            }

            botOrder = OrderStep.None;
            if (Status.LanguageSetting == false)
            {
                status_label.Content = EN_Constants.IDLE;
            }
            else
            {
                status_label.Content = JP_Constants.IDLE;
            }
        }

        private void bot_log_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = System.IO.Path.Combine("Log", bottask.id + ".txt");
                Process.Start("notepad.exe", filename);
            }
            catch
            {

            }
        }


        private void AddLog(string log_string)
        {
            try
            {
                string filename = System.IO.Path.Combine("Log", bottask.id + ".txt");
                if (File.Exists(filename))
                {
                    using (StreamWriter w = File.AppendText(filename))
                    {
                        w.WriteLine(DateTime.Now.ToString("hh:mm:ss fff") + "---->" + log_string);
                    }
                }
                else
                {
                    using (StreamWriter writetext = new StreamWriter(filename))
                    {
                        writetext.WriteLine(DateTime.Now.ToString("hh:mm:ss fff") + "---->" + log_string);
                    }
                }
            }
            catch
            {

            }
        }


        private void task_edit_checked_Click(object sender, RoutedEventArgs e)
        {
            if (task_edit_checked.IsChecked == true)
            {
                this.Background = new SolidColorBrush(Color.FromRgb(20, 20, 20));
                if (!TaskManagement.SeletedTasks.Contains(bottask.id))
                {
                    TaskManagement.SeletedTasks.Add(bottask.id);
                }
            }
            else
            {
                this.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                if (TaskManagement.SeletedTasks.Contains(bottask.id))
                {
                    TaskManagement.SeletedTasks.Remove(bottask.id);
                }
            }
        }

        private void sche_checkbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void sche_checkbox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BotProcess()
        {
            if (CGlobalVar.g_strProxyID != "")
            {
                var proxy = new WebProxy
                {

                    Address = new Uri($"http://{CGlobalVar.g_strProxyIP}:{CGlobalVar.g_nProxyPort}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                                userName: CGlobalVar.g_strProxyID,
                                password: CGlobalVar.g_strProxyPass)
                };
                HttpClientHandler clientHandler = new HttpClientHandler()
                {
                    CookieContainer = _cookiejar,
                    Proxy = proxy,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                botclient = new HttpClient(clientHandler);
            }
            else
            {
                HttpClientHandler clientHandler = new HttpClientHandler()
                {
                    CookieContainer = _cookiejar,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                botclient = new HttpClient(clientHandler);
            }

            switch (bottask.store)
            {
                case "A+S":
                    if (!TaskManagement.GA_token.Contains("_ga"))
                    {
                        Dispatcher.Invoke(new Action(() => CustomMessageBox.Show("Please input the GA Token")));
                        return;
                    }
                    as_thread = new Thread(AS_Thread);
                    as_thread.Start();
                    break;
                case "zozo":
                    zozo_thread = new Thread(Zozo_Thread);
                    zozo_thread.Start();
                    break;
                case "MORTAR":
                    mortar_thread = new Thread(Mortar_Thread);
                    mortar_thread.Start();
                    break;
                case "mct":
                    mct_thread = new Thread(MCT_Thread);
                    mct_thread.Start();
                    break;
                case "FTC":
                    ftc_thread = new Thread(FTC_Thread);
                    ftc_thread.Start();
                    break;
                case "arktz":
                    ark_thread = new Thread(ARK_Thread);
                    ark_thread.Start();
                    break;
                case "ZINGARO":
                    zingaro_thread = new Thread(ZINGARO_Thread);
                    zingaro_thread.Start();
                    break;
            }
        }

        /// <summary>
        /// //////////////===========ZINGARO==================
        /// </summary>
        private void ZINGARO_Thread()
        {
        }

        private void LoginProcess_Zingaro()
        {           
        }

        private void GetInfoProduct_Zingaro()
        {        
        }

        private void AddtoCart_Zingaro()
        {           
        }

        /// <summary>
        /// /////////////////////////
        /// </summary>

        private void ARK_Thread()
        {            
        }

        private void LoginProcess_ARK()
        {          
        }

        private void GetInfoProduct_ARK()
        {          
        }

        private void Checkout_ARK()
        {
        }

        private void Checkout_Credit_ARK()
        {           
        }
        private void Checkout_Cash_ARK()
        {         
        }




        ////////////////////////////
        ///==========================================FTC THREAD================================================
        /////////////////
        private void FTC_Thread()
        {
       
        }

        private void LoginProcess_FTC()
        {
        }


        private void GetInfoProduct_FTC()
        {          
        }

        private void Checkout_FTC()
        {            
        }

        private void Checkout_Credit_FTC()
        {          
        }
        private void Checkout_Cash_FTC()
        {            
        }

        /// <summary>
        /// //////////////////////------Mortar Task Thread-------------------------/////////////////////////
        /// </summary>
        /// 
        private void Mortar_Thread()
        {                  
        }
        private void LoginProcess_Mortar()
        {            
        }
        private void AddToCart_Mortar()
        {         
        }

        private void Checkout_Preview_Mortar()
        {           
        }

        private void Checkout_Payment_Mortar()
        {           
        }

        public void WebHookSend_Mortar()
        {           
        }      

        /// <summary>
        /// /////////////---------Zozo Bot Process ----------------------------------------
        /// </summary>
        private void Zozo_Thread()
        {
        }

        private void FindProductZoZo()
        {

        }


        private void AddToCart_Zozo()
        {

        }

        private void GoCheckOut_Zozo()
        {
            
        }


        private void MCT_Thread()
        {
           
        }

        private void FindProductMCT()
        {
            
        }

        private void DumpFile(string content, string filename)
        {
           
        }
        private void AddToCart_MCT()
        {
        }

        private void GoCheckOut_MCT()
        {
           
        }
        private void AS_Thread()
        {                   
        }

        private void LoginProcess()
        {           
        }

        private void GetInfoProduct()
        {           
        }

        private void Checkout_AS()
        {           
        }

        private void Checkout_Credit_AS()
        {            
        }
        private void Checkout_Cash_AS()
        {       
        }

        private void AddtoCart_AS()
        {

        }
        private void remove_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var temp_task in TaskManagement.User_Tasks)
            {
                if (temp_task.id == bottask.id)
                {
                    TaskManagement.User_Tasks.Remove(temp_task);
                    break;
                }
            }
            parent.Refresh_TaskList(false, true);
        }

        public void SelectTask()
        {
            task_edit_checked.IsChecked = true;
            if (!TaskManagement.SeletedTasks.Contains(bottask.id))
            {
                TaskManagement.SeletedTasks.Add(bottask.id);
            }
        }
        public void UnSelectTask()
        {
            task_edit_checked.IsChecked = false;
            if (TaskManagement.SeletedTasks.Contains(bottask.id))
            {
                TaskManagement.SeletedTasks.Remove(bottask.id);
            }
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            EditTask editTask = new EditTask(bottask, this);
            editTask.Show();
        }

        public void setProductURL(string product_url)
        {
            bottask.product_url = product_url;
            product_label.Text = product_url;
        }

        public void setProductSize(string product_size)
        {
            bottask.size=product_size;
            size_label.Content = product_size;
        }

        public void setProfileName(string profilename)
        {
            try
            {
                bottask.profilename = profilename;
                profile_label.Content = profilename;
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == bottask.profilename)
                    {
                        botprofile = temp_profile;
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        public void setCardType(int cardtype)
        {
            bottask.cardtype = cardtype;
        }

        public void setProxy(string proxy)
        {
            bottask.activeProxy = proxy;
        }

        public string GetID()
        {
            return bottask.id;
        }

        public void WebHookSend()
        {           
        }

        public void WebHookSend_FTC()
        {          
        }
        public void WebHookSend_ARK()
        {           
        }
        public void WebHookSend_Zingaro()
        {          
        }
        public void AddCookies(System.Net.Cookie cookie)
        {
            _cookiejar.Add(cookie);
        }
    }
}
