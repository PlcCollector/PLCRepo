using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using DataObjects;

namespace Utility.DisplayElements
{
    public class GraphFactory
    {
        private Chart createdChart;

        private String colorScheme;
        private String style;
        private List<List<PLCData>> displayedData;

        public GraphFactory()
        {
            this.ResetFactory();
        }

        public void ResetFactory()
        {
            this.createdChart = this.CreateDefaultChart();

            this.colorScheme = "default";
            this.style = "default";
            this.displayedData = new List<List<PLCData>>();
        }

        public Chart Create() 
        {
            this.DoColoring();
            this.DoStyling();
            this.CreateData();
            return this.createdChart;
        }

        private Chart CreateDefaultChart()
        {
            Chart newChart = new Chart();
            //newChart.Size = new System.Drawing.Size(8, 8);
            //newChart.TabIndex = 0;
            //newChart.Visible = true;
            //newChart.BackColor = System.Drawing.Color.Red;
            //newChart.Location = new System.Drawing.Point(93, 69);

            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = System.Drawing.Color.Beige;
            chartArea.Name = "ChartArea1";
            newChart.ChartAreas.Add(chartArea);

            //Legend legend1 = new Legend();
            //legend1.Name = "Legend1";
            //newChart.Legends.Add(legend1);

            //Series series1 = new Series();

            return newChart;
        }

        public Chart GiveMeATestChart()
        {
            return CreateDefaultChart();
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
        