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
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Graphic.xaml
    /// </summary>
    public partial class Graphic : Window
    {
        public Graphic()
        {
            InitializeComponent();
            this.InitGraph();
        }

        private System.Windows.Forms.DataVisualization.Charting.Chart InitGraph()
        {
            Chart chart1 =new Chart();


            return chart1;
        }
    }
}
