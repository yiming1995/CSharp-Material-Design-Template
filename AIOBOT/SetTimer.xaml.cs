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
    /// Interaction logic for SetTimer.xaml
    /// </summary>
    public partial class SetTimer : UserControl
    {
        int cur_hour, cur_min, cur_sec = 0;
        public SetTimer()
        {
            InitializeComponent();
        }
        private void minute_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && cur_min < 59)
            {
                cur_min++;
                if (cur_min < 10)
                {
                    minute.Text = "0" + cur_min.ToString();
                }
                else
                {
                    minute.Text = cur_min.ToString();
                }
            }
            else if (e.Delta < 0 && cur_min > 0)
            {
                cur_min--;
                if (cur_min < 10)
                {
                    minute.Text = "0" + cur_min.ToString();
                }
                else
                {
                    minute.Text = cur_min.ToString();
                }
            }
        }

        private void second_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (e.Delta > 0 && cur_sec < 59)
            {
                cur_sec++;
                if (cur_sec < 10)
                {
                    second.Text = "0" + cur_sec.ToString();
                }
                else
                {
                    second.Text = cur_sec.ToString();
                }
            }
            else if (e.Delta < 0 && cur_sec > 0)
            {
                cur_sec--;
                if (cur_sec < 10)
                {
                    second.Text = "0" + cur_sec.ToString();
                }
                else
                {
                    second.Text = cur_sec.ToString();
                }
            }
        }

        private void hour_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && cur_hour < 23)
            {
                cur_hour++;
                if (cur_hour < 10)
                {
                    hour.Text = "0" + cur_hour.ToString();
                }
                else
                {
                    hour.Text = cur_hour.ToString();
                }
            }
            else if (e.Delta < 0 && cur_hour > 0)
            {
                cur_hour--;
                if (cur_hour < 10)
                {
                    hour.Text = "0" + cur_hour.ToString();
                }
                else
                {
                    hour.Text = cur_hour.ToString();
                }
            }
        }
        public int get_hour()
        {
            return cur_hour;
        }

        private void hour_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                cur_hour = Convert.ToInt32(hour.Text);
            }
            catch
            {

            }
        }

        private void minute_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                cur_min = Convert.ToInt32(minute.Text);
            }
            catch
            {

            }
        }

        private void second_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                cur_sec = Convert.ToInt32(second.Text);
            }
            catch
            {

            }
        }

        public int get_minute()
        {
            return cur_min;
        }
        public int get_second()
        {
            return cur_sec;
        }
        public void set_Timer(int h, int m, int s)
        {
            cur_hour = h;
            cur_min = m;
            cur_sec = s;

            if (cur_sec < 10)
            {
                second.Text = "0" + cur_sec.ToString();
            }
            else
            {
                second.Text = cur_sec.ToString();
            }

            if (cur_min < 10)
            {
                minute.Text = "0" + cur_min.ToString();
            }
            else
            {
                minute.Text = cur_min.ToString();
            }
            if (cur_hour < 10)
            {
                hour.Text = "0" + cur_hour.ToString();
            }
            else
            {
                hour.Text = cur_hour.ToString();
            }
        }
    }
}
