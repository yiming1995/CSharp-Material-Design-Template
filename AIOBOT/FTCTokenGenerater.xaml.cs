using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for FTCTokenGenerater.xaml
    /// </summary>
    public partial class FTCTokenGenerater : Window
    {
        Profile genProfile;
        string producturl = "";
        MainWindow parentWindow;
        public FTCTokenGenerater(Profile profile, MainWindow main)
        {
            InitializeComponent();
            genProfile = profile;
            parentWindow = main;
        }

        private void btntokenGenerate_Click(object sender, RoutedEventArgs e)
        {
            producturl = txttestingurl.Text;
            Thread thr = new Thread(GenToken);
            thr.Start();
        }
        private void GenToken()
        {
            iconWaiting.Dispatcher.Invoke(new Action(() => iconWaiting.Visibility = Visibility.Visible));
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var chromeoptions = new ChromeOptions();
            chromeoptions.AddArgument("headless");            
            chromeoptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");
            var TokenBot = new ChromeDriver(driverService, chromeoptions);
            try
            {                           
                TokenBot.Url = producturl;
                var product_info_tag = TokenBot.FindElementById("productInformation");
                product_info_tag.FindElement(By.TagName("button")).Click();
                Thread.Sleep(7000);

                IJavaScriptExecutor js = (IJavaScriptExecutor)TokenBot;
                js.ExecuteScript("document.getElementById(\"payment_method_787670\").click()");
                Thread.Sleep(3000);
                js.ExecuteScript("document.getElementById(\"create-token-launch\").click()");

                var iframe = TokenBot.FindElementById("webcollect-token-iframe");
                TokenBot.SwitchTo().Frame(iframe);

                TokenBot.FindElementById("card-no").SendKeys(genProfile.cardNumber);
                TokenBot.FindElementById("card-owner").SendKeys(genProfile.cardName);
                TokenBot.FindElementById("exp-month").SendKeys(genProfile.cardMonth);
                TokenBot.FindElementById("exp-year").SendKeys(genProfile.cardYear.Substring(2,2));
                TokenBot.FindElementById("security-code").SendKeys(genProfile.cardCVV);
                TokenBot.FindElementById("send-request").Click();
                Thread.Sleep(3000);
                TokenBot.SwitchTo().ParentFrame();
                txttoken.Dispatcher.Invoke(new Action(() =>txttoken.Text=TokenBot.FindElementById("webcollect-token").GetAttribute("value")));
                try
                {
                   TokenBot.Close();
                    TokenBot.Quit();
                }
                catch
                {

                }
                iconWaiting.Dispatcher.Invoke(new Action(() => iconWaiting.Visibility = Visibility.Hidden));
            }
            catch
            {
                iconWaiting.Dispatcher.Invoke(new Action(() => iconWaiting.Visibility = Visibility.Hidden));
                Dispatcher.Invoke(new Action(() => CustomMessageBox.Show("Error", "Please input other Product url", MessageBoxButton.OK)));
                try
                {
                   TokenBot.Close();
                   TokenBot.Quit();
                }
                catch
                {

                }
            }
        }

        private void btntokenCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btntokenCopy_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.SetTokenFTC(txttoken.Text);
            this.Close();
        }
    }
}
