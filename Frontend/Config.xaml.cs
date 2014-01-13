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
        private ListBoxItem selectedItemPLCConfigConfigTabItem;
        private ListBoxItem selectedItemPLCConfigVariableConfigTabItem;
       
        
        public Config()
        {
            InitializeComponent();
            dbHandler = new PLCDBHandler();
            UpdateListBoxConfigs(ListBoxConfigs);
            ComboBoxPlcType.Items.Add("Siemens 300");
            ComboBoxPlcType.Items.Add("Simulator");
        }

        #region private functions

        private void UpdateListBoxConfigs(ListBox ListBox)
        {
            listOfPlcConfig = dbHandler.GetListOfPLCConfigurations();

            foreach (PLCConfig plcConfig in listOfPlcConfig)
            {
                ListBoxItem lbItem = new ListBoxItem();
                lbItem.Content = plcConfig.plcName;
                lbItem.Tag = plcConfig.plcID;
                ListBox.Items.Add(lbItem);
            
            }

        }

        private void UpdatePLCConfig()
        { 
        
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

            #region ValidCheckFunctions

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

            private bool CheckIfPLCNameInputIsValid() 
            {
                return true;
            }

            private bool CheckIfIPAdressInputIsValid() 
            {
                string ipAdress;

                ipAdress = TextBoxIPAddress.Text;

                return CheckIFStringIsAIPAdress(ipAdress);
                
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

        private bool CheckIFStringIsAIPAdress(string ipAdress)
        {
            char point = '.';
            bool isValid = false;

            for(int i = 0; i < ipAdress.Length; i++)
            {
                if (ipAdress.Length <= 16)
                {
                    if (i % 4 == 0)
                    {
                        isValid = char.Equals(ipAdress[i], point);
                    }
                    else if (i % 4 > 0)
                    {
                        isValid = Char.IsDigit(ipAdress[i]);
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else 
                {
                    break;
                }       
            }
            return isValid;
        }

            #endregion

        #endregion

        #region Buttons

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

        private void ButtonChangeConfig_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonVariableSetup_Click(object sender, RoutedEventArgs e)
        {
            TabControlConfig.SelectedIndex = 1;
            UpdateListBoxConfigs(ListBoxPLCConfigInVariableTabItem);
            
        }

        #endregion

        #region Listboxes

        private void ListBoxPLCConfigInVariableTabItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItemPLCConfigVariableConfigTabItem = (ListBoxItem)ListBoxPLCConfigInVariableTabItem.SelectedItem; 
        }

        private void ListBoxConfigs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItemPLCConfigConfigTabItem = (ListBoxItem)ListBoxConfigs.SelectedItem;
        }

        #endregion

    }
}
