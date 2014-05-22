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

        private void buttonTimeToMinus_Click(object sender, RoutedEventArgs e)
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


        #region valueChanges

        private void TextBoxTimeFromHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxTimeFromHour.Text = CheckIfTextFieldValueIsValidHourValueAndConvertIt(TextBoxTimeFromHour).ToString();
        }

        private void TextboxTimeFromMinute_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextboxTimeFromMinute.Text = CheckIfTextFieldValueIsValidMinuteValueAndConvertIt(TextboxTimeFromMinute).ToString();
        }

        private void TextBoxTimeToHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxTimeToHour.Text = CheckIfTextFieldValueIsValidHourValueAndConvertIt(TextBoxTimeToHour).ToString();
        }

        private void TextboxTimeToMinute_TextChanged(object sender, TextChangedEventArgs e)
        {
           TextboxTimeToMinute.Text= CheckIfTextFieldValueIsValidMinuteValueAndConvertIt(TextboxTimeToMinute).ToString();
        }

        #endregion

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

        private int CheckIfTextFieldValueIsValidHourValueAndConvertIt(TextBox textBoxHour)
        {
            int number ;
            
            bool result = int.TryParse(textBoxHour.Text, out number);

            if (!result || number > 23 || number < 0)
            {
                MessageBox.Show("Der eingetragene Wert ist kein gültiger Stundenwert\n Bitte eine Zahl zwischen 0-24 auswählen");
                return 0;
            }
             
            return number;
        }

        private int CheckIfTextFieldValueIsValidMinuteValueAndConvertIt(TextBox textBoxHour)
        {
            int number;

            bool result = int.TryParse(textBoxHour.Text, out number);

            if (!result || number > 59 || number < 0)
            {
                MessageBox.Show("Der eingetragene Wert ist kein gültiger Minutenwert\n Bitte eine Zahl zwischen 0-59 auswählen");
                return 0;
            }

            return number;
        }

        #endregion

       


    }
}
