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

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für GraphConfig.xaml
    /// </summary>
    public partial class GraphConfig : Window
    {
        public GraphConfig()
        {
            InitializeComponent();
        }

        #region buttons

        private void buttonTimeFromPlus_Click(object sender, RoutedEventArgs e)
        {
            int minute;

            minute = Convert.ToInt32(TextboxTimeFromMinute.Text);
            minute++;
            if (minute > 59)
            {
                minute = 0;
                AddHour(TextBoxTimeFromHour);
            }
            
            TextboxTimeFromMinute.Text = minute.ToString();
        }

        private void buttonTimeFromMinus_Click(object sender, RoutedEventArgs e)
        {
            int minute;

            minute = Convert.ToInt32(TextboxTimeFromMinute.Text);
            minute--;
            if (minute < 0)
            {
                minute = 59;
                MinusHour(TextBoxTimeFromHour);
            }
            
            TextboxTimeFromMinute.Text = minute.ToString();

        }
      
        private void buttonTimeToPlus_Click(object sender, RoutedEventArgs e)
        {
            int minute;

            minute = Convert.ToInt32(TextboxTimeToMinute.Text);
            minute++;

            if (minute > 59)
            {
                minute = 0;
                AddHour(TextBoxTimeToHour);
            }
           
            TextboxTimeToMinute.Text = minute.ToString();
        }

        private void buttonTimetoMinus_Click(object sender, RoutedEventArgs e)
        {
            int minute;

            minute = Convert.ToInt32(TextboxTimeToMinute.Text);
            minute--;

            if (minute < 0)
            {
                minute = 59;
                MinusHour(TextBoxTimeToHour);
            }
            
            TextboxTimeToMinute.Text = minute.ToString();
        }

        private void buttonChooseColor_Click(object sender, RoutedEventArgs e)
        {
            ColorPicker colorPicker = new ColorPicker();

            colorPicker.ShowDialog();

        }

        #endregion

        #region private functions

        private void AddHour(TextBox textboxHour)
        {
            int hour;

            hour = Convert.ToInt32(textboxHour.Text);

            hour++;

            if (hour > 23)
            {
                hour = 0;
            }


            textboxHour.Text = hour.ToString();
        }

        private void MinusHour(TextBox textboxHour)
        {
            int hour;

            hour = Convert.ToInt32(textboxHour.Text);
            hour--;
            if (hour < 0)
            {
                hour = 23;
            }

            textboxHour.Text = hour.ToString();
        }

        #endregion

       


    }
}
