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
    /// Interaction logic for AddCaptcha.xaml
    /// </summary>
    public partial class AddCaptcha : Window
    {
        MainWindow parent;
        public AddCaptcha(MainWindow main)
        {
            InitializeComponent();
            parent = main;
        }

        private void btnSaveCaptcha_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string captcha_txt = new TextRange(txtCaptcha.Document.ContentStart, txtCaptcha.Document.ContentEnd).Text;
                if (captcha_txt != "")
                {
                    var lines = captcha_txt.Split('\n').ToList();
                    foreach (string token in lines)
                    {
                        if (token.Contains("03A"))
                        {
                            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                            TaskManagement.captcha_token_Mortar.Add(token.Trim(), (int)t.TotalSeconds);
                        }
                    }
                }
            }
            catch
            {
                CustomMessageBox.Show("Error", "Can not add Captcha Token", MessageBoxButton.OK);
            }
            this.Close();
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
