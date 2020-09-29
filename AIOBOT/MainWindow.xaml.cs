using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AIOBOT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //AS
        private string strProductSKU_AS = "";
        private string strProductURL_AS = "";
        private bool boolProductFound_AS = false;        

        private Random GenID = new Random();         
        private Thread thread_getinfo = null;         
        delegate void FindComboIndexCallBack(ComboBox ctrl, string str);
        private string strProductImage = "";
        //MCT
        private string strProductInfo_MCT = "";
        private bool ProductFound_MCT = false;
        private string strProductImage_MCT = "";
        private string strProductHandle_MCT = "";
        private string strProductVariant_MCT = "";
        private List<string> strProductVariants_MCT = new List<string>();
        private string strProductSKU_MCT = "";
        private List<string> strProductSKUs_MCT = new List<string>();
        private string strProductName_MCt = "";

        //MORTAR
        private string strProductURL_Mortar = "";
        private Profile selectedMortarProfile=new Profile();

        //FTC
        private string strProductSKU_FTC = "";
        private string strProductURL_FTC = "";
        private bool boolProductFound_FTC = false;
        private string strProductImage_FTC = "";
        private Thread thread_getinfo_FTC = null;

        //ARK
        private string strProductSKU_ARK = "";
        private string strProductURL_ARK = "";
        private bool boolProductFound_ARK = false;
        private string strProductImage_ARK = "";
        private Thread thread_getinfo_ARK = null;

        //ISB
        private string strProductURL_ISB = "";
        private bool boolProductFound_ISB = false;
        //Zozo Site
        private string strImageUrl_Zozo = "";
        private List<string> cart_opt = new List<string>();

        //setting proxy

        List<ProxyListItem> proxyListItems = new List<ProxyListItem>();
        public List<MyProxy> myproxys = new List<MyProxy>();
        public List<string> temp_proxy_lists = new List<string>();
        List<string> speed_values = new List<string>();
        List<ProxyGroup> proxyGroups = new List<ProxyGroup>();
        List<ProxyGroupItem> proxyGroupItems = new List<ProxyGroupItem>();
        Thread speed_test;

        //settings
        List<string> musicfiles = new List<string>();
        List<string> success_musicfiles = new List<string>();
        List<string> warning_musicfiles = new List<string>();
        string str_cur_backmusic = "";
        MediaPlayer wowSound;
        //Manage Task Controls
        public List<TaskControl> taskControls = new List<TaskControl>();

        //token timer
        private DispatcherTimer timer_token;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void dashboard_show__btn_Click(object sender, RoutedEventArgs e)
        {
            TaskBoard.Visibility = Visibility.Visible;
            CreateTask_Panel.Visibility = Visibility.Hidden;
            Profile_panel.Visibility = Visibility.Hidden;
            FootPanel.Visibility = Visibility.Visible;
            panProxy.Visibility = Visibility.Hidden;
            panSetting.Visibility = Visibility.Hidden;
        }

        private void create_task_show_btn_Click(object sender, RoutedEventArgs e)
        {
            TaskBoard.Visibility = Visibility.Hidden;
            CreateTask_Panel.Visibility = Visibility.Visible;
            Profile_panel.Visibility = Visibility.Hidden;
            FootPanel.Visibility = Visibility.Hidden;
            panProxy.Visibility = Visibility.Hidden;
            panSetting.Visibility = Visibility.Hidden;
        }

        private void profilepanel_show_btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_panel.Visibility = Visibility.Visible;
            FootPanel.Visibility = Visibility.Hidden;
            CreateTask_Panel.Visibility = Visibility.Hidden;
            TaskBoard.Visibility = Visibility.Hidden;
            panProxy.Visibility = Visibility.Hidden;
            panSetting.Visibility = Visibility.Hidden;
        }

        private void btnProxy_Click(object sender, RoutedEventArgs e)
        {
            Profile_panel.Visibility = Visibility.Hidden;
            FootPanel.Visibility = Visibility.Hidden;
            CreateTask_Panel.Visibility = Visibility.Hidden;
            TaskBoard.Visibility = Visibility.Hidden;
            panProxy.Visibility = Visibility.Visible;
            panSetting.Visibility = Visibility.Hidden;
        }
        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            Profile_panel.Visibility = Visibility.Hidden;
            FootPanel.Visibility = Visibility.Hidden;
            CreateTask_Panel.Visibility = Visibility.Hidden;
            TaskBoard.Visibility = Visibility.Hidden;
            panProxy.Visibility = Visibility.Hidden;
            panSetting.Visibility = Visibility.Visible;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string subPath = "Log";
                bool exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);
            }
            catch
            {

            }

            //initialize
            RefreshProfile(true, 0);
            Refresh_Proxy_Group_list();
            Refresh_TaskList(true, true);
            Load_Music();
            //setting constants
            CGlobalVar.g_nRetryCount = Convert.ToInt32(txtRetryCount.Text);
            CGlobalVar.g_nRetryInterval = Convert.ToInt32(txtRetryInterval.Text);
            CGlobalVar.g_nOrderSleepTime = Convert.ToInt32(txtOrderSleepTime.Text);

            //refresh webhook            
            string webhook = ConfigurationManager.AppSettings["webhook"];
            txtwebhook.Text = webhook;
            TaskManagement.webhook = webhook;


            //Token timer

            timer_token = new DispatcherTimer();
            timer_token.Tick += dispatcherTimer_Tick;
            timer_token.Interval = new TimeSpan(0, 0, 1);
            timer_token.Start();


            if (TaskManagement.user_MCT == false)
            {
                panCreateTaskMCT.Visibility = Visibility.Hidden;
            }
            if (TaskManagement.user_AS == false)
            {
                panCreateTaskAS.Visibility = Visibility.Hidden;
            }
        }

        private void Load_Music()
        {
            string music_dir = System.IO.Path.Combine(Environment.CurrentDirectory, "music");
            string[] files = System.IO.Directory.GetFiles(music_dir);
            foreach (var musicfile in files)
            {
                if (musicfile.Contains("back"))
                {
                    musicfiles.Add(musicfile);
                }
                else if (musicfile.Contains("suc"))
                {
                    success_musicfiles.Add(musicfile);
                }
                else if (musicfile.Contains("warn"))
                {
                    warning_musicfiles.Add(musicfile);
                }
            }
            for (int i = 0; i < musicfiles.Count; i++)
            {
                cmbBackGroundSound.Items.Add("Music" + (i + 1).ToString());
            }
            for (int i = 0; i < success_musicfiles.Count; i++)
            {
                cmbSuccessSound.Items.Add("Success Sound" + (i + 1).ToString());
            }
            for (int i = 0; i < warning_musicfiles.Count; i++)
            {
                cmbWarningSound.Items.Add("Warning Sound" + (i + 1).ToString());
            }
        }

        public void Refresh_TaskList(bool init, bool delete_task)
        {
            if (init)
            {
                DataProcess db = new DataProcess();
                db.CreateConnection("user.db");
                TaskManagement.User_Tasks = db.Read_tasks();
                db.Endprocess();
            }

            if (!delete_task)
            {
                foreach (var botTask in TaskManagement.User_Tasks)
                {
                    bool task_exist = false;
                    foreach (var temp_taskcontrol in taskControls)
                    {
                        if (temp_taskcontrol.GetID() == botTask.id)
                        {
                            task_exist = true;
                            break;
                        }
                    }
                    if (task_exist == false)
                    {
                        TaskControl taskControl = new TaskControl(botTask, this);
                        taskControls.Add(taskControl);
                        taskcontrols_panel.Children.Add(taskControl);
                    }
                }
            }
            else
            {
                taskControls.Clear();
                taskcontrols_panel.Children.Clear();
                foreach (var botTask in TaskManagement.User_Tasks)
                {
                    TaskControl taskControl = new TaskControl(botTask, this);
                    taskControls.Add(taskControl);
                    taskcontrols_panel.Children.Add(taskControl);
                }
            }
            labTaskCount.Text = TaskManagement.User_Tasks.Count.ToString();
            foreach (var taskControl in taskControls)
            {
                taskControl.UnSelectTask();
            }
        }


        private void Window_Close_btn_Click(object sender, RoutedEventArgs e)
        {
            var response = CustomMessageBox.Show("Question", "Will you save current tasks and profiles?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (response == MessageBoxResult.Yes)
            {
                DataProcess db = new DataProcess();
                db.CreateConnection("user.db");
                db.RemoveData_profile();
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    db.InsertData_profile(temp_profile);
                }
               
                db.RemoveData_profile_mct();                               
                foreach (var temp_profile in TaskManagement.User_Profiles_MCT)
                {
                    db.InsertData_profile_MCT(temp_profile);
                }
                
                db.RemoveData_task();
                foreach (var temp_task in TaskManagement.User_Tasks)
                {
                    db.InsertData_task(temp_task);
                }
                db.Endprocess();
            }
            Application.Current.Shutdown();
        }

        private void Window_Min_btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void txtLoginID_TextChanged(object sender, TextChangedEventArgs e)
        {
            CGlobalVar.g_strSiteLoginID = txtLoginID.Text;
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            CGlobalVar.g_strSiteLoginPass = txtPassword.Text;
        }

        private void btnCreateProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            if (txtLoginID.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Email_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtLoginID.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Email_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtPassword.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Password_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtCardName.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtCardSecurity.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardCVV_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtProfileName.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var Profile in TaskManagement.User_Profiles)
            {
                if (Profile.profilename == txtProfileName.Text)
                {
                    var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Duplicate, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            profile.email = txtLoginID.Text;
            profile.password = txtPassword.Text;
            profile.cardNumber = txtCardNumber.Text;
            profile.cardName = txtCardName.Text;
            profile.cardCVV = txtCardSecurity.Text;
            profile.cardType = cmbProfileCardType.SelectedIndex.ToString();
            profile.cardMonth = cmbCardMonth.Text;
            if (cmbCardMonth.Text == "")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Please Input the Card Month", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            profile.cardYear = cmbCardYear.Text;
            if (cmbCardYear.Text == "")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Please Input the Card Year", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            profile.profilename = txtProfileName.Text;
            profile.store = "A+S";
            TaskManagement.User_Profiles.Add(profile);
            Thread notifythread = new Thread(CreateProfileNotify);
            notifythread.Start();
            RefreshProfile(false, -1);
        }

        private void btnCancelTask_Click(object sender, RoutedEventArgs e)
        {
            CreateTask_Panel.Visibility = Visibility.Hidden;
            TaskBoard.Visibility = Visibility.Visible;
        }

        private void RefreshProfile(bool init, int index)
        {
            cmbTaskProfile_MCT.Items.Clear();
            cmbTaskProfileArci.Items.Clear();
            cmbTaskProfileMortar.Items.Clear();
            cmbProfileName.Items.Clear();
            cmbProfileNameMCT.Items.Clear();
            cmbProfileName_Mortar.Items.Clear();
            cmbTaskProfileMortar.Items.Clear();

            cmbTaskProfileFTC.Items.Clear();
            cmbProfileName_FTC.Items.Clear();

            cmbTaskProfileARK.Items.Clear();
            cmbProfileName_ARK.Items.Clear();

            cmbTaskProfileZingaro.Items.Clear();
            cmbProfileName_Zingaro.Items.Clear();

            //AS
            txtLoginID.Text = "";
            txtPassword.Text = "";
            txtCardName.Text = "";
            txtCardNumber.Text = "";
            txtCardSecurity.Text = "";
            cmbCardMonth.SelectedIndex = -1;
            cmbCardYear.SelectedIndex = -1;
            cmbProfileCardType.SelectedIndex = -1;
            txtProfileName.Text = "";

            //MCT
            txtAddress1MCT.Text = "";
            txtAddress2MCT.Text = "";
            txtCardNameMCT.Text = "";
            txtCardNumberMCT.Text = "";
            txtCardSecurityMCT.Text = "";
            txtCompanyMCT.Text = "";
            txtEmailMCT.Text = "";
            txtFirstNameMCT.Text = "";
            txtLastNameMCT.Text = "";
            txtphoneMCT.Text = "";
            txtPostalCodeMCT.Text = "";
            txtprofileNameMCT.Text = "";
            txtCityMCT.Text = "";
            cmbCardYearMCT.SelectedIndex = -1;
            cmbPrefectureMCT.SelectedIndex = -1;
            cmbProfileCardTypeMCT.SelectedIndex = 0;
            cmbProfileNameMCT.SelectedIndex = -1;
            cmbCardMonthMCT.SelectedIndex = -1;


            //Mortar            
            txtLoginID_Mortar.Text = "";
            txtPassword_Mortar.Text = "";
            txtCardName_Mortar.Text = "";
            txtCardNumber_Mortar.Text = "";
            txtCardSecurity_Mortar.Text = "";
            cmbCardMonth_Mortar.SelectedIndex = -1;
            cmbCardYear_Mortar.SelectedIndex = -1;
            cmbProfileCardType_Mortar.SelectedIndex = -1;
            txtProfileName_Mortar.Text = "";

            //FTC
            txtLoginID_FTC.Text = "";
            txtPassword_FTC.Text = "";
            txtCardName_FTC.Text = "";
            txtCardNumber_FTC.Text = "";
            txtCardSecurity_FTC.Text = "";
            cmbCardMonth_FTC.SelectedIndex = -1;
            cmbCardYear_FTC.SelectedIndex = -1;
            cmbProfileCardType_FTC.SelectedIndex = -1;
            txtProfileName_FTC.Text = "";

            //ARK
            txtLoginID_ARK.Text = "";
            txtPassword_ARK.Text = "";
            txtCardName_ARK.Text = "";
            txtCardNumber_ARK.Text = "";
            txtCardSecurity_ARK.Text = "";
            cmbCardMonth_ARK.SelectedIndex = -1;
            cmbCardYear_ARK.SelectedIndex = -1;
            cmbProfileCardType_ARK.SelectedIndex = -1;
            txtProfileName_ARK.Text = "";

            //Zingaro
            txtLoginID_Zingaro.Text = "";
            txtPassword_Zingaro.Text = "";           
            txtProfileName_Zingaro.Text = "";

            if (init)
            {
                DataProcess db = new DataProcess();
                db.CreateConnection("user.db");
                try
                {
                    TaskManagement.User_Profiles = db.Read_profile();
                }
                catch
                {

                }
                try
                {
                    TaskManagement.User_Profiles_MCT = db.Read_profile_MCT();
                }
                catch
                {

                }
                db.Endprocess();
            }
            foreach (var temp_profile in TaskManagement.User_Profiles)
            {
                if (temp_profile.store == "A+S")
                {
                    cmbTaskProfileArci.Items.Add(temp_profile.profilename);
                    cmbProfileName.Items.Add(temp_profile.profilename);
                }
                else if(temp_profile.store == "MORTAR")
                {
                    cmbTaskProfileMortar.Items.Add(temp_profile.profilename);
                    cmbProfileName_Mortar.Items.Add(temp_profile.profilename);
                }else if(temp_profile.store =="FTC")
                {
                    cmbTaskProfileFTC.Items.Add(temp_profile.profilename);
                    cmbProfileName_FTC.Items.Add(temp_profile.profilename);
                }
                else if(temp_profile.store=="ARK")
                {
                    cmbTaskProfileARK.Items.Add(temp_profile.profilename);
                    cmbProfileName_ARK.Items.Add(temp_profile.profilename);
                }
                else if(temp_profile.store=="Zingaro")
                {
                    cmbTaskProfileZingaro.Items.Add(temp_profile.profilename);
                    cmbProfileName_Zingaro.Items.Add(temp_profile.profilename);
                }
            }
            foreach (var temp_profile in TaskManagement.User_Profiles_MCT)
            {
                cmbTaskProfile_MCT.Items.Add(temp_profile.profilename);
                cmbProfileNameMCT.Items.Add(temp_profile.profilename);
            }           
            try
            {
                if (index >= 0)
                {
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "A+S")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp[index];
                        txtLoginID.Text = temp.email;
                        txtPassword.Text = temp.password;
                        txtCardName.Text = temp.cardName;
                        txtCardNumber.Text = temp.cardNumber;
                        txtCardSecurity.Text = temp.cardCVV;
                        cmbCardMonth.Text = temp.cardMonth;
                        cmbCardYear.Text = temp.cardYear;
                        txtProfileName.Text = temp.profilename;
                        cmbProfileName.Text = temp.profilename;
                        cmbProfileCardType.SelectedIndex = Convert.ToInt32(temp.cardType);
                    }
                    catch
                    {

                    }
                    try
                    {
                        ProfileMCT temp_mct = TaskManagement.User_Profiles_MCT[index];
                        txtphoneMCT.Text = temp_mct.phone;
                        txtAddress1MCT.Text = temp_mct.address1;
                        txtAddress2MCT.Text = temp_mct.address2;
                        txtCardNameMCT.Text = temp_mct.cardName;
                        txtCardNumberMCT.Text = temp_mct.cardNumber;
                        txtCardSecurityMCT.Text = temp_mct.cardCVV;
                        txtCityMCT.Text = temp_mct.city;
                        txtCompanyMCT.Text = temp_mct.company;
                        txtFirstNameMCT.Text = temp_mct.firstname;
                        txtLastNameMCT.Text = temp_mct.lastname;
                        txtPostalCodeMCT.Text = temp_mct.postalcode;
                        txtprofileNameMCT.Text = temp_mct.profilename;
                        cmbProfileCardTypeMCT.SelectedIndex = 0;
                        cmbCardYearMCT.Text = temp_mct.cardYear;
                        cmbCardMonthMCT.Text = temp_mct.cardMonth;
                        cmbProfileNameMCT.Text = temp_mct.profilename;
                        cmbPrefectureMCT.Text = temp_mct.province;
                        txtEmailMCT.Text = temp_mct.email;
                    }
                    catch
                    {

                    }
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach(var temp_profile in TaskManagement.User_Profiles)
                        {
                            if(temp_profile.store=="MORTAR")
                            {
                                list_temp.Add(temp_profile);
                            }                            
                        }
                        Profile temp = list_temp[index];
                        txtLoginID_Mortar.Text = temp.email;
                        txtPassword_Mortar.Text = temp.password;
                        txtCardName_Mortar.Text = temp.cardName;
                        txtCardNumber_Mortar.Text = temp.cardNumber;
                        txtCardSecurity_Mortar.Text = temp.cardCVV;
                        cmbCardMonth_Mortar.Text = temp.cardMonth;
                        cmbCardYear_Mortar.Text = temp.cardYear;
                        txtProfileName_Mortar.Text = temp.profilename;
                        cmbProfileName_Mortar.Text = temp.profilename;
                        cmbProfileCardType_Mortar.SelectedIndex = Convert.ToInt32(temp.cardType);
                    }
                    catch
                    {

                    }
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "FTC")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp[index];
                        txtLoginID_FTC.Text = temp.email;
                        txtPassword_FTC.Text = temp.password;
                        txtCardName_FTC.Text = temp.cardName;
                        txtCardNumber_FTC.Text = temp.cardNumber;
                        txtCardSecurity_FTC.Text = temp.cardCVV;
                        cmbCardMonth_FTC.Text = temp.cardMonth;
                        cmbCardYear_FTC.Text = temp.cardYear;
                        txtProfileName_FTC.Text = temp.profilename;
                        cmbProfileName_FTC.Text = temp.profilename;
                        cmbProfileCardType_FTC.SelectedIndex = Convert.ToInt32(temp.cardType);
                    }
                    catch
                    {

                    }
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "ARK")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp[index];
                        txtLoginID_ARK.Text = temp.email;
                        txtPassword_ARK.Text = temp.password;
                        txtCardName_ARK.Text = temp.cardName;
                        txtCardNumber_ARK.Text = temp.cardNumber;
                        txtCardSecurity_ARK.Text = temp.cardCVV;
                        cmbCardMonth_ARK.Text = temp.cardMonth;
                        cmbCardYear_ARK.Text = temp.cardYear;
                        txtProfileName_ARK.Text = temp.profilename;
                        cmbProfileName_ARK.Text = temp.profilename;
                        cmbProfileCardType_ARK.SelectedIndex = Convert.ToInt32(temp.cardType);
                    }
                    catch
                    {

                    }

                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "Zingaro")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp[index];
                        txtLoginID_Zingaro.Text = temp.email;
                        txtPassword_Zingaro.Text = temp.password;                   
                        txtProfileName_Zingaro.Text = temp.profilename;
                        cmbProfileName_Zingaro.Text = temp.profilename;                        
                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "A+S")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp.Last();
                        txtLoginID.Text = temp.email;
                        txtPassword.Text = temp.password;
                        txtCardName.Text = temp.cardName;
                        txtCardNumber.Text = temp.cardNumber;
                        txtCardSecurity.Text = temp.cardCVV;
                        cmbCardMonth.Text = temp.cardMonth;
                        cmbCardYear.Text = temp.cardYear;
                        txtProfileName.Text = temp.profilename;
                        cmbProfileName.Text = temp.profilename;
                        cmbProfileCardType.SelectedIndex = Convert.ToInt32(temp.cardType);
                    }
                    catch
                    {

                    }                   
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "MORTAR")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp.Last();
                        txtLoginID_Mortar.Text = temp.email;
                        txtPassword_Mortar.Text = temp.password;
                        txtCardName_Mortar.Text = temp.cardName;
                        txtCardNumber_Mortar.Text = temp.cardNumber;
                        txtCardSecurity_Mortar.Text = temp.cardCVV;
                        cmbCardMonth_Mortar.Text = temp.cardMonth;
                        cmbCardYear_Mortar.Text = temp.cardYear;
                        txtProfileName_Mortar.Text = temp.profilename;
                        cmbProfileName_Mortar.Text = temp.profilename;
                        cmbProfileCardType_Mortar.SelectedIndex = Convert.ToInt32(temp.cardType);
                    }
                    catch
                    {

                    }
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "FTC")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp.Last();
                        txtLoginID_FTC.Text = temp.email;
                        txtPassword_FTC.Text = temp.password;
                        txtCardName_FTC.Text = temp.cardName;
                        txtCardNumber_FTC.Text = temp.cardNumber;
                        txtCardSecurity_FTC.Text = temp.cardCVV;
                        cmbCardMonth_FTC.Text = temp.cardMonth;
                        cmbCardYear_FTC.Text = temp.cardYear;
                        txtProfileName_FTC.Text = temp.profilename;
                        cmbProfileName_FTC.Text = temp.profilename;
                        cmbProfileCardType_FTC.SelectedIndex = Convert.ToInt32(temp.cardType);
                    }
                    catch
                    {

                    }
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "ARK")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp.Last();
                        txtLoginID_ARK.Text = temp.email;
                        txtPassword_ARK.Text = temp.password;
                        txtCardName_ARK.Text = temp.cardName;
                        txtCardNumber_ARK.Text = temp.cardNumber;
                        txtCardSecurity_ARK.Text = temp.cardCVV;
                        cmbCardMonth_ARK.Text = temp.cardMonth;
                        cmbCardYear_ARK.Text = temp.cardYear;
                        txtProfileName_ARK.Text = temp.profilename;
                        cmbProfileName_ARK.Text = temp.profilename;
                        cmbProfileCardType_ARK.SelectedIndex = Convert.ToInt32(temp.cardType);
                    }
                    catch
                    {

                    }
                    try
                    {
                        List<Profile> list_temp = new List<Profile>();
                        foreach (var temp_profile in TaskManagement.User_Profiles)
                        {
                            if (temp_profile.store == "Zingaro")
                            {
                                list_temp.Add(temp_profile);
                            }
                        }
                        Profile temp = list_temp.Last();
                        txtLoginID_Zingaro.Text = temp.email;
                        txtPassword_Zingaro.Text = temp.password;                   
                        txtProfileName_Zingaro.Text = temp.profilename;
                        cmbProfileName_Zingaro.Text = temp.profilename;                        
                    }
                    catch
                    {

                    }
                    try
                    {

                        ProfileMCT temp_mct = TaskManagement.User_Profiles_MCT.Last();
                        txtphoneMCT.Text = temp_mct.phone;
                        txtAddress1MCT.Text = temp_mct.address1;
                        txtAddress2MCT.Text = temp_mct.address2;
                        txtCardNameMCT.Text = temp_mct.cardName;
                        txtCardNumberMCT.Text = temp_mct.cardNumber;
                        txtCardSecurityMCT.Text = temp_mct.cardCVV;
                        txtCityMCT.Text = temp_mct.city;
                        txtCompanyMCT.Text = temp_mct.company;
                        txtFirstNameMCT.Text = temp_mct.firstname;
                        txtLastNameMCT.Text = temp_mct.lastname;
                        txtPostalCodeMCT.Text = temp_mct.postalcode;
                        txtprofileNameMCT.Text = temp_mct.profilename;
                        cmbProfileCardTypeMCT.SelectedIndex = 0;
                        cmbCardYearMCT.Text = temp_mct.cardYear;
                        cmbCardMonthMCT.Text = temp_mct.cardMonth;
                        cmbProfileNameMCT.Text = temp_mct.profilename;
                        cmbPrefectureMCT.Text = temp_mct.province;
                        txtEmailMCT.Text = temp_mct.email;
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {
                
            }

        }

        private void togLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (togLanguage.IsChecked == false)
            {
                txtLanguageSetting.Text = "EN";
                Refresh_Windows(false);
                Status.LanguageSetting = false;
            }
            else
            {
                txtLanguageSetting.Text = "JP";
                Refresh_Windows(true);
                Status.LanguageSetting = true;
            }

        }
        private void Refresh_Windows(bool opt)
        {
            if (!opt)
            {
                labDashboard.Text = EN_Constants.Dashboard;
                labeSetting.Text = EN_Constants.Setting;
                labNewTask.Text = EN_Constants.NewTask;
                labProfiles.Text = EN_Constants.Profile;
                labProxy.Text = EN_Constants.Proxy;
                labStatistics.Text = EN_Constants.Statistics;
                //labTaskBoard.Text = EN_Constants.TaskBoard;

                labProxySelect_MCT.Text = EN_Constants.SelectProxy;
                labProxySelectAS.Text = EN_Constants.SelectProxy;

                labSearchPrductAS.Text = EN_Constants.SearchProduct;

                labTaskNotify.Text = EN_Constants.TaskNotify;
                labPayment_MCT.Text = EN_Constants.SelectPayment;
                labPaymentAS.Text = EN_Constants.SelectPayment;
                labSiteType.Text = EN_Constants.SiteType;

                labWishComment.Text = EN_Constants.WishComment;

                labSelectProfile_MCT.Text = EN_Constants.SelectProfile;
                labSelectProfileAS.Text = EN_Constants.SelectProfile;

                labProductDetailsAS.Text = EN_Constants.ProductDetails;
                labCmbSizeAS.Text = EN_Constants.LabelSize;

                labCreateTask.Text = EN_Constants.CreateTask;
                labCreateTaskAS.Text = EN_Constants.CreateTask;
                labTaskCancel.Text = EN_Constants.CancelTask;
                labTaskCancelAS.Text = EN_Constants.CancelTask;

                labProductURLAS.Text = EN_Constants.ProductURL;

                labRefreshTime.Text = EN_Constants.RefreshTime;
                labRetryingLimit.Text = EN_Constants.RetryingLimit;
                labSleepTime.Text = EN_Constants.SleepTime;

                //proxypanel
                labProxyPanelName.Text = EN_Constants.ProxyPanel;
                labProxyAdd.Text = EN_Constants.ProxyAdd;
                labProxyClear.Text = EN_Constants.ProxyClear;
                labProxyCounts.Text = EN_Constants.ProxyCount;
                labProxyList.Text = EN_Constants.ProxyList;
                labProxySave.Text = EN_Constants.ProxySave;
                labProxyTest.Text = EN_Constants.ProxyTest;
                labEditProxy.Text = EN_Constants.EditProxy;
                labEditProxyAction.Text = EN_Constants.EditProxyAction;
                labEditProxyStatus.Text = EN_Constants.EditProxyStatus;
                labGroupAction.Text = EN_Constants.GroupAction;
                labGroupList.Text = EN_Constants.GroupList;
                labGroupName.Text = EN_Constants.GroupName;
                labClearGroup.Text = EN_Constants.ClearGroup;

                //setting panel
                labSettingPanel.Text = EN_Constants.SettingPanel;
                groupwebhook.Header = EN_Constants.WebHook;
                txtTestWebhook.Text = EN_Constants.TestWebhook;
                groupSoundSetting.Header = EN_Constants.GroudSoundSetting;
                if (togSizeAS.IsChecked == true)
                {
                    labSelectSizeAS.Text = EN_Constants.ManualSize;
                }
                else
                {

                    labSelectSizeAS.Text = EN_Constants.AutoSize;

                }
            }
            else
            {
                labDashboard.Text = JP_Constants.Dashboard;
                labeSetting.Text = JP_Constants.Setting;
                labNewTask.Text = JP_Constants.NewTask;
                labProfiles.Text = JP_Constants.Profile;
                labProxy.Text = JP_Constants.Proxy;
                labStatistics.Text = JP_Constants.Statistics;
                //labTaskBoard.Text = JP_Constants.TaskBoard;

                labProxySelect_MCT.Text = JP_Constants.SelectProxy;
                labProxySelectAS.Text = JP_Constants.SelectProxy;
                labPayment_MCT.Text = JP_Constants.SelectPayment;
                labPaymentAS.Text = JP_Constants.SelectPayment;
                labSiteType.Text = JP_Constants.SiteType;


                labTaskNotify.Text = JP_Constants.TaskNotify;
                labSearchPrductAS.Text = JP_Constants.SearchProduct;

                labSelectProfile_MCT.Text = JP_Constants.SelectProfile;
                labSelectProfileAS.Text = JP_Constants.SelectProfile;

                labProductDetailsAS.Text = JP_Constants.ProductDetails;
                labProductURLAS.Text = JP_Constants.ProductURL;
                labCmbSizeAS.Text = JP_Constants.LabelSize;

                labWishComment.Text = JP_Constants.WishComment;

                labCreateTask.Text = JP_Constants.CreateTask;
                labCreateTaskAS.Text = JP_Constants.CreateTask;

                labTaskCancel.Text = JP_Constants.CancelTask;
                labTaskCancelAS.Text = JP_Constants.CancelTask;


                labRefreshTime.Text = JP_Constants.RefreshTime;
                labRetryingLimit.Text = JP_Constants.RetryingLimit;
                labSleepTime.Text = JP_Constants.SleepTime;

                //proxypanel
                labProxyPanelName.Text = JP_Constants.ProxyPanel;
                labProxyAdd.Text = JP_Constants.ProxyAdd;
                labProxyClear.Text = JP_Constants.ProxyClear;
                labProxyCounts.Text = JP_Constants.ProxyCount;
                labProxyList.Text = JP_Constants.ProxyList;
                labProxySave.Text = JP_Constants.ProxySave;
                labProxyTest.Text = JP_Constants.ProxyTest;

                labEditProxy.Text = JP_Constants.EditProxy;
                labEditProxyAction.Text = JP_Constants.EditProxyAction;
                labEditProxyStatus.Text = JP_Constants.EditProxyStatus;
                labGroupAction.Text = JP_Constants.GroupAction;
                labGroupList.Text = JP_Constants.GroupList;
                labGroupName.Text = JP_Constants.GroupName;
                labClearGroup.Text = JP_Constants.ClearGroup;

                //setting panel
                labSettingPanel.Text = JP_Constants.SettingPanel;
                groupwebhook.Header = JP_Constants.WebHook;
                txtTestWebhook.Text = JP_Constants.TestWebhook;
                groupSoundSetting.Header = JP_Constants.GroudSoundSetting;
                if (togSizeAS.IsChecked == true)
                {
                    labSelectSizeAS.Text = JP_Constants.ManualSize;
                }
                else
                {
                    labSelectSizeAS.Text = JP_Constants.AutoSize;
                }
            }
        }


        private void cmbStore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (cmbStore.SelectedIndex)
                {
                    case 0:
                        if (TaskManagement.user_AS == true)
                        {                            
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Visible;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            CustomMessageBox.Show(EN_Constants.Waring, "A+S account is not verified", MessageBoxButton.OK, MessageBoxImage.None);
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            //cmbStore.SelectedIndex = 1;
                        }
                        break;
                    case 1:
                        if (TaskManagement.user_FTC == true)
                        {
                            panCreateTaskFTC.Visibility = Visibility.Visible;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            CustomMessageBox.Show(EN_Constants.Waring, "FTC account is not verified", MessageBoxButton.OK, MessageBoxImage.None);
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            //cmbStore.SelectedIndex = 0;
                        }
                        break;
                    case 2:
                        if (TaskManagement.user_ARK == true)
                        {
                            panCreateTaskARK.Visibility = Visibility.Visible;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            CustomMessageBox.Show(EN_Constants.Waring, "arktz account is not verified", MessageBoxButton.OK, MessageBoxImage.None);
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            //cmbStore.SelectedIndex = 0;
                        }
                        break;
                    case 3:
                        if (TaskManagement.user_MCT == true)
                        {
                            panCreateTaskMCT.Visibility = Visibility.Visible;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            CustomMessageBox.Show(EN_Constants.Waring, "MCT account is not verified", MessageBoxButton.OK, MessageBoxImage.None);
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            //cmbStore.SelectedIndex = 0;
                        }
                        break;
                    case 4:
                        if (TaskManagement.user_MORTAR == true)
                        {
                            panCreateTaskMortar.Visibility = Visibility.Visible;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            CustomMessageBox.Show(EN_Constants.Waring, "MORTAR account is not verified", MessageBoxButton.OK, MessageBoxImage.None);
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            //cmbStore.SelectedIndex = 0;
                        }
                        break;
                    case 5:
                        if (TaskManagement.user_ZI == true)
                        {
                            panCreateTaskZingaro.Visibility = Visibility.Visible;
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            CustomMessageBox.Show(EN_Constants.Waring, "Zingaro account is not verified", MessageBoxButton.OK, MessageBoxImage.None);
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            //cmbStore.SelectedIndex = 0;
                        }
                        break;
                    case 6:
                        if (TaskManagement.user_Zozo == true)
                        {
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Visible;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            CustomMessageBox.Show(EN_Constants.Waring, "Zozo account is not verified", MessageBoxButton.OK, MessageBoxImage.None);
                            //cmbStore.SelectedIndex = 0;
                        }
                        break;
                    case 7:
                        if (TaskManagement.user_ISB == true)
                        {
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            CustomMessageBox.Show(EN_Constants.Waring, "InstantSB account is not verified", MessageBoxButton.OK, MessageBoxImage.None);
                            panCreateTaskMortar.Visibility = Visibility.Hidden;
                            panCreateTaskMCT.Visibility = Visibility.Hidden;
                            panCreateTaskAS.Visibility = Visibility.Hidden;
                            panCreateTaskFTC.Visibility = Visibility.Hidden;
                            panCreateTaskZoZo.Visibility = Visibility.Hidden;
                            panCreateTaskARK.Visibility = Visibility.Hidden;
                            panCreateTaskZingaro.Visibility = Visibility.Hidden;
                            panCreateTaskISB.Visibility = Visibility.Hidden;
                            //cmbStore.SelectedIndex = 0;
                        }
                        break;
                }
            }
            catch
            {

            }
        }

        private void btnConfirmProductAS_Click(object sender, RoutedEventArgs e)
        {
            if (txtProuductSKU.Text == "")
            {
                if (Status.LanguageSetting == false)
                {
                    var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ERROR_INVALID_SITE_GOODNO, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var messageBoxResult = CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ERROR_INVALID_SITE_GOODNO, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return;
            }
            cmbSizeArci.Items.Clear();
            strProductSKU_AS = txtProuductSKU.Text;
            thread_getinfo = new Thread(new ThreadStart(ProcessGetInfoArci));
            thread_getinfo.IsBackground = true;
            thread_getinfo.Start();
        }

        private void ProcessGetInfoArci()
        {
            try
            {
                ArciBotLogic arciBotLogic = new ArciBotLogic();
                string search_url = Constant.g_strProductSearch_AS + strProductSKU_AS;
                string response = arciBotLogic.Get_Info(search_url);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(response);               
                List<HtmlNode> product_links = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "a" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("prd_lst_link")).ToList();
                string pid_value = product_links[0].Attributes["href"].Value;
                string product_url = Constant.g_strBase_AS + pid_value;
                strProductURL_AS = pid_value;
                response = arciBotLogic.Get_Info(product_url);
                htmlDocument.LoadHtml(response);
                var size_chip = htmlDocument.GetElementbyId("option_tbl");
                List<HtmlNode> size_nodes = size_chip.Descendants().Where(x => x.Name == "th").ToList();
                foreach (var size_node in size_nodes)
                {
                    Dispatcher.Invoke(new Action(() => cmbSizeArci.Items.Add(size_node.InnerText)));
                }
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Success, EN_Constants.GETINFO, MessageBoxButton.OK, MessageBoxImage.Warning)));
                try
                {
                    List<HtmlNode> img_nodes = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value == "inWrap").ToList();
                    string img_tag = img_nodes[0].FirstChild.Attributes["src"].Value;
                    strProductImage = img_tag;
                    imgProduct.Dispatcher.Invoke(new Action(() => imgProduct.Source = new BitmapImage(new Uri(img_tag))));
                }
                catch
                {

                }
                boolProductFound_AS = true;
            }
            catch
            {
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ERROR_PRODUCT_NOT_EXIST + "\n" + "Please Add Product manually", MessageBoxButton.OK, MessageBoxImage.Warning)));
            }

        }
        private void btnVolumn_Click(object sender, RoutedEventArgs e)
        {
            if (cntVolume.Visibility == Visibility.Visible)
            {
                cntVolume.Visibility = Visibility.Hidden;
            }
            else
            {
                cntVolume.Visibility = Visibility.Visible;
            }

        }

        private void addproxy_btn_Click(object sender, RoutedEventArgs e)
        {
            AddProxyWindow addproxy = new AddProxyWindow(this);
            addproxy.Show();
        }


        public void Refresh_Edit_Group_Proxy_List()
        {
            int index = 1;
            Edit_Proxy_Group_panel.Children.Clear();
            proxyListItems.Clear();
            myproxys.Clear();
            foreach (string proxy in temp_proxy_lists)
            {
                try
                {
                    ProxyListItem proxyListItem = new ProxyListItem(index, proxy.Trim(), this);
                    try
                    {
                        proxyListItem.set_status(speed_values[index - 1]);
                    }
                    catch
                    {

                    }
                    MyProxy myProxy = new MyProxy();
                    myProxy.url = proxy.Trim().Split(':')[0];
                    myProxy.port = proxy.Trim().Split(':')[1];
                    myProxy.user = proxy.Trim().Split(':')[2];
                    myProxy.pass = proxy.Trim().Split(':')[3];
                    myproxys.Add(myProxy);
                    Edit_Proxy_Group_panel.Children.Add(proxyListItem);
                    proxyListItems.Add(proxyListItem);
                    index++;
                }
                catch
                {

                }
            }
            proxy_count_show.Text = (index - 1).ToString();
        }

        public void Remove_Temp_Proxy(int index)
        {
            try
            {
                temp_proxy_lists.RemoveAt(index);
            }
            catch
            {

            }
            try
            {
                speed_values.RemoveAt(index);
            }
            catch
            {

            }
            Refresh_Edit_Group_Proxy_List();
        }

        private void clearproxy_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                temp_proxy_lists.Clear();
                Refresh_Edit_Group_Proxy_List();
            }
            catch
            {

            }
        }

        private void saveproxy_btn_Click(object sender, RoutedEventArgs e)
        {
            bool edit_status = false;
            if (proxy_group_name_input.Text != "")
            {
                if (edit_status == false)
                {
                    string id = GenID.Next(10000, 99999).ToString();
                    string name = proxy_group_name_input.Text;
                    string write_proxy = "";
                    foreach (string temp_proxy in temp_proxy_lists)
                    {
                        write_proxy += temp_proxy + ";";
                    }
                    DataProcess db = new DataProcess();
                    db.CreateConnection("user.db");
                    db.InsertData_proxy_group(id, name, write_proxy);
                    db.Endprocess();
                    Refresh_Proxy_Group_list();
                }
                else
                {
                    DataProcess db = new DataProcess();
                    db.CreateConnection("user.db");
                    string write_proxy = "";
                    foreach (string temp_proxy in temp_proxy_lists)
                    {
                        write_proxy += temp_proxy + ";";
                    }
                    //db.Update_proxy_group(mainitem.getProxy().group_id, proxy_group_name_input.Text, write_proxy);
                    db.Endprocess();
                    Refresh_Proxy_Group_list();
                }
            }
            else
            {
                if (Status.LanguageSetting == false)
                {
                    CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProxyGroupName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.ProxyGroupName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        public void Refresh_Proxy_Group_list()
        {
            proxyGroups.Clear();
            proxyGroupItems.Clear();
            cmbTaskProxyArci.Items.Clear();
            cmbTaskProxy_MCT.Items.Clear();
            cmbTaskProxyFTC.Items.Clear();
            proxy_wrap_panel.Children.Clear();
            DataProcess db = new DataProcess();
            db.CreateConnection("user.db");
            proxyGroups = db.ReadData_proxy_group();
            db.Endprocess();
            foreach (var proxygroup in proxyGroups)
            {
                ProxyGroupItem proxyGroupItem = new ProxyGroupItem(proxygroup, this);
                proxyGroupItems.Add(proxyGroupItem);
                proxy_wrap_panel.Children.Add(proxyGroupItem);
                cmbTaskProxyArci.Items.Add(proxygroup.group_name);
                cmbTaskProxy_MCT.Items.Add(proxygroup.group_name);
                cmbTaskProxyZozo.Items.Add(proxygroup.group_name);
                cmbTaskProxyARK.Items.Add(proxygroup.group_name);
                cmbTaskProxyMortar.Items.Add(proxygroup.group_name);
                cmbTaskProxyFTC.Items.Add(proxygroup.group_name);
            }
        }

        private void testproxy_btn_Click(object sender, RoutedEventArgs e)
        {
            speed_test = new Thread(Speed_Test);
            speed_test.Start();
        }

        private void Speed_Test()
        {
            try
            {
                for (int index = 0; index < myproxys.Count; index++)
                {
                    speed_values.Add("");
                    int result = ProxyChecker(myproxys[index]);
                    if (result == -1)
                    {
                        proxyListItems[index].set_status("Not");
                        speed_values[index] = "Not";
                    }
                    else
                    {
                        proxyListItems[index].set_status(result + "ms");
                        speed_values[index] = result + "ms";
                    }
                }
            }
            catch
            {

            }
        }
        private int ProxyChecker(MyProxy test_proxy)
        {
            try
            {
                long milliseconds1 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var proxy = new WebProxy
                {
                    Address = new Uri($"http://{test_proxy.url}:{test_proxy.port}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        userName: test_proxy.user,
                        password: test_proxy.pass)
                };
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                };
                var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                var response = client.GetAsync(Constant.g_strBase_AS).Result;
                long milliseconds2 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                return Convert.ToInt32(milliseconds2 - milliseconds1);
            }
            catch
            {
                return -1;
            }
        }

        private void clear_proxy_group_btn_Click(object sender, RoutedEventArgs e)
        {
            DataProcess db = new DataProcess();
            db.CreateConnection("user.db");
            db.RemoveData_proxy_group("");
            db.Endprocess();
            Refresh_Proxy_Group_list();
        }

        private void cmbBackGroundSound_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                str_cur_backmusic = musicfiles[cmbBackGroundSound.SelectedIndex];
                if (chkbackmusic.IsChecked == true)
                {
                    MediaPlayer wowSound = new MediaPlayer();
                    wowSound.Open(new Uri(str_cur_backmusic));
                    wowSound.Play();
                }
            }
            catch
            {

            }
        }

        private void chkbackmusic_Click(object sender, RoutedEventArgs e)
        {
            if (chkbackmusic.IsChecked == true)
            {
                try
                {
                    wowSound = new MediaPlayer();
                    wowSound.Open(new Uri(str_cur_backmusic));
                    wowSound.Play();
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    wowSound.Stop();
                }
                catch
                {

                }
            }
        }

        private void txtwebhook_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskManagement.webhook = txtwebhook.Text;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["webhook"].Value = txtwebhook.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void btnTestWebhook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var keyValues = new List<KeyValuePair<string, string>>();
                Dictionary<string, string> formdata = new Dictionary<string, string>();
                formdata.Add("username", "WebHook Test");
                formdata.Add("content", "Webhook test success");
                foreach (var element in formdata.Keys)
                {
                    keyValues.Add(new KeyValuePair<string, string>(element, formdata[element]));
                }
                var formContent = new FormUrlEncodedContent(keyValues);
                var resp = client.PostAsync(TaskManagement.webhook, formContent).Result;
            }
            catch
            {

            }
        }

        private void task_start_all_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (TaskControl tempControl in taskControls)
            {
                tempControl.bot_Start();
            }
        }

        private void chksucsound_Click(object sender, RoutedEventArgs e)
        {
            if (chksucsound.IsChecked == true)
            {
                try
                {
                    TaskManagement.success_notify = true;
                    TaskManagement.success_sound = success_musicfiles[cmbSuccessSound.SelectedIndex];
                    MediaPlayer testsound = new MediaPlayer(); //Initialize a new instance of MediaPlayer of name wowSound
                    testsound.Open(new Uri(TaskManagement.success_sound)); //Open the file for a media playback
                    testsound.Play(); //Play the media
                }
                catch
                {

                }
            }
            else
            {
                TaskManagement.success_notify = false;
            }
        }

        private void cmbSuccessSound_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chksucsound.IsChecked == true)
            {
                try
                {
                    TaskManagement.success_notify = true;
                    TaskManagement.success_sound = success_musicfiles[cmbSuccessSound.SelectedIndex];
                    MediaPlayer testsound = new MediaPlayer(); //Initialize a new instance of MediaPlayer of name wowSound
                    testsound.Open(new Uri(TaskManagement.success_sound)); //Open the file for a media playback
                    testsound.Play(); //Play the media
                }
                catch
                {

                }
            }
            else
            {
                TaskManagement.success_notify = false;
            }

        }

        private void chkwarnsnd_Click(object sender, RoutedEventArgs e)
        {
            if (chkwarnsnd.IsChecked == true)
            {
                try
                {
                    TaskManagement.warning_notify = true;
                    TaskManagement.warning_sound = warning_musicfiles[cmbWarningSound.SelectedIndex];
                    MediaPlayer testsound = new MediaPlayer(); //Initialize a new instance of MediaPlayer of name wowSound
                    testsound.Open(new Uri(TaskManagement.warning_sound)); //Open the file for a media playback
                    testsound.Play(); //Play the media
                }
                catch
                {

                }
            }
            else
            {
                TaskManagement.warning_notify = false;
            }
        }

        private void cmbWarningSound_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chkwarnsnd.IsChecked == true)
            {
                try
                {
                    TaskManagement.warning_notify = true;
                    TaskManagement.warning_sound = warning_musicfiles[cmbWarningSound.SelectedIndex];
                    MediaPlayer testsound = new MediaPlayer(); //Initialize a new instance of MediaPlayer of name wowSound
                    testsound.Open(new Uri(TaskManagement.warning_sound)); //Open the file for a media playback
                    testsound.Play(); //Play the media
                }
                catch
                {

                }
            }
            else
            {
                TaskManagement.warning_notify = false;
            }
        }

        private void cmbProfileName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string profilename = cmbProfileName.SelectedItem.ToString();
                foreach (var profile in TaskManagement.User_Profiles)
                {
                    if (profile.profilename == profilename)
                    {
                        txtProfileName.Text = profile.profilename;
                        txtLoginID.Text = profile.email;
                        txtPassword.Text = profile.password;
                        txtCardName.Text = profile.cardName;
                        txtCardNumber.Text = profile.cardNumber;
                        txtCardSecurity.Text = profile.cardCVV;
                        cmbCardMonth.Text = profile.cardMonth;
                        cmbCardYear.Text = profile.cardYear;
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnDeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProfileName.Text == "")
            {
                if (Status.LanguageSetting == false)
                {
                    CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return;
            }
            try
            {
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == cmbProfileName.SelectedItem.ToString())
                    {
                        TaskManagement.User_Profiles.Remove(temp_profile);
                        break;
                    }
                }
            }
            catch
            {

            }
            RefreshProfile(false, 0);
        }

        private void txtRetryCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                CGlobalVar.g_nRetryCount = Convert.ToInt32(txtRetryCount.Text);
            }
            catch
            {

            }
        }

        private void txtOrderSleepTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                CGlobalVar.g_nOrderSleepTime = Convert.ToInt32(txtOrderSleepTime.Text);
            }
            catch
            {

            }
        }

        private void txtRetryInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                CGlobalVar.g_nRetryInterval = Convert.ToInt32(txtRetryInterval.Text);
            }
            catch
            {

            }
        }

        private void task_stop_all_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (TaskControl tempcontrol in taskControls)
            {
                tempcontrol.bot_Stop();
            }
        }

        private void copytask_btn_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            foreach (string temp_id in TaskManagement.SeletedTasks)
            {
                foreach (var temp_task in TaskManagement.User_Tasks)
                {
                    if (temp_task.id == temp_id)
                    {
                        BotTask newtask = temp_task;
                        newtask.id = random.Next(100000, 999999).ToString();
                        TaskManagement.User_Tasks.Add(newtask);
                        break;
                    }
                }
            }
            Refresh_TaskList(false, false);
            TaskManagement.SeletedTasks.Clear();
        }

        private void task_delete_btn_Click(object sender, RoutedEventArgs e)
        {

            foreach (string temp_id in TaskManagement.SeletedTasks)
            {
                foreach (var temp_task in TaskManagement.User_Tasks)
                {
                    if (temp_task.id == temp_id)
                    {
                        TaskManagement.User_Tasks.Remove(temp_task);
                        break;
                    }
                }
            }
            Refresh_TaskList(false, true);
            TaskManagement.SeletedTasks.Clear();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (Keyboard.IsKeyDown(Key.A))
                {
                    foreach (var taskControl in taskControls)
                    {
                        taskControl.SelectTask();
                    }
                }
            }
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                foreach (var taskControl in taskControls)
                {
                    taskControl.UnSelectTask();
                }
            }
        }

        private void CreateTaskNotify()
        {
            for (int i = 0; i < 255; i += 2)
            {
                labTaskNotify.Dispatcher.Invoke(new Action(() => labTaskNotify.Foreground = new SolidColorBrush(Color.FromArgb((byte)(255 - i), 0, 255, 23))));
                Thread.Sleep(10);
            }
        }
        private void CreateProfileNotify()
        {
            for (int i = 0; i < 255; i += 2)
            {
                labProfileNotify.Dispatcher.Invoke(new Action(() => labProfileNotify.Foreground = new SolidColorBrush(Color.FromArgb((byte)(255 - i), 0, 255, 23))));
                Thread.Sleep(10);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnCreateTaskArci_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                if (txtProuductSKU.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
                if ((togSizeAS.IsChecked == false && cmbSizeArci.Text == "") || (togSizeAS.IsChecked == true && txtSizeAS.Text == ""))
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Size_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.Size_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
                if (cmbTaskProfileArci.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return;
                }
                if (cmbTaskPaymentArci.SelectedIndex == 0 && txtCreditToken.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardToken_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.CardToken_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }

                Random random = new Random();
                BotTask botTask = new BotTask();
                botTask.id = random.Next(100000, 999999).ToString();
                if (boolProductFound_AS == true)
                {
                    botTask.product_url = txtProuductSKU.Text + ";" + strProductURL_AS;
                }
                else
                {
                    botTask.product_url = txtProuductSKU.Text;
                }
                botTask.store = cmbStore.Text;
                if (togSizeAS.IsChecked == false)
                {
                    botTask.size = cmbSizeArci.Text;
                }
                else
                {
                    botTask.size = txtSizeAS.Text;
                }
                botTask.color_option = strProductImage;
                botTask.wish = txtWishArci.Text;
                botTask.token = txtCreditToken.Text;
                botTask.profilename = cmbTaskProfileArci.Text;
                botTask.cardtype = cmbTaskPaymentArci.SelectedIndex;
                botTask.activeProxy = cmbTaskProxyArci.Text;
                TaskManagement.User_Tasks.Add(botTask);
                Refresh_TaskList(false, false);
                Thread notify_thread = new Thread(CreateTaskNotify);
                notify_thread.Start();
            }
            catch
            {

            }
        }

        private void togSizeAS_Click(object sender, RoutedEventArgs e)
        {
            if (togSizeAS.IsChecked == true)
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSizeAS.Text = EN_Constants.ManualSize;
                }
                else
                {
                    labSelectSizeAS.Text = JP_Constants.ManualSize;
                }

                panAutoSize.Visibility = Visibility.Hidden;
                txtSizeAS.Visibility = Visibility.Visible;
            }
            else
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSizeAS.Text = EN_Constants.AutoSize;
                }
                else
                {
                    labSelectSizeAS.Text = JP_Constants.AutoSize;
                }
                panAutoSize.Visibility = Visibility.Visible;
                txtSizeAS.Visibility = Visibility.Hidden;
            }
        }

        private void txtProuductSKU_TextChanged(object sender, TextChangedEventArgs e)
        {
            boolProductFound_AS = false;
        }

        private void btnCreateTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {                

                if (cmbTaskProfile_MCT.Text == "")
                {
                    CustomMessageBox.Show("Please select the profile");
                    return;
                }
                
                Random random = new Random();
                BotTask botTask = new BotTask();
                botTask.id = random.Next(100000, 999999).ToString();
                if (ProductFound_MCT == true && strProductHandle_MCT != "")
                {
                    if(strProductSKUs_MCT.Count>1)
                    {
                        if(cmbProperty_MCT.SelectedIndex==-1)
                        {
                            CustomMessageBox.Show("Please Select the Color and Size");
                            return;
                        }
                    }
                    try
                    {
                        botTask.size = cmbProperty_MCT.Text;
                    }
                    catch
                    {

                    }
                    botTask.product_id = strProductHandle_MCT;
                    botTask.variant = strProductVariant_MCT;
                    botTask.image_url = strProductImage_MCT;
                    botTask.product_url = strProductName_MCt;
                    botTask.sku = strProductSKU_MCT;
                }
                else
                {
                    if(strProductInfo_MCT == "")
                    {
                        return;
                    }
                    else
                    {
                        botTask.product_url = strProductInfo_MCT;                        
                        botTask.size = txtSize_MCT + ";" + txtColor_MCT.Text;
                    }
                }

                botTask.store = cmbStore.Text;
                botTask.profilename = cmbTaskProfile_MCT.Text;
                botTask.cardtype = cmbTaskPayment_MCT.SelectedIndex;
                botTask.activeProxy = cmbTaskProxy_MCT.Text;
                TaskManagement.User_Tasks.Add(botTask);
                Thread notifythread = new Thread(CreateTaskNotify);
                notifythread.Start();
                Refresh_TaskList(false, false);
                ProductFound_MCT = false;
            }
            catch
            {

            }
        }

        private void txtGA_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskManagement.GA_token = txtGA.Text;
        }

        private void btnPreviewProduct_MCT_Click(object sender, RoutedEventArgs e)
        {           
            if(strProductInfo_MCT=="")
            {
                CustomMessageBox.Show("Please Input the Product Info");
            }
            if(strProductInfo_MCT.Contains("https"))
            {
                string temp = strProductInfo_MCT.Split('/').Last();
                if(temp.Contains("?"))
                {
                    strProductInfo_MCT = temp.Split('?')[0];
                }else
                {
                    strProductInfo_MCT = temp;
                }
            }
            Thread thrFindProduct = new Thread(FindProduct_MCT);
            thrFindProduct.Start();
        }

        private void FindProduct_MCT()
        {
            MCTBotLogic mCTBotLogic = new MCTBotLogic();
            cmbProperty_MCT.Dispatcher.Invoke(new Action(() => cmbProperty_MCT.Items.Clear()));
            strProductVariants_MCT.Clear();
            strProductSKUs_MCT.Clear();
            //first list
            string product_info1 = mCTBotLogic.Get_Product_List(Constant.g_strMCTProduct1);
            if (product_info1.Contains(strProductInfo_MCT))
            {
                JObject products = JObject.Parse(product_info1);
                JArray product_lists = JArray.Parse(products["products"].ToString());
                foreach (var product in product_lists)
                {
                    if (product.ToString().Contains(strProductInfo_MCT))
                    {
                        strProductHandle_MCT = product["handle"].ToString();
                        strProductName_MCt = product["title"].ToString();                        
                        JArray variants = JArray.Parse(product["variants"].ToString());
                        if(variants.Count>1)
                        {
                            foreach(var variant in variants)
                            {
                                strProductVariants_MCT.Add(variant["id"].ToString());
                                strProductSKUs_MCT.Add(variant["sku"].ToString());
                                cmbProperty_MCT.Dispatcher.Invoke(new Action(()=> cmbProperty_MCT.Items.Add(variant["option1"].ToString())));
                            }
                        }
                        else
                        {
                            strProductVariant_MCT = variants[0]["id"].ToString();
                            strProductSKU_MCT = variants[0]["sku"].ToString();
                        }                        
                        
                        JArray imgs = JArray.Parse(product["images"].ToString());
                        strProductImage_MCT = imgs[0]["src"].ToString();                       
                        imgProduct_MCT.Dispatcher.Invoke(new Action(() => imgProduct_MCT.Source = new BitmapImage(new Uri(strProductImage_MCT))));
                        break;
                    }
                }
                ProductFound_MCT = true;
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show("Product Success")));
                return;
            }

            //second list
            string product_info2 = mCTBotLogic.Get_Product_List(Constant.g_strMCTProduct2);
            if (product_info2.Contains(strProductInfo_MCT))
            {
                JObject products = JObject.Parse(product_info1);
                JArray product_lists = JArray.Parse(products["products"].ToString());
                foreach (var product in product_lists)
                {
                    if (product.ToString().Contains(strProductInfo_MCT))
                    {
                        strProductHandle_MCT = product["handle"].ToString();
                        strProductName_MCt = product["title"].ToString();
                        JArray variants = JArray.Parse(product["variants"].ToString());
                        if (variants.Count > 1)
                        {
                            foreach (var variant in variants)
                            {
                                strProductVariants_MCT.Add(variant["id"].ToString());
                                strProductSKUs_MCT.Add(variant["sku"].ToString());
                                cmbProperty_MCT.Dispatcher.Invoke(new Action(() => cmbProperty_MCT.Items.Add(variant["option1"].ToString())));
                            }
                        }
                        else
                        {
                            strProductVariant_MCT = variants[0]["id"].ToString();
                            strProductSKU_MCT = variants[0]["sku"].ToString();
                        }
                        JArray imgs = JArray.Parse(product["images"].ToString());
                        strProductImage_MCT = imgs[0]["src"].ToString();
                        imgProduct_MCT.Dispatcher.Invoke(new Action(() => imgProduct_MCT.Source = new BitmapImage(new Uri(strProductImage_MCT))));
                        break;
                    }
                }
                ProductFound_MCT = true;
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show("Product Success")));
                return;
            }
            Dispatcher.Invoke(new Action(() => CustomMessageBox.Show("Product not Found")));
        }

        private void DumpFile(string content, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(content);
            }
        }

        private void btnImportProfileMCT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;
                    Dictionary<string, string> profile = new Dictionary<string, string>();
                    List<Dictionary<string, string>> lstProfiles = new List<Dictionary<string, string>>();
                    string str_profiles = File.ReadAllText(filename);
                    JArray jarray_profiles = JArray.Parse(str_profiles);
                    if (jarray_profiles.Count == 0)
                    {
                        return;
                    }
                    lstProfiles = new List<Dictionary<string, string>>();
                    foreach (var json_profile in jarray_profiles)
                    {
                        Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_profile.ToString());
                        lstProfiles.Add(temp);
                    }
                    foreach (var dic_temp_profile in lstProfiles)
                    {
                        ProfileMCT temp_profile = new ProfileMCT();
                        temp_profile.profilename = dic_temp_profile["profilename"];
                        temp_profile.email = dic_temp_profile["email"];
                        temp_profile.firstname = dic_temp_profile["firstname"];
                        temp_profile.lastname = dic_temp_profile["lastname"];
                        temp_profile.phone = dic_temp_profile["phone"];
                        temp_profile.address1 = dic_temp_profile["address1"];
                        temp_profile.address2 = dic_temp_profile["address2"];
                        temp_profile.city = dic_temp_profile["city"];
                        temp_profile.postalcode = dic_temp_profile["postalcode"];
                        temp_profile.cardNumber = dic_temp_profile["cardnumber"];
                        temp_profile.cardCVV = dic_temp_profile["securitycode"];
                        temp_profile.province = dic_temp_profile["state"];
                        temp_profile.cardYear = dic_temp_profile["expire_year"];
                        temp_profile.cardMonth = dic_temp_profile["expire_month"];
                        temp_profile.cardType = "0";
                        temp_profile.cardName = dic_temp_profile["cardname"];
                        TaskManagement.User_Profiles_MCT.Add(temp_profile);
                    }
                    RefreshProfile(false, 0);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnCreateProfileMCT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteProfileMCT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string del_profilename = cmbProfileNameMCT.Text;
                foreach (var temp_profile in TaskManagement.User_Profiles_MCT)
                {
                    if (temp_profile.profilename == del_profilename)
                    {
                        TaskManagement.User_Profiles_MCT.Remove(temp_profile);
                        break;
                    }
                }
                RefreshProfile(false, 0);
            }
            catch (Exception ee)
            {
                CustomMessageBox.Show(ee.Message);
            }
        }

        private void btnImportProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;
                    Dictionary<string, string> profile = new Dictionary<string, string>();
                    List<Dictionary<string, string>> lstProfiles = new List<Dictionary<string, string>>();
                    string str_profiles = File.ReadAllText(filename);
                    JArray jarray_profiles = JArray.Parse(str_profiles);
                    if (jarray_profiles.Count == 0)
                    {
                        return;
                    }
                    lstProfiles = new List<Dictionary<string, string>>();
                    foreach (var json_profile in jarray_profiles)
                    {
                        Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_profile.ToString());
                        lstProfiles.Add(temp);
                    }
                    foreach (var dic_temp_profile in lstProfiles)
                    {
                        Profile temp_profile = new Profile();
                        temp_profile.profilename = dic_temp_profile["profilename"];
                        temp_profile.email = dic_temp_profile["email"];
                        temp_profile.password = dic_temp_profile["password"];
                        temp_profile.cardNumber = dic_temp_profile["cardnumber"];
                        temp_profile.cardCVV = dic_temp_profile["securitycode"];
                        temp_profile.cardYear = dic_temp_profile["expire_year"];

                        if (dic_temp_profile["expire_month"].Length == 1)
                        {
                            temp_profile.cardMonth = "0" + dic_temp_profile["expire_month"];
                        }
                        else
                        {
                            temp_profile.cardMonth = dic_temp_profile["expire_month"];
                        }
                        temp_profile.cardType = "0";
                        temp_profile.cardName = dic_temp_profile["cardname"];
                        temp_profile.store = "A+S";
                        TaskManagement.User_Profiles.Add(temp_profile);
                    }
                    RefreshProfile(false, 0);
                }
            }
            catch (Exception ee)
            {
                CustomMessageBox.Show(ee.Message);
            }
        }

        private void cmbProfileNameMCT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string profilename = cmbProfileNameMCT.SelectedItem.ToString();
                foreach (var temp_mct in TaskManagement.User_Profiles_MCT)
                {
                    if (temp_mct.profilename == profilename)
                    {
                        txtphoneMCT.Text = temp_mct.phone;
                        txtAddress1MCT.Text = temp_mct.address1;
                        txtAddress2MCT.Text = temp_mct.address2;
                        txtCardNameMCT.Text = temp_mct.cardName;
                        txtCardNumberMCT.Text = temp_mct.cardNumber;
                        txtCardSecurityMCT.Text = temp_mct.cardCVV;
                        txtCityMCT.Text = temp_mct.city;
                        txtCompanyMCT.Text = temp_mct.company;
                        txtFirstNameMCT.Text = temp_mct.firstname;
                        txtLastNameMCT.Text = temp_mct.lastname;
                        txtPostalCodeMCT.Text = temp_mct.postalcode;
                        txtprofileNameMCT.Text = temp_mct.profilename;
                        cmbProfileCardTypeMCT.SelectedIndex = 0;
                        cmbCardYearMCT.Text = temp_mct.cardYear;
                        cmbCardMonthMCT.Text = temp_mct.cardMonth;
                        cmbProfileNameMCT.Text = temp_mct.profilename;
                        cmbPrefectureMCT.Text = temp_mct.province;
                        break;
                    }
                }
            }
            catch
            {

            }
        }


        private void radURL_Click(object sender, RoutedEventArgs e)
        {
            if (radURL.IsChecked == true)
            {
                txtProductURL_MCT.Visibility = Visibility.Visible;
                txtProductID_MCT.Visibility = Visibility.Hidden;
                txtProductID_MCT.Text = "";
                txtProductKeyword_MCT.Visibility = Visibility.Hidden;
                txtProductKeyword_MCT.Text = "";
                txtProductSKU_MCT.Visibility = Visibility.Hidden;
                txtProductSKU_MCT.Text = "";

            }
            else
            {
                txtProductURL_MCT.Visibility = Visibility.Hidden;
                txtProductURL_MCT.Text = "";
            }
        }

        private void radSKU_MCT_Click(object sender, RoutedEventArgs e)
        {
            if (radSKU_MCT.IsChecked == true)
            {
                txtProductURL_MCT.Visibility = Visibility.Hidden;
                txtProductURL_MCT.Text = "";
                txtProductID_MCT.Visibility = Visibility.Hidden;
                txtProductID_MCT.Text = "";
                txtProductKeyword_MCT.Visibility = Visibility.Hidden;
                txtProductKeyword_MCT.Text = "";
                txtProductSKU_MCT.Visibility = Visibility.Visible;
            }
            else
            {
                txtProductSKU_MCT.Visibility = Visibility.Hidden;
                txtProductSKU_MCT.Text = "";
            }
        }

        private void radID_MCT_Click(object sender, RoutedEventArgs e)
        {
            if (radID_MCT.IsChecked == true)
            {
                txtProductURL_MCT.Visibility = Visibility.Hidden;
                txtProductURL_MCT.Text = "";
                txtProductID_MCT.Visibility = Visibility.Visible;
                txtProductKeyword_MCT.Visibility = Visibility.Hidden;
                txtProductKeyword_MCT.Text = "";
                txtProductSKU_MCT.Visibility = Visibility.Hidden;
                txtProductSKU_MCT.Text = "";
            }
            else
            {
                txtProductID_MCT.Visibility = Visibility.Hidden;
                txtProductID_MCT.Text = "";
            }
        }

        private void radKeyword_Click(object sender, RoutedEventArgs e)
        {
            if (radKeyword.IsChecked == true)
            {
                txtProductURL_MCT.Visibility = Visibility.Hidden;
                txtProductURL_MCT.Text = "";
                txtProductID_MCT.Visibility = Visibility.Hidden;
                txtProductID_MCT.Text = "";
                txtProductKeyword_MCT.Visibility = Visibility.Visible;
                txtProductSKU_MCT.Visibility = Visibility.Hidden;
                txtProductSKU_MCT.Text = "";
            }
            else
            {
                txtProductID_MCT.Visibility = Visibility.Visible;
                txtProductID_MCT.Text = "";
            }
        }

        private void txtProductURL_MCT_TextChanged(object sender, TextChangedEventArgs e)
        {
            strProductInfo_MCT = txtProductURL_MCT.Text;
        }

        private void txtProductSKU_MCT_TextChanged(object sender, TextChangedEventArgs e)
        {
            strProductInfo_MCT = txtProductSKU_MCT.Text;
        }

        private void txtProductID_MCT_TextChanged(object sender, TextChangedEventArgs e)
        {
            strProductInfo_MCT = txtProductID_MCT.Text;
        }

        private void txtProductKeyword_MCT_TextChanged(object sender, TextChangedEventArgs e)
        {
            strProductInfo_MCT = txtProductKeyword_MCT.Text;
        }




        private void cmbStore_Profile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (cmbStore_Profile.SelectedIndex)
                {
                    case 0:
                        panCreateAccount_AS.Visibility = Visibility.Visible;
                        panCreateAccount_MCT.Visibility = Visibility.Hidden;
                        panCreateAccount_FTC.Visibility = Visibility.Hidden;
                        panCreateAccount_Mortar.Visibility = Visibility.Hidden;
                        panCreateAccount_ARK.Visibility = Visibility.Hidden;
                        panCreateAccount_Zingaro.Visibility = Visibility.Hidden;
                        break;
                    case 1:
                        panCreateAccount_FTC.Visibility = Visibility.Visible;
                        panCreateAccount_MCT.Visibility = Visibility.Hidden;
                        panCreateAccount_AS.Visibility = Visibility.Hidden;
                        panCreateAccount_Mortar.Visibility = Visibility.Hidden;
                        panCreateAccount_ARK.Visibility = Visibility.Hidden;
                        panCreateAccount_Zingaro.Visibility = Visibility.Hidden;
                        break;
                    case 2:
                        panCreateAccount_ARK.Visibility = Visibility.Visible;
                        panCreateAccount_FTC.Visibility = Visibility.Hidden;
                        panCreateAccount_MCT.Visibility = Visibility.Hidden;
                        panCreateAccount_AS.Visibility = Visibility.Hidden;
                        panCreateAccount_Mortar.Visibility = Visibility.Hidden;
                        panCreateAccount_Zingaro.Visibility = Visibility.Hidden;
                        break;
                    case 3:
                        panCreateAccount_FTC.Visibility = Visibility.Hidden;
                        panCreateAccount_MCT.Visibility = Visibility.Visible;
                        panCreateAccount_AS.Visibility = Visibility.Hidden;
                        panCreateAccount_ARK.Visibility = Visibility.Hidden;
                        panCreateAccount_Mortar.Visibility = Visibility.Hidden;
                        panCreateAccount_Zingaro.Visibility = Visibility.Hidden;
                        break;
                    case 4:
                        panCreateAccount_FTC.Visibility = Visibility.Hidden;
                        panCreateAccount_MCT.Visibility = Visibility.Hidden;
                        panCreateAccount_AS.Visibility = Visibility.Hidden;
                        panCreateAccount_ARK.Visibility = Visibility.Hidden;
                        panCreateAccount_Mortar.Visibility = Visibility.Visible;
                        panCreateAccount_Zingaro.Visibility = Visibility.Hidden;
                        break;
                    case 5:
                        panCreateAccount_FTC.Visibility = Visibility.Hidden;
                        panCreateAccount_MCT.Visibility = Visibility.Hidden;
                        panCreateAccount_AS.Visibility = Visibility.Hidden;
                        panCreateAccount_Mortar.Visibility = Visibility.Hidden;
                        panCreateAccount_ARK.Visibility = Visibility.Hidden;
                        panCreateAccount_Zingaro.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }
        }

        private void btnDeleteProfile_Mortar_Click(object sender, RoutedEventArgs e)
        {
            string selected_profilename = cmbProfileName_Mortar.Text;
            try
            {
                foreach (var profile in TaskManagement.User_Profiles)
                {
                    if (profile.profilename == selected_profilename)
                    {
                        TaskManagement.User_Profiles.Remove(profile);
                        break;
                    }
                }
            }
            catch
            {

            }
            RefreshProfile(false, 0);

        }

        private void btnCreateProfile_Mortar_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            if (txtLoginID_Mortar.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Email_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtPassword_Mortar.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Password_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtCardName_Mortar.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtCardSecurity_Mortar.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardCVV_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtProfileName_Mortar.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var Profile in TaskManagement.User_Profiles)
            {
                if (Profile.profilename == txtProfileName.Text)
                {
                    var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Duplicate, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            profile.email = txtLoginID_Mortar.Text;
            profile.password = txtPassword_Mortar.Text;
            profile.cardNumber = txtCardNumber_Mortar.Text;
            profile.cardName = txtCardName_Mortar.Text;
            profile.cardCVV = txtCardSecurity_Mortar.Text;
            profile.cardType = cmbProfileCardType_Mortar.SelectedIndex.ToString();
            profile.cardMonth = cmbCardMonth_Mortar.Text;
            if (cmbCardMonth_Mortar.Text == "")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Please Input the Expire Month", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            profile.cardYear = cmbCardYear_Mortar.Text;
            if (cmbCardYear_Mortar.Text == "")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Please Input the Expire Year", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            profile.profilename = txtProfileName_Mortar.Text;
            profile.store = "MORTAR";
            TaskManagement.User_Profiles.Add(profile);
            Thread notifythread = new Thread(CreateProfileNotify);
            notifythread.Start();
            RefreshProfile(false, -1);
        }

        private void btnImportProfile_Mortar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;
                    Dictionary<string, string> profile = new Dictionary<string, string>();
                    List<Dictionary<string, string>> lstProfiles = new List<Dictionary<string, string>>();
                    string str_profiles = File.ReadAllText(filename);
                    JArray jarray_profiles = JArray.Parse(str_profiles);
                    if (jarray_profiles.Count == 0)
                    {
                        return;
                    }
                    lstProfiles = new List<Dictionary<string, string>>();
                    foreach (var json_profile in jarray_profiles)
                    {
                        Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_profile.ToString());
                        lstProfiles.Add(temp);
                    }
                    foreach (var dic_temp_profile in lstProfiles)
                    {
                        Profile temp_profile = new Profile();
                        temp_profile.profilename = dic_temp_profile["profilename"];
                        temp_profile.email = dic_temp_profile["email"];
                        temp_profile.password = dic_temp_profile["password"];
                        temp_profile.cardNumber = dic_temp_profile["cardnumber"];
                        temp_profile.cardCVV = dic_temp_profile["securitycode"];
                        temp_profile.cardYear = dic_temp_profile["expire_year"];
                        if (dic_temp_profile["expire_month"].Length == 1)
                        {
                            temp_profile.cardMonth = "0" + dic_temp_profile["expire_month"];
                        }
                        else
                        {
                            temp_profile.cardMonth = dic_temp_profile["expire_month"];
                        }
                        temp_profile.cardType = "0";
                        temp_profile.cardName = dic_temp_profile["cardname"];
                        temp_profile.store = "MORTAR";
                        TaskManagement.User_Profiles.Add(temp_profile);
                    }
                    RefreshProfile(false, 0);
                }
            }
            catch (Exception ee)
            {
                CustomMessageBox.Show(ee.Message);
            }
        }

        private void btnCreateTaskMortar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductURL_Mortar.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
                if ((togSizeMortar.IsChecked == false && cmbSizeMortar.Text == "") || (togSizeMortar.IsChecked == true && txtSizeMortar.Text == ""))
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Size_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.Size_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
                if (cmbTaskProfileMortar.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return;
                }
                //if (txtCreditToken.Text == "")
                //{
                //    if (Status.LanguageSetting == false)
                //    {
                //        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardToken_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                //    }
                //    else
                //    {
                //        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.CardToken_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                //    }
                //    return;
                //}

                Random random = new Random();
                BotTask botTask = new BotTask();
                botTask.id = random.Next(100000, 999999).ToString();
                botTask.product_url = txtProductURL_Mortar.Text;
                botTask.store = cmbStore.Text;

                if (togSizeMortar.IsChecked == false)
                {
                    botTask.size = cmbSizeMortar.Text;
                }
                else
                {
                    botTask.size = txtSizeMortar.Text;
                }
                botTask.color_option = strProductImage;
                botTask.wish = txtWishMortar.Text;
                if (txtToken1.Text.Split(';').ToList().Count!=7)
                {
                    CustomMessageBox.Show(EN_Constants.Waring, "Invalid Token 1", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtToken1.Text.Contains("null"))
                {
                    CustomMessageBox.Show(EN_Constants.Waring, "Invalid Token 1", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtToken2.Text=="")
                {
                    CustomMessageBox.Show(EN_Constants.Waring, "Invalid Token 2", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //string temp1_token = jsontoken.Replace("token1_value", txtToken1.Text);
                //string temp2_token = temp1_token.Replace("token2_value", txtToken2.Text);
                //string temp3_token = temp2_token.Replace("token3_value", txtToken3.Text);
                //StringContent token_content = new StringContent(temp3_token);
                botTask.token = txtToken1.Text + ";" + txtToken2.Text;
                botTask.profilename = cmbTaskProfileMortar.Text;
                botTask.cardtype = cmbTaskPaymentMortar.SelectedIndex;
                botTask.activeProxy = cmbTaskProxyMortar.Text;
                botTask.profilename = cmbTaskProfileMortar.Text;
                TaskManagement.User_Tasks.Add(botTask);
                Refresh_TaskList(false, false);
                Thread notify_thread = new Thread(CreateTaskNotify);
                notify_thread.Start();
            }
            catch
            {

            }
        }

        private void togSizeMortar_Click(object sender, RoutedEventArgs e)
        {
            if (togSizeMortar.IsChecked == true)
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSize_Mortar.Text = EN_Constants.ManualSize;
                }
                else
                {
                    labSelectSize_Mortar.Text = JP_Constants.ManualSize;
                }

                panAutoSize_Mortar.Visibility = Visibility.Hidden;
                txtSizeMortar.Visibility = Visibility.Visible;
            }
            else
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSize_Mortar.Text = EN_Constants.AutoSize;
                }
                else
                {
                    labSelectSize_Mortar.Text = JP_Constants.AutoSize;
                }
                panAutoSize_Mortar.Visibility = Visibility.Visible;
                txtSizeMortar.Visibility = Visibility.Hidden;
            }
        }

        private void btnConfirmProductMortar_Click(object sender, RoutedEventArgs e)
        {
            strProductURL_Mortar = txtProductURL_Mortar.Text;
            SearchProduct_Mortar();           
        }
        private void SearchProduct_Mortar()
        {
            Random random = new Random();

            HttpClient httpClient;
            try
            {
                //size_release.Clear();
                cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Clear()));
                HttpClientHandler clientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                httpClient = new HttpClient(clientHandler);
                string HtmlString = httpClient.GetAsync(strProductURL_Mortar).Result.Content.ReadAsStringAsync().Result;
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(HtmlString);
                List<HtmlNode> products_node = htmlDoc.DocumentNode.Descendants().Where(x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value == "main_content_result")).ToList();
                List<HtmlNode> product_lists = products_node[0].Descendants().Where(x => (x.Name == "ul" && x.Attributes["class"] != null && x.Attributes["class"].Value == "main_content_result_item_list")).ToList();
                if (product_lists.Count > 0)
                {
                    //detect sizes                        
                    List<HtmlNode> size_items = product_lists[0].Descendants().Where(x => (x.Name == "li")).ToList();
                    List<string> size_texts = new List<string>();
                    foreach (var item in size_items)
                    {
                        List<HtmlNode> buttons = item.Descendants().Where(x => (x.Name == "button")).ToList();
                        if (buttons.Count == 1)
                        {
                            //size_release.Add(true);
                        }
                        else
                        {
                            //size_release.Add(false);
                        }
                        List<HtmlNode> sold_outs = item.Descendants().Where(x => (x.Name == "p" && x.Attributes["class"].Value.Contains("soldout"))).ToList();
                        if (sold_outs.Count == 0)
                        {
                            size_texts.Add(item.Descendants().Where(x => (x.Name == "div" && x.Attributes["class"].Value == "variation_name")).ToList()[0].InnerText.Trim());
                        }
                        else
                        {
                            size_texts.Add(item.Descendants().Where(x => (x.Name == "div" && x.Attributes["class"].Value == "variation_name")).ToList()[0].InnerText.Trim() + ':' + sold_outs[0].InnerText.Trim());
                        }

                    }
                    //add possible sizes
                    foreach (var size_text in size_texts)
                    {
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add(size_text.Replace(" ", ""))));
                        List<HtmlNode> cartbtn = products_node[0].Descendants().Where(x => (x.Name == "button" && x.Attributes["class"] != null)).ToList();
                        if (cartbtn.Count > 0)
                        {
                            // size_release.Add(true);
                        }
                        else
                        {
                            //  size_release.Add(false);
                        }
                    }
                }
                else
                {
                    if (products_node[0].Descendants().Where(x => x.Name == "button").ToList().Count == 1)
                    {
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("One Size")));
                    }
                    else
                    {
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("24.0")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("24.5")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("25.0")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("25.5")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("26.0")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("26.5")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("27.0")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("27.5")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("28.0")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("28.5")));
                        cmbSizeMortar.Dispatcher.Invoke(new Action(() => cmbSizeMortar.Items.Add("29.0")));
                    }
                }
                // preview product
                List<HtmlNode> img_divs = htmlDoc.DocumentNode.Descendants().Where(x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("main_content_gallery_image"))).ToList();
                List<HtmlNode> img_srcs = img_divs[0].Descendants().Where(x => (x.Name == "img" && x.Attributes["src"] != null)).ToList();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(img_srcs[0].Attributes["src"].Value, UriKind.Absolute);
                bitmap.EndInit();
                imgProductMortar.Source = bitmap;
                CustomMessageBox.Show("Success", "Product Found.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                CustomMessageBox.Show("Error", "Product Not Found.", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        /// <summary>
        /// Create Task for Zozo
        /// </summary>        

        private void btnConfirmProductZozo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart_opt.Clear();
                List<string> sizes = new List<string>();
                ZozoBotLogic zozoBot = new ZozoBotLogic();
                var source_page = zozoBot.Get_ProductPage(txtProductURL_Zozo.Text);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(source_page);
                var sizes_body = htmlDocument.GetElementbyId("goodsRight").Descendants().Where(x => x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value == "blockMain").ToList()[0];
                var size_tags = sizes_body.Descendants().Where(x => x.Name == "ul").ToList()[0].Descendants().Where(x => x.Name == "li").ToList();
                foreach (var size_tag in size_tags)
                {
                    string size = size_tag.Attributes["data-size"].Value;
                    var inputs = size_tag.Descendants().Where(x => x.Name == "input" && x.Attributes["type"] != null && x.Attributes["type"].Value == "hidden").ToList();                    
                    string cart_option = "";
                    if (inputs.Count == 7)
                    {
                        foreach (var input_tag in inputs)
                        {
                            cart_option += input_tag.Attributes["value"].Value + ";";                           
                        }
                    }                    
                    sizes.Add(size);
                    cart_opt.Add(cart_option);
                }
                cmbSizeZozo.ItemsSource = sizes;
                try
                {
                    strImageUrl_Zozo = htmlDocument.GetElementbyId("photoMain").Descendants().Where(x => x.Name == "img").ToList()[0].Attributes["src"].Value;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(strImageUrl_Zozo, UriKind.Absolute);
                    bitmap.EndInit();
                    imgProductZozo.Source = bitmap;
                }
                catch
                {

                }
                CustomMessageBox.Show("Success", "Product Found.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                CustomMessageBox.Show("Error", "Product not Found.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnCreateTaskZozo_Click(object sender, RoutedEventArgs e)
        {            
            string size = "";
            string cart_option = "";
            if(togSizeZozo.IsChecked==true)
            {
                if(txtSizeZozo.Text=="")
                {
                    CustomMessageBox.Show("Error", "Please Input the size manually.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                size = txtSizeZozo.Text;
                cart_option = "";
            }
            else
            {
                if(cmbSizeZozo.SelectedIndex<0)
                {
                    CustomMessageBox.Show("Error", "Please Select the size.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                size = cmbSizeZozo.Text;
                cart_option = cart_opt[cmbSizeZozo.SelectedIndex];
            }

            if(cmbTaskPaymentZozo.SelectedIndex<0)
            {
                CustomMessageBox.Show("Error", "Please Select the Payment Method", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Random random = new Random();
            BotTask botTask = new BotTask();
            botTask.id = random.Next(100000, 999999).ToString();
            botTask.store = "zozo";
            botTask.product_url = txtProductURL_Zozo.Text;
            botTask.size = size;
            botTask.sku = cart_option;
            botTask.cardtype = cmbTaskPaymentZozo.SelectedIndex;
            botTask.activeProxy = cmbTaskProxyZozo.Text;
            botTask.profilename = txtEmailZozo.Text;
            TaskManagement.User_Tasks.Add(botTask);
            Refresh_TaskList(false, false);
            Thread thr = new Thread(CreateTaskNotify);
            thr.Start();
        }

        private void btnCancelTaskZozo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void togSizeZozo_Click(object sender, RoutedEventArgs e)
        {
            if (togSizeZozo.IsChecked == true)
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSize_Zozo.Text = EN_Constants.ManualSize;
                }
                else
                {
                    labSelectSize_Zozo.Text = JP_Constants.ManualSize;
                }

                panAutoSize_Zozo.Visibility = Visibility.Hidden;
                txtSizeZozo.Visibility = Visibility.Visible;
            }
            else
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSize_Zozo.Text = EN_Constants.AutoSize;
                }
                else
                {
                    labSelectSize_Zozo.Text = JP_Constants.AutoSize;
                }
                panAutoSize_Zozo.Visibility = Visibility.Visible;
                txtSizeZozo.Visibility = Visibility.Hidden;
            }
        }



        private void cmbProfileName_Mortar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = cmbProfileName_Mortar.SelectedIndex;

                var selected_profilename = cmbProfileName_Mortar.SelectedItem.ToString();
                foreach(var temp in TaskManagement.User_Profiles)
                {
                    if(temp.profilename==selected_profilename)
                    {
                        txtLoginID_Mortar.Text = temp.email;
                        txtPassword_Mortar.Text = temp.password;
                        txtCardName_Mortar.Text = temp.cardName;
                        txtCardNumber_Mortar.Text = temp.cardNumber;
                        txtCardSecurity_Mortar.Text = temp.cardCVV;
                        cmbCardMonth_Mortar.Text = temp.cardMonth;
                        cmbCardYear_Mortar.Text = temp.cardYear;
                        txtProfileName_Mortar.Text = temp.profilename;
                        cmbProfileName_Mortar.Text = temp.profilename;
                        cmbProfileCardType_Mortar.SelectedIndex = Convert.ToInt32(temp.cardType);
                        break;
                    }
                }             
            }
            catch
            {

            }
        }

        private void task_save_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataProcess db = new DataProcess();
                db.CreateConnection("user.db");
                db.RemoveData_profile();
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    db.InsertData_profile(temp_profile);
                }

                db.RemoveData_profile_mct();
                foreach (var temp_profile in TaskManagement.User_Profiles_MCT)
                {
                    db.InsertData_profile_MCT(temp_profile);
                }
                
                db.RemoveData_task();
                foreach (var temp_task in TaskManagement.User_Tasks)
                {
                    db.InsertData_task(temp_task);
                }
                db.Endprocess();
                CustomMessageBox.Show("Success", "All Data is Saved Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                CustomMessageBox.Show("Error", "Save Failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }









        /// <summary>
        /// /////////////------------------FTC----------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateTaskFTC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProuductSKU_FTC.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
                if ((togSizeFTC.IsChecked == false && cmbSizeFTC.Text == "") || (togSizeFTC.IsChecked == true && txtSizeFTC.Text == ""))
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Size_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.Size_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
                if (cmbTaskProfileFTC.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return;
                }
                if (cmbTaskPaymentFTC.SelectedIndex == 0 && txtCreditTokenFTC.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardToken_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.CardToken_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }

                Random random = new Random();
                BotTask botTask = new BotTask();
                botTask.id = random.Next(100000, 999999).ToString();
                if (boolProductFound_FTC == true)
                {
                    botTask.product_url = txtProuductSKU_FTC.Text + ";" + strProductURL_FTC;
                }
                else
                {
                    botTask.product_url = txtProuductSKU_FTC.Text;
                }
                botTask.store = cmbStore.Text;
                if (togSizeFTC.IsChecked == false)
                {
                    botTask.size = cmbSizeFTC.Text;
                }
                else
                {
                    botTask.size = txtSizeFTC.Text;
                }
                botTask.color_option = strProductImage_FTC;
                botTask.wish = txtWishFTC.Text;
                botTask.token =txtCreditTokenFTC.Text;
                botTask.profilename = cmbTaskProfileFTC.Text;
                botTask.cardtype = cmbTaskPaymentFTC.SelectedIndex;
                botTask.activeProxy = cmbTaskProxyFTC.Text;
                TaskManagement.User_Tasks.Add(botTask);
                Refresh_TaskList(false, false);
                Thread notify_thread = new Thread(CreateTaskNotify);
                notify_thread.Start();
            }
            catch
            {

            }
        }

        private void btnCancelTaskFTC_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnImportProfile_FTC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;
                    Dictionary<string, string> profile = new Dictionary<string, string>();
                    List<Dictionary<string, string>> lstProfiles = new List<Dictionary<string, string>>();
                    string str_profiles = File.ReadAllText(filename);
                    JArray jarray_profiles = JArray.Parse(str_profiles);
                    if (jarray_profiles.Count == 0)
                    {
                        return;
                    }
                    lstProfiles = new List<Dictionary<string, string>>();
                    foreach (var json_profile in jarray_profiles)
                    {
                        Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_profile.ToString());
                        lstProfiles.Add(temp);
                    }
                    foreach (var dic_temp_profile in lstProfiles)
                    {
                        Profile temp_profile = new Profile();
                        temp_profile.profilename = dic_temp_profile["profilename"];
                        temp_profile.email = dic_temp_profile["email"];
                        temp_profile.password = dic_temp_profile["password"];
                        temp_profile.cardNumber = dic_temp_profile["cardnumber"];
                        temp_profile.cardCVV = dic_temp_profile["securitycode"];
                        temp_profile.cardYear = dic_temp_profile["expire_year"];
                        if (dic_temp_profile["expire_month"].Length == 1)
                        {
                            temp_profile.cardMonth = "0" + dic_temp_profile["expire_month"];
                        }
                        else
                        {
                            temp_profile.cardMonth = dic_temp_profile["expire_month"];
                        }
                        temp_profile.cardType = "0";
                        temp_profile.cardName = dic_temp_profile["cardname"];
                        temp_profile.store = "FTC";
                        TaskManagement.User_Profiles.Add(temp_profile);
                    }
                    RefreshProfile(false, 0);
                }
            }
            catch (Exception ee)
            {
                CustomMessageBox.Show(ee.Message);
            }
        }



        private void btnCreateProfile_FTC_Click(object sender, RoutedEventArgs e)
        {

            Profile profile = new Profile();
            if (txtLoginID_FTC.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Email_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtPassword_FTC.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Password_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtCardName_FTC.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtCardSecurity_FTC.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardCVV_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtProfileName_FTC.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var Profile in TaskManagement.User_Profiles)
            {
                if (Profile.profilename == txtProfileName_FTC.Text)
                {
                    var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Duplicate, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            profile.email = txtLoginID_FTC.Text;
            profile.password = txtPassword_FTC.Text;
            profile.cardNumber = txtCardNumber_FTC.Text;
            profile.cardName = txtCardName_FTC.Text;
            profile.cardCVV = txtCardSecurity_FTC.Text;
            profile.cardType = cmbProfileCardType_FTC.SelectedIndex.ToString();
            profile.cardMonth = cmbCardMonth_FTC.Text;           
            if (cmbCardMonth_FTC.Text == "")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Please Input the Card Month", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            profile.cardYear = cmbCardYear_FTC.Text;
            if (cmbCardYear_FTC.Text == "")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Please Input the Card Year", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            profile.profilename = txtProfileName_FTC.Text;
            profile.store = "FTC";
            TaskManagement.User_Profiles.Add(profile);
            Thread notifythread = new Thread(CreateProfileNotify);
            notifythread.Start();
            RefreshProfile(false, -1);
        }

        private void btnDeleteProfile_FTC_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProfileName_FTC.Text == "")
            {
                if (Status.LanguageSetting == false)
                {
                    CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return;
            }
            try
            {
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == cmbProfileName_FTC.SelectedItem.ToString())
                    {
                        TaskManagement.User_Profiles.Remove(temp_profile);
                        break;
                    }
                }
            }
            catch
            {

            }
            RefreshProfile(false, 0);
        }

        private void togSizeFTC_Click(object sender, RoutedEventArgs e)
        {
            if (togSizeFTC.IsChecked == true)
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSizeFTC.Text = EN_Constants.ManualSize;
                }
                else
                {
                    labSelectSizeFTC.Text = JP_Constants.ManualSize;
                }

                panAutoSizeFTC.Visibility = Visibility.Hidden;
                txtSizeFTC.Visibility = Visibility.Visible;
            }
            else
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSizeFTC.Text = EN_Constants.AutoSize;
                }
                else
                {
                    labSelectSizeFTC.Text = JP_Constants.AutoSize;
                }
                panAutoSizeFTC.Visibility = Visibility.Visible;
                txtSizeFTC.Visibility = Visibility.Hidden;
            }
        }

        private void cmbProfileName_FTC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string profilename = cmbProfileName_FTC.SelectedItem.ToString();
                foreach (var profile in TaskManagement.User_Profiles)
                {
                    if (profile.profilename == profilename)
                    {
                        txtProfileName_FTC.Text = profile.profilename;
                        txtLoginID_FTC.Text = profile.email;
                        txtPassword_FTC.Text = profile.password;
                        txtCardName_FTC.Text = profile.cardName;
                        txtCardNumber_FTC.Text = profile.cardNumber;
                        txtCardSecurity_FTC.Text = profile.cardCVV;
                        cmbCardMonth_FTC.Text = profile.cardMonth;
                        cmbCardYear_FTC.Text = profile.cardYear;
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnConfirmProductFTC_Click(object sender, RoutedEventArgs e)
        {
            if (txtProuductSKU_FTC.Text == "")
            {
                if (Status.LanguageSetting == false)
                {
                    var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ERROR_INVALID_SITE_GOODNO, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var messageBoxResult = CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ERROR_INVALID_SITE_GOODNO, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return;
            }
            cmbSizeFTC.Items.Clear();
            strProductSKU_FTC = txtProuductSKU_FTC.Text;
            thread_getinfo_FTC = new Thread(new ThreadStart(ProcessGetInfoFTC));
            thread_getinfo_FTC.IsBackground = true;
            thread_getinfo_FTC.Start();
        }
        private void ProcessGetInfoFTC()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            string product_url = "";
            string response = "";
            try
            {
                FTCBotLogic ftcBotLogic = new FTCBotLogic();
                if (strProductSKU_FTC.Contains("http"))
                {
                    if (!strProductSKU_FTC.Contains("?"))
                    {
                        Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Waring, "Invalid Product URL", MessageBoxButton.OK, MessageBoxImage.Warning)));
                        return;
                    }
                    product_url = strProductSKU_FTC;
                    strProductURL_FTC = "?" + product_url.Split('?')[1];
                }
                else
                {
                    string search_url = Constant.g_strProductSearch_FTC + strProductSKU_FTC;
                    response = ftcBotLogic.Get_Info(search_url);                    
                    htmlDocument.LoadHtml(response);
                    List<HtmlNode> product_links = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "a" && x.Attributes["href"] != null && x.Attributes["href"].Value.Contains("pid=")).ToList();
                    string pid_value = product_links[0].Attributes["href"].Value;
                    product_url = Constant.g_strBase_FTC + pid_value;
                    strProductURL_FTC = pid_value;
                }
                response = ftcBotLogic.Get_Info(product_url);
                htmlDocument.LoadHtml(response);                
                List<HtmlNode> select_nodes = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "select" && x.Attributes["name"]!=null && x.Attributes["name"].Value.Contains("option")).ToList();
                var size_nodes = select_nodes[1].Descendants().Where(x => x.Name == "option").ToList();
                
                foreach (var size_node in size_nodes)
                {
                    Dispatcher.Invoke(new Action(() => cmbSizeFTC.Items.Add(size_node.InnerText)));
                }
                
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Success, EN_Constants.GETINFO, MessageBoxButton.OK, MessageBoxImage.Warning)));                
                try
                {
                    List<HtmlNode> img_nodes = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "img" && x.Attributes["class"] != null && x.Attributes["class"].Value == "main_img").ToList();
                    string img_tag = img_nodes[0].Attributes["src"].Value;
                    strProductImage_FTC = img_tag;
                    imgProductFTC.Dispatcher.Invoke(new Action(() => imgProduct.Source = new BitmapImage(new Uri(img_tag))));
                }
                catch
                {

                }
                boolProductFound_FTC = true;
            }
            catch
            {
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ERROR_PRODUCT_NOT_EXIST + "\n" + "Please Add Product manually", MessageBoxButton.OK, MessageBoxImage.Warning)));
            }
        }

        private void btnTokenGenerater_FTC_Click(object sender, RoutedEventArgs e)
        {
            string profilename = cmbTaskProfileFTC.Text;
            if(profilename=="")
            {
                CustomMessageBox.Show("Warning", "Please select the Profile", MessageBoxButton.OK);
                return;
            }

            Profile selected_profile = new Profile();
            foreach(var profile in TaskManagement.User_Profiles)
            {
                if(profile.profilename == profilename)
                {
                    selected_profile = profile;
                    break;
                }
            }
            FTCTokenGenerater fTCTokenGenerater = new FTCTokenGenerater(selected_profile,this);
            fTCTokenGenerater.Show();
        }
        public void SetTokenFTC(string token)
        {
            txtCreditTokenFTC.Text = token;
        }


        private void btnHarvestCaptcha_Click(object sender, RoutedEventArgs e)
        {
            AddCaptcha addCaptcha = new AddCaptcha(this);
            addCaptcha.Show();
        }


        //////////////////////////////////////
        ///Token Timer
        ///

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {            
            try
            {
                Refresh_tokens();
                TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                int curtime = (int)t.TotalSeconds;
                foreach (var capkey in TaskManagement.captcha_token_Mortar.Keys)
                {
                    int startcaptime = TaskManagement.captcha_token_Mortar[capkey];
                    if ((curtime - startcaptime) > 120)
                    {
                        TaskManagement.captcha_token_Mortar.Remove(capkey);
                    }
                }
            }
            catch
            {

            }
        }

        private void Refresh_tokens()
        {
            try
            {
                int token_count_Mortar = TaskManagement.captcha_token_Mortar.Keys.Count;
                txttokenCount.Dispatcher.Invoke(new Action(() => txttokenCount.Text = token_count_Mortar.ToString()));
            }
            catch
            {

            }
        }



        /// <summary>
        /// ///////////-----------------ARKTZ-----------------------
        /// </summary>        

        private void togSizeARK_Click(object sender, RoutedEventArgs e)
        {
            if (togSizeARK.IsChecked == true)
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSizeARK.Text = EN_Constants.ManualSize;
                }
                else
                {
                    labSelectSizeARK.Text = JP_Constants.ManualSize;
                }

                panAutoSizeARK.Visibility = Visibility.Hidden;
                txtSizeARK.Visibility = Visibility.Visible;
            }
            else
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSizeARK.Text = EN_Constants.AutoSize;
                }
                else
                {
                    labSelectSizeARK.Text = JP_Constants.AutoSize;
                }
                panAutoSizeARK.Visibility = Visibility.Visible;
                txtSizeARK.Visibility = Visibility.Hidden;
            }
        }

        private void btnConfirmProductARK_Click(object sender, RoutedEventArgs e)
        {
            if (txtProuduct_ARK.Text == "")
            {
                if (Status.LanguageSetting == false)
                {
                    var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ERROR_INVALID_SITE_GOODNO, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var messageBoxResult = CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ERROR_INVALID_SITE_GOODNO, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return;
            }
            cmbSizeARK.Items.Clear();
            strProductSKU_ARK = txtProuduct_ARK.Text;         
            thread_getinfo_ARK = new Thread(new ThreadStart(ProcessGetInfoARK));
            thread_getinfo_ARK.IsBackground = true;
            thread_getinfo_ARK.Start();
        }
        private void ProcessGetInfoARK()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            string product_url = "";
            string response = "";
            try
            {
                ARKBotLogic arkBotLogic = new ARKBotLogic();
                if (strProductSKU_ARK.Contains("http"))
                {
                    if (!strProductSKU_ARK.Contains("?"))
                    {
                        Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Waring, "Invalid Product URL", MessageBoxButton.OK, MessageBoxImage.Warning)));
                        return;
                    }
                    product_url = strProductSKU_ARK;
                    strProductURL_ARK = "?" + product_url.Split('?')[1];
                }
                else
                {
                    string search_url = Constant.g_strProductSearch_ARK + strProductSKU_ARK;
                    response = arkBotLogic.Get_Info(search_url);
                    htmlDocument.LoadHtml(response);
                    List<HtmlNode> product_links = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "a" && x.Attributes["href"] != null && x.Attributes["href"].Value.Contains("pid=")).ToList();
                    string pid_value = product_links[0].Attributes["href"].Value;
                    product_url = Constant.g_strBase_ARK + pid_value;
                    strProductURL_ARK = pid_value;
                }              
                response = arkBotLogic.Get_Info(product_url);
                htmlDocument.LoadHtml(response);  
                List<HtmlNode> select_nodes = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "select" && x.Attributes["name"] != null && x.Attributes["name"].Value.Contains("option1")).ToList();
                var size_nodes = select_nodes[0].Descendants().Where(x => x.Name == "option").ToList();
                foreach (var size_node in size_nodes)
                {
                    Dispatcher.Invoke(new Action(() => cmbSizeARK.Items.Add(size_node.InnerText)));
                }
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Success, EN_Constants.GETINFO, MessageBoxButton.OK, MessageBoxImage.Warning)));
                try
                {
                    List<HtmlNode> img_nodes = htmlDocument.DocumentNode.Descendants().Where(x => x.Name == "img" && x.Attributes["class"] != null && x.Attributes["class"].Value == "mainImage").ToList();
                    string img_tag = img_nodes[0].Attributes["src"].Value;
                    strProductImage_ARK = img_tag;
                    imgProductARK.Dispatcher.Invoke(new Action(() => imgProductARK.Source = new BitmapImage(new Uri(img_tag))));
                }
                catch
                {

                }
                boolProductFound_ARK = true;
            }
            catch
            {
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ERROR_PRODUCT_NOT_EXIST + "\n" + "Please Add Product manually", MessageBoxButton.OK, MessageBoxImage.Warning)));
            }
        }

        private void btnTokenGenerater_ARK_Click(object sender, RoutedEventArgs e)
        {
            string profilename = cmbTaskProfileARK.Text;
            if (profilename == "")
            {
                CustomMessageBox.Show("Warning", "Please select the Profile", MessageBoxButton.OK);
                return;
            }

            Profile selected_profile = new Profile();
            foreach (var profile in TaskManagement.User_Profiles)
            {
                if (profile.profilename == profilename)
                {
                    selected_profile = profile;
                    break;
                }
            }
            ARKTokenGenerator arkTokenGenerater = new ARKTokenGenerator(selected_profile, this);
            arkTokenGenerater.Show();
        }

        private void btnCreateTaskARK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProuduct_ARK.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
                if ((togSizeARK.IsChecked == false && cmbSizeARK.Text == "") || (togSizeARK.IsChecked == true && txtSizeARK.Text == ""))
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Size_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.Size_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
                if (cmbTaskProfileARK.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return;
                }
                if (cmbTaskPaymentARK.SelectedIndex == 0 && txtCreditTokenARK.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardToken_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.CardToken_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }

                Random random = new Random();
                BotTask botTask = new BotTask();
                botTask.id = random.Next(100000, 999999).ToString();
                if (boolProductFound_ARK == true)
                {
                    botTask.product_url =txtProuduct_ARK.Text + ";" + strProductURL_ARK;
                }
                else
                {
                    botTask.product_url = txtProuduct_ARK.Text;
                }
                botTask.store = cmbStore.Text;
                if (togSizeARK.IsChecked == false)
                {
                    botTask.size = cmbSizeARK.Text;
                }
                else
                {
                    botTask.size = txtSizeARK.Text;
                }
                botTask.color_option = strProductImage_ARK;
                botTask.wish = txtWishARK.Text;
                botTask.token = txtCreditTokenARK.Text;
                botTask.profilename = cmbTaskProfileARK.Text;
                botTask.cardtype = cmbTaskPaymentARK.SelectedIndex;
                botTask.activeProxy = cmbTaskProxyARK.Text;
                TaskManagement.User_Tasks.Add(botTask);
                Refresh_TaskList(false, false);
                Thread notify_thread = new Thread(CreateTaskNotify);
                notify_thread.Start();
            }
            catch
            {

            }
        }

        private void btnCancelTaskARK_Click(object sender, RoutedEventArgs e)
        {
            string profilename = cmbTaskProfileARK.Text;
            if (profilename == "")
            {
                CustomMessageBox.Show("Warning", "Please select the Profile", MessageBoxButton.OK);
                return;
            }

            Profile selected_profile = new Profile();
            foreach (var profile in TaskManagement.User_Profiles)
            {
                if (profile.profilename == profilename)
                {
                    selected_profile = profile;
                    break;
                }
            }
            FTCTokenGenerater fTCTokenGenerater = new FTCTokenGenerater(selected_profile, this);
            fTCTokenGenerater.Show();
        }

        private void cmbProfileName_ARK_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string profilename = cmbProfileName_ARK.SelectedItem.ToString();
                foreach (var profile in TaskManagement.User_Profiles)
                {
                    if (profile.profilename == profilename)
                    {
                        txtProfileName_ARK.Text = profile.profilename;
                        txtLoginID_ARK.Text = profile.email;
                        txtPassword_ARK.Text = profile.password;
                        txtCardName_ARK.Text = profile.cardName;
                        txtCardNumber_ARK.Text = profile.cardNumber;
                        txtCardSecurity_ARK.Text = profile.cardCVV;
                        cmbCardMonth_ARK.Text = profile.cardMonth;
                        cmbCardYear_ARK.Text = profile.cardYear;
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnImportProfile_ARK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;
                    Dictionary<string, string> profile = new Dictionary<string, string>();
                    List<Dictionary<string, string>> lstProfiles = new List<Dictionary<string, string>>();
                    string str_profiles = File.ReadAllText(filename);
                    JArray jarray_profiles = JArray.Parse(str_profiles);
                    if (jarray_profiles.Count == 0)
                    {
                        return;
                    }
                    lstProfiles = new List<Dictionary<string, string>>();
                    foreach (var json_profile in jarray_profiles)
                    {
                        Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_profile.ToString());
                        lstProfiles.Add(temp);
                    }
                    foreach (var dic_temp_profile in lstProfiles)
                    {
                        Profile temp_profile = new Profile();
                        temp_profile.profilename = dic_temp_profile["profilename"];
                        temp_profile.email = dic_temp_profile["email"];
                        temp_profile.password = dic_temp_profile["password"];
                        temp_profile.cardNumber = dic_temp_profile["cardnumber"];
                        temp_profile.cardCVV = dic_temp_profile["securitycode"];
                        temp_profile.cardYear = dic_temp_profile["expire_year"];
                        if (dic_temp_profile["expire_month"].Length == 1)
                        {
                            temp_profile.cardMonth = "0" + dic_temp_profile["expire_month"];
                        }
                        else
                        {
                            temp_profile.cardMonth = dic_temp_profile["expire_month"];
                        }
                        temp_profile.cardType = "0";
                        temp_profile.cardName = dic_temp_profile["cardname"];
                        temp_profile.store = "ARK";
                        TaskManagement.User_Profiles.Add(temp_profile);
                    }
                    RefreshProfile(false, 0);
                }
            }
            catch (Exception ee)
            {
                CustomMessageBox.Show(ee.Message);
            }
        }

        private void btnCreateProfile_ARK_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            if (txtLoginID_ARK.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Email_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }          
            if (txtPassword_ARK.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Password_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtCardName_ARK.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtCardSecurity_ARK.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.CardCVV_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtProfileName_ARK.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var Profile in TaskManagement.User_Profiles)
            {
                if (Profile.profilename == txtProfileName_ARK.Text)
                {
                    var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Duplicate, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            profile.email = txtLoginID_ARK.Text;
            profile.password = txtPassword_ARK.Text;
            profile.cardNumber = txtCardNumber_ARK.Text;
            profile.cardName = txtCardName_ARK.Text;
            profile.cardCVV = txtCardSecurity_ARK.Text;
            profile.cardType = cmbProfileCardType_ARK.SelectedIndex.ToString();
            profile.cardMonth = cmbCardMonth_ARK.Text;
            if (cmbCardMonth_ARK.Text == "")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Please Input the Card Month", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            profile.cardYear = cmbCardYear_ARK.Text;
            if (cmbCardYear_ARK.Text == "")
            {
                CustomMessageBox.Show(EN_Constants.Waring, "Please Input the Card Year", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            profile.profilename = txtProfileName_ARK.Text;
            profile.store = "ARK";
            TaskManagement.User_Profiles.Add(profile);
            Thread notifythread = new Thread(CreateProfileNotify);
            notifythread.Start();
            RefreshProfile(false, -1);
        }

        private void btnDeleteProfile_ARK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProfileName_ARK.Text == "")
            {
                if (Status.LanguageSetting == false)
                {
                    CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return;
            }
            try
            {
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == cmbProfileName_ARK.SelectedItem.ToString())
                    {
                        TaskManagement.User_Profiles.Remove(temp_profile);
                        break;
                    }
                }
            }
            catch
            {

            }
            RefreshProfile(false, 0);
        }
        public void SetTokenARK(string token)
        {
            txtCreditTokenARK.Text = token;
        }

        /// <summary>
        /// //////////// -----------Instant SB-------------------
        /// </summary>        


        private void togSizeISB_Click(object sender, RoutedEventArgs e)
        {
            if (togSizeISB.IsChecked == true)
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSizeISB.Text = EN_Constants.ManualSize;
                }
                else
                {
                    labSelectSizeISB.Text = JP_Constants.ManualSize;
                }

                panAutoSizeISB.Visibility = Visibility.Hidden;
                txtSizeISB.Visibility = Visibility.Visible;
            }
            else
            {
                if (Status.LanguageSetting == false)
                {
                    labSelectSizeISB.Text = EN_Constants.AutoSize;
                }
                else
                {
                    labSelectSizeISB.Text = JP_Constants.AutoSize;
                }
                panAutoSizeISB.Visibility = Visibility.Visible;
                txtSizeISB.Visibility = Visibility.Hidden;
            }
        }

        private void btnConfirmProductISB_Click(object sender, RoutedEventArgs e)
        {
            strProductURL_ISB = txtProduct_ISB.Text;
            Thread thr = new Thread(FindProduct_ISB);
            thr.Start();

        }

        private void FindProduct_ISB()
        {
            ISBBotLogic botLogic = new ISBBotLogic();  
            var response = botLogic.Get_Info(strProductURL_ISB);
            DumpFile(response, "isb.htm");
            MessageBox.Show("product found");
        }

        private void btnCreateTaskISB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelTaskISB_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// ///// -------------Zingaro------------------
        /// </summary>
        /// 

        private void btnCreateTaskZingaro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProuduct_Zingaro.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProductURL_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }               
                if (cmbTaskProfileZingaro.Text == "")
                {
                    if (Status.LanguageSetting == false)
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        CustomMessageBox.Show(EN_Constants.Waring, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return;
                }                

                Random random = new Random();
                BotTask botTask = new BotTask();
                botTask.id = random.Next(100000, 999999).ToString();               
                botTask.product_url = txtProuduct_Zingaro.Text;
                botTask.store = cmbStore.Text;
                botTask.size = txtSizeZingaro.Text;
                botTask.wish = txtWishZingaro.Text;
                botTask.token = "";
                botTask.profilename = cmbTaskProfileZingaro.Text;
                botTask.cardtype = cmbTaskPaymentZingaro.SelectedIndex;
                botTask.activeProxy = cmbTaskProxyZingaro.Text;
                TaskManagement.User_Tasks.Add(botTask);
                Refresh_TaskList(false, false);
                Thread notify_thread = new Thread(CreateTaskNotify);
                notify_thread.Start();
            }
            catch
            {

            }
        }


        private void btnCancelTaskZingaro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTokenGenerater_Zingaro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnImportProfile_Zingaro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;
                    Dictionary<string, string> profile = new Dictionary<string, string>();
                    List<Dictionary<string, string>> lstProfiles = new List<Dictionary<string, string>>();
                    string str_profiles = File.ReadAllText(filename);
                    JArray jarray_profiles = JArray.Parse(str_profiles);
                    if (jarray_profiles.Count == 0)
                    {
                        return;
                    }
                    lstProfiles = new List<Dictionary<string, string>>();
                    foreach (var json_profile in jarray_profiles)
                    {
                        Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_profile.ToString());
                        lstProfiles.Add(temp);
                    }
                    foreach (var dic_temp_profile in lstProfiles)
                    {
                        Profile temp_profile = new Profile();
                        temp_profile.profilename = dic_temp_profile["profilename"];
                        temp_profile.email = dic_temp_profile["email"];
                        temp_profile.password = dic_temp_profile["password"];
                        temp_profile.cardNumber = dic_temp_profile["cardnumber"];
                        temp_profile.cardCVV = dic_temp_profile["securitycode"];
                        temp_profile.cardYear = dic_temp_profile["expire_year"];
                        if (dic_temp_profile["expire_month"].Length == 1)
                        {
                            temp_profile.cardMonth = "0" + dic_temp_profile["expire_month"];
                        }
                        else
                        {
                            temp_profile.cardMonth = dic_temp_profile["expire_month"];
                        }
                        temp_profile.cardType = "0";
                        temp_profile.cardName = dic_temp_profile["cardname"];
                        temp_profile.store = "Zingaro";
                        TaskManagement.User_Profiles.Add(temp_profile);
                    }
                    RefreshProfile(false, 0);
                }
            }
            catch (Exception ee)
            {
                CustomMessageBox.Show(ee.Message);
            }
        }

        private void cmbProfileName_Zingaro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string profilename = cmbProfileName_Zingaro.SelectedItem.ToString();
                foreach (var profile in TaskManagement.User_Profiles)
                {
                    if (profile.profilename == profilename)
                    {
                        txtProfileName_Zingaro.Text = profile.profilename;
                        txtLoginID_Zingaro.Text = profile.email;
                        txtPassword_Zingaro.Text = profile.password;
                       break;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnCreateProfile_Zingaro_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            if (txtLoginID_Zingaro.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Email_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtPassword_Zingaro.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.Password_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
                  
            if (txtProfileName_Zingaro.Text == "")
            {
                var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var Profile in TaskManagement.User_Profiles)
            {
                if (Profile.profilename == txtProfileName_Zingaro.Text)
                {
                    var messageBoxResult = CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Duplicate, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            profile.email = txtLoginID_Zingaro.Text;
            profile.password = txtPassword_Zingaro.Text;
            profile.cardNumber = "";
            profile.cardName = "";
            profile.cardCVV = "";
            profile.cardType = "";
            profile.cardMonth = "";
            profile.cardYear = "";            
            profile.profilename = txtProfileName_Zingaro.Text;
            profile.store = "Zingaro";
            TaskManagement.User_Profiles.Add(profile);
            Thread notifythread = new Thread(CreateProfileNotify);
            notifythread.Start();
            RefreshProfile(false, -1);
        }

        private void btnDeleteProfile_Zingaro_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProfileName_Zingaro.Text == "")
            {
                if (Status.LanguageSetting == false)
                {
                    CustomMessageBox.Show(EN_Constants.Waring, EN_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    CustomMessageBox.Show(JP_Constants.Warning, JP_Constants.ProfileName_Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return;
            }
            try
            {
                foreach (var temp_profile in TaskManagement.User_Profiles)
                {
                    if (temp_profile.profilename == cmbProfileName_Zingaro.SelectedItem.ToString())
                    {
                        TaskManagement.User_Profiles.Remove(temp_profile);
                        break;
                    }
                }
            }
            catch
            {

            }
            RefreshProfile(false, 0);
        }

        private void cmbProperty_MCT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i_selectedItem = cmbProperty_MCT.SelectedIndex;
            strProductSKU_MCT = strProductSKUs_MCT[i_selectedItem];
            strProductVariant_MCT = strProductVariants_MCT[i_selectedItem];
        }

        private void togVariantMCT_Click(object sender, RoutedEventArgs e)
        {
            if(togVariantMCT.IsChecked==true)
            {
                panAutoProperty_MCT.Visibility = Visibility.Hidden;
                panManualProperty_MCT.Visibility = Visibility.Visible;
            }
            else
            {
                panAutoProperty_MCT.Visibility = Visibility.Visible;
                panManualProperty_MCT.Visibility = Visibility.Hidden;
            }
        }

        private void btnGenerateToken_Mortar_Click(object sender, RoutedEventArgs e)
        {
            if(cmbTaskProfileMortar.SelectedIndex==-1 || cmbTaskProfileMortar.Text=="")
            {
                CustomMessageBox.Show("Please select the Profile");
                return;
            }
            string profilename = cmbTaskProfileMortar.Text;
            foreach(var profile in TaskManagement.User_Profiles)
            {
                if(profile.profilename==profilename && profile.store== "MORTAR")
                {
                    selectedMortarProfile = profile;
                    break;
                }
            }
            if(selectedMortarProfile.cardNumber=="")
            {
                CustomMessageBox.Show("Invalid Profile");
                return;
            }
            CustomMessageBox.Show("Please wait for 5~10 seconds");
            Thread genToken = new Thread(GenearteToken);
            genToken.Start();
        }

        private void GenearteToken()
        {
            try
            {
                string token1 = global::AIOBOT.Properties.Resources.Token1;
                string token2 = global::AIOBOT.Properties.Resources.Token2;
                string gentoken1 = token1.Replace("111111", selectedMortarProfile.cardNumber).Replace("222222", selectedMortarProfile.cardYear).Replace("333333", selectedMortarProfile.cardMonth).Replace("444444", selectedMortarProfile.cardCVV).Replace("555555", selectedMortarProfile.cardName);
                string gentoken2 = token2.Replace("111111", selectedMortarProfile.cardNumber).Replace("222222", selectedMortarProfile.cardYear).Replace("333333", selectedMortarProfile.cardMonth).Replace("444444", selectedMortarProfile.cardCVV).Replace("555555", selectedMortarProfile.cardName);
                DumpFile(gentoken1, "Token1.htm");
                DumpFile(gentoken2, "Token2.htm");
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                var chromeoptions = new ChromeOptions();
                chromeoptions.AddArgument("headless");
                chromeoptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");
                var TokenBot = new ChromeDriver(driverService, chromeoptions);
                string filename1 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Token1.htm");
                string filename2 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Token2.htm");
                TokenBot.Url = "file://" + filename1;
                TokenBot.FindElementByTagName("button").Click();
                string gmotoken = TokenBot.FindElementById("gmo_value").GetAttribute("value");
                string stripetoken = TokenBot.FindElementById("stripe_value").GetAttribute("value");
                while (true)
                {
                    gmotoken = TokenBot.FindElementById("gmo_value").GetAttribute("value");
                    stripetoken = TokenBot.FindElementById("stripe_value").GetAttribute("value");
                    if (gmotoken != "" && stripetoken != "")
                    {
                        break;
                    }
                }
                TokenBot.Url = "file://" + filename2;
                TokenBot.FindElementByTagName("button").Click();
                Thread.Sleep(2000);
                string ipstoken = TokenBot.FindElementById("result_token").GetAttribute("value");
                if (gmotoken != "" && stripetoken != "" && ipstoken != "")
                {
                    txtToken1.Dispatcher.Invoke(new Action(() => txtToken1.Text = gmotoken + ";" + stripetoken + ";" + ipstoken));
                    Dispatcher.Invoke(new Action(() => CustomMessageBox.Show("Generated Token!")));
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => CustomMessageBox.Show("Failed. Retry Again!")));
                }
                try
                {
                    TokenBot.Close();
                    TokenBot.Quit();
                }
                catch
                {

                }
                try
                {
                    File.Delete(filename1);
                }
                catch
                {

                }
                try
                {
                    File.Delete(filename2);
                }
                catch
                {

                }
            }
            catch
            {
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show("Failed. Retry Again!")));
            }
        }
    }
}
