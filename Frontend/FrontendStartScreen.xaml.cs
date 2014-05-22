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
    /// Interaktionslogik für FrontendStartScreen.xaml
    /// </summary>
    public partial class FrontendStartScreen : Window
    {
        
       
        public FrontendStartScreen()
        {
            InitializeComponent();
        }

        private void ButtonConfig_Click(object sender, RoutedEventArgs e)
        {                  
            Config configWindow = new Config();
           
                //Show Dialog is used if you want to lock the parent window
            try
            {
                configWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fenster konnte nicht geöffnet werden", "Fehler" );
            }
            
          
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GraphConfig graphConfig = new GraphConfig();

            graphConfig.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Graphic graphic = new Graphic();

            graphic.ShowDialog();
        }
    }
}
