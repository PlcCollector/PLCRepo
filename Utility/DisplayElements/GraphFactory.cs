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
    class GraphFactory
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
            newChart.Size = new System.Drawing.Size(8, 8);
            newChart.TabIndex = 0;
            newChart.Visible = true;

            ChartArea chartArea = new ChartArea();
            chartArea.Name = "ChartArea1";
            newChart.ChartAreas.Add(chartArea);

            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            newChart.Legends.Add(legend1);

            Series series1 = new Series();

            return newChart;
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
