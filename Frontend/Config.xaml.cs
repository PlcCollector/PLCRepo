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
using DBHandler;
using DataObjects;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Config.xaml
    /// </summary>
    public partial class Config : Window
    {
        PLCDBHandler dbHandler;
        List<PLCConfig> listOfPlcConfig;
        
        public Config()
        {
            InitializeComponent();
            dbHandler = new PLCDBHandler();
            UpdateListBoxConfigs();
            ComboBoxPlcType.Items.Add("Siemens 300");
            ComboBoxPlcType.Items.Add("Simulator");
        }

        private void UpdateListBoxConfigs()
        {
            listOfPlcConfig = dbHandler.GetListOfPLCConfigurations();

            foreach (PLCConfig plcConfig in listOfPlcConfig)
            {
                ListBoxItem lbItem = new ListBoxItem();
                lbItem.Content = plcConfig.plcName;
                lbItem.Tag = plcConfig.plcID;
                ListBoxConfigs.Items.Add(lbItem);
            
            }

            //listboxPLC.Items.Clear();

            //foreach (PLCInterface plcInterface in listOfPLCs)
            //{
            //    ListBoxItem listBoxItem = new ListBoxItem();
            //    listBoxItem.Content = plcInterface.GetPLCConfig().plcID.ToString() + "." + plcInterface.GetPLCConfig().plcName;
            //    listBoxItem.Tag = plcInterface.GetPLCConfig().plcID;
            //    listboxPLC.Items.Add(listBoxItem);
            //}
            //this.Show();

            //this.SetColorOfListRunningBoxElements();
        }
    }
}
