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
        private Config configWindow;
       
        public FrontendStartScreen()
        {
            InitializeComponent();
        }

        private void ButtonConfig_Click(object sender, RoutedEventArgs e)
        {         

            if (configWindow == null )
            {
                //TODO Window singelton***********************
                configWindow = new Config();
            }
            else
            {
                MessageBox.Show("Das Config fenster ist bereits geöffnet", "INFO");
            }
            configWindow.Show();
            
        }
    }
}
