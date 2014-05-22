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
using System.Drawing;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        SolidColorBrush mySolidColorBrush = new SolidColorBrush();

       // Color color = new Color();

        private byte colorRed = 0;
        private byte colorGreen = 0;
        private byte colorBlue = 0;
        

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void sliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            colorRed = Convert.ToByte(sliderRed.Value);
            createColor();            
        }

        private void sliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            colorGreen = Convert.ToByte(sliderGreen.Value);
            createColor();
        }

        private void sliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            colorBlue = Convert.ToByte(sliderBlue.Value);
            createColor();
        }


        private void createColor()
        {

            mySolidColorBrush.Color = System.Windows.Media.Color.FromArgb(255, colorRed, colorGreen, colorBlue);

            rectangleColor.Fill = mySolidColorBrush;
        }

      
       
        
    }
}
