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
        private PLCDBHandler dbHandler;
        private List<PLCConfig> listOfPlcConfig;
        private bool ConfigChanged = false;
        private ListBoxItem selectedItem;

         //ListBoxItem selectedItem = (ListBoxItem)listboxPLC.SelectedItem;
         //   if (selectedItem == null) return;

         //   int plcID = (int)selectedItem.Tag;
         //   PLCInterface plcInterface = GetPLCInterfaceFromList(plcID);
         //   this.StartOneWorkerThread(plcInterface);
        
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

        }

        private void UpdatePLCConfig()
        { 
        
        }

        private bool CheckInput()
        {
            bool validInputs = true;

            validInputs &= CheckIfPLCNameInputIsValid();
            validInputs &= CheckIfIPAdressInputIsValid();
            validInputs &= CheckIfIntervalInputIsValid();
            validInputs &= CheckIfPortInputIsValid();
            validInputs &= CheckIfPLCTypeInputIsValid();

            return validInputs;
        }

        private PLCConfig CreatePlcConfigObjectWithInputData()
        {
            PLCConfig plcConfig = new PLCConfig();

            plcConfig.plcName = TextBoxPLCName.Text;
            plcConfig.ipAddress = TextBoxIPAddress.Text;
            plcConfig.interval = Convert.ToInt32(TextBoxSampleIntervall.Text);
            plcConfig.plcPort = Convert.ToInt32(TextBoxPort.Text);
            plcConfig.type = ComboBoxPlcType.Text;

            return plcConfig;
        }

        private bool CheckIfPLCNameInputIsValid() 
        {
            return true;
        }

        private bool CheckIfIPAdressInputIsValid() 
        {
            return true;
        }

        private bool CheckIfIntervalInputIsValid() 
        {
            return true;
        }

        private bool CheckIfPortInputIsValid() 
        {
            return true;
        }

        private bool CheckIfPLCTypeInputIsValid() 
        {
            return true;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            PLCConfig plcConfig ;

            if (CheckInput())
            {
                plcConfig = CreatePlcConfigObjectWithInputData();

                if (!ConfigChanged)
                {
                     dbHandler.UpdatePLCConfigInDB(plcConfig);
                }
                else
                {
                    dbHandler.WritePLCConfigToDB(plcConfig);
                }
            }

        }

        private void ButtonDischarge_Click(object sender, RoutedEventArgs e)
        {
            ConfigChanged = false;
        }

        private void ButtonDischarge_Copy_Click(object sender, RoutedEventArgs e)
        {
            selectedItem = (ListBoxItem)ListBoxConfigs.SelectedItem;
        }
    }
}
