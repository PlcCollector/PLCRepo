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
using Utility.DisplayElements;
using System.Drawing;



namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Graphic.xaml
    /// </summary>
    public partial class Graphic : Window
    {
        private GraphFactory graphFactory = new GraphFactory();
        private Chart currentChart = new Chart();

        public Graphic()
        {
            InitializeComponent();
            this.InitGraph();
        }


        private void InitGraph()
        {
            this.chartWindow = graphFactory.Create();

            //ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            //this.chartWindow.ChartAreas.Add(chartArea1);
            //this.currentChart = graphFactory.GiveMeATestChart();

            //this.chartWindow = currentChart;

            //this.chartWindow.BackColor = System.Drawing.Color.Purple;
            //this.chartWindow.Visible = true;
           

            
            //Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            //Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            //Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            //DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 2D);
            //DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 3D);

            //chartArea1.Name = "ChartArea1";
            //this.chartWindow.ChartAreas.Add(chartArea1);
            //legend1.Name = "Legend1";
            //this.chartWindow.Legends.Add(legend1);
            //chart1.Location = new System.Drawing.Point(93, 69);

            //this.chartWindow.Name = "chart1";
            //series1.ChartArea = "ChartArea1";
            //series1.Legend = "Legend1";
            //series1.Name = "Series1";
            //series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            //// series1.Points.Add(dataPoint1);
            //// series1.Points.Add(dataPoint3);
            //this.chartWindow.Series.Add(series1);
            //chart1.Size = new System.Drawing.Size(8, 8);
            //this.chartWindow.TabIndex = 0;
            //this.chartWindow.Text = "chart1";

        }
    }
}
