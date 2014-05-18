using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DataObjects;

namespace Utility.DisplayElements
{
    public class GraphFactory
    {
        private Chart chartWindow;

        private String colorScheme;
        private String style;
        private List<List<PLCData>> displayedData;

        public GraphFactory()
        {
            this.ResetFactory();
        }

        public void ResetFactory()
        {
            //this.createdChart = this.CreateDefaultChart();

            this.colorScheme = "default";
            this.style = "default";
            this.displayedData = new List<List<PLCData>>();
        }

        public void Create(ref Chart chartWindow) 
        {
            this.chartWindow = chartWindow;
            this.DoColoring();
            this.DoStyling();
            this.CreateData();
            CreateDefaultChart();                       
        }

        private void CreateDefaultChart()
        {
            
            ChartArea chartArea = new ChartArea();
            Legend chartLegend = new Legend();
            Series series1 = new Series();
            Series series2 = new Series();
            DataPoint point1 = new DataPoint();
            DataPoint point2 = new DataPoint();
            DataPoint point3 = new DataPoint();
            DataPoint point4 = new DataPoint();
            DataPoint point5 = new DataPoint();
            DataPoint point6 = new DataPoint();
            DataPoint point7 = new DataPoint();
            DataPoint point8 = new DataPoint();
            double[] points = new double[]{ 10, 20, 30, 15, 12, 14, 55, 19 };

            
            chartLegend.BackColor = System.Drawing.Color.AliceBlue;
            chartLegend.Name = "Legende";

            point1.XValue = 100;
            point1.SetValueY(20);
            point2.XValue = (point1.XValue + 100);
            point2.SetValueY(25);
            point3.XValue = 50;
            point3.SetValueY(45);
            point4.XValue = (point3.XValue + 50);
            point4.SetValueY(12);
            point5.XValue = (point4.XValue + 50);
            point5.SetValueY(20);
            point6.XValue = (point5.XValue + 50);
            point6.SetValueY(44);
            point7.XValue = (point6.XValue + 50);
            point7.SetValueY(66);
            point8.XValue = (point7.XValue + 30);
            point8.SetValueY(33);
            //point1.YValues = points;
            //point1.YValues
            point1.AxisLabel = "tp1";
            series1.Name = "hugo";
            series1.ChartType = SeriesChartType.Spline;
            series1.Points.AddY(0);
            series1.Points.Add(point1);
            series1.Points.Add(point2);

            series2.Name = "valueKessel????????!123very long String";
            series2.ChartType = SeriesChartType.Spline;
            series2.Points.Add(point3);
            series2.Points.Add(point4);
            series2.Points.Add(point5);
            series2.Points.Add(point6);
            series2.Points.Add(point7);
            series2.Points.Add(point8);
            //TODO use the private function FillSeriesWithDataPoints(List<List<PLCData>> displayedData)
            //chartArea.


            //  *************************
            chartWindow.Series.Add(series2);
            chartWindow.Series.Add(series1);
            chartWindow.Legends.Add(chartLegend);
            chartWindow.Size =new System.Drawing.Size(8, 8);
            chartWindow.BackColor = System.Drawing.Color.BurlyWood;
            chartWindow.Location = new System.Drawing.Point(10, 10);

          
            chartWindow.Name = "ChartArea1";
            chartWindow.ChartAreas.Add(chartArea); //Adds the created Chart
            chartWindow.Visible = true;

            //newChart.Size = new System.Drawing.Size(8, 8);
            //newChart.TabIndex = 0;
            //newChart.Visible = true;
            //newChart.BackColor = System.Drawing.Color.Red;
            //newChart.Location = new System.Drawing.Point(93, 69);
           // chartWindow

            //Legend legend1 = new Legend();
            //legend1.Name = "Legend1";
            //newChart.Legends.Add(legend1);

            //Series series1 = new Series();

           // return newChart;
        }

       

        #region sets

        public void SetColorSheme()
        {
            
        }

        public void SetStyle()
        {

        }

        public void SetData()
        {

        }

        #endregion

       
       
        private void CreateData()
        {

        }

        #region ***********Do Coloring****************  
 
        private void DoColoring()
        {
            switch (colorScheme)
            {
                case "green":
                    this.DoColoringGreen();
                    break;
                case "blue":
                    this.DoColoringBlue();
                    break;
                case "default":
                    this.DoColoringDefault();
                    break;
            }

        }


        private void DoColoringGreen()
        { 
            
        }

        private void DoColoringBlue()
        {
        
        }

        private void DoColoringDefault()
        { 
        
        }
        #endregion

        #region ************Do Styling****************

        private void DoStyling()
        {
            switch (style)
            {
                case "Lines":
                    this.DoStylingLines();
                    break;
                case "Bar":
                    this.DoStylingBar();
                    break;
                case "default":
                    this.DoStylingDefault();
                    break;
            }

        }

        private void DoStylingLines()
        { 
        
        }

        private void DoStylingBar()
        { 
        
        }

        private void DoStylingDefault()
        {

        }

        private Series FillSeriesWithDataPoints(List<List<PLCData>> displayedData)
        {
            Series series = new Series();

            return series;
        }

        #endregion
    }
}

  //          ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
  //          Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
  //          Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
  //          Chart chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
  //          DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 2D);
  //          DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 3D);

  //          chartArea1.Name = "ChartArea1";
  //          this.blub2.ChartAreas.Add(chartArea1);
  //          legend1.Name = "Legend1";
  //          this.blub2.Legends.Add(legend1);           
  //          chart1.Location = new System.Drawing.Point(93, 69);
  //          this.blub2.Name = "chart1";
  //          series1.ChartArea = "ChartArea1";
  //          series1.Legend = "Legend1";
  //          series1.Name = "Series1";
  //          series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
  //         // series1.Points.Add(dataPoint1);
  //         // series1.Points.Add(dataPoint3);
  //          this.blub2.Series.Add(series1);
  //          chart1.Size = new System.Drawing.Size(8, 8);
  //          this.blub2.TabIndex = 0;
  //          this.blub2.Text = "chart1";
  //          chart1.Visible = true;

  //          return chart1;
        