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
        private bool IsConfigForUpdate = false;
        private ListBoxItem selectedItemPLCConfigConfigTabItem;
        private ListBoxItem selectedItemPLCConfigVariableConfigTabItem;
        private PLCConfig loadedPLCConfigFromDB;
        
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
            PLCConfig plcConfig = new PLCConfig();

            if (CheckInput())
            {
                plcConfig = CreatePlcConfigObjectWithInputData();

                dbHandler.UpdatePLCConfigInDB(plcConfig);
            }
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

        private void FillTheTextBoxesWithTheConfigToChange(ListBoxItem lbItem)
        {
            PLCConfig plcConfig = new PLCConfig();

            plcConfig = dbHandler.GetPLCConfigByID((int)lbItem.Tag);

            TextBoxPLCName.Text = plcConfig.plcName;
            TextBoxIPAddress.Text = plcConfig.ipAddress;
            TextBoxSampleIntervall.Text = Convert.ToString(plcConfig.interval);
            TextBoxPort.Text = Convert.ToString(plcConfig.plcPort);

            this.loadedPLCConfigFromDB = plcConfig;
        }

        private void CleanConfigTextBoxes()
        {
            TextBoxPLCName.Text = "";
            TextBoxIPAddress.Text = "";
            TextBoxSampleIntervall.Text = "";
            TextBoxPort.Text = "";
        }

            #region ValidCheckFunctions

            private bool CheckInput()
            {
                bool validInputs = true;

                validInputs &= CheckIfPLCNameInputIsValid(TextBoxPLCName.Text);
                validInputs &= CheckIfIPAdressInputIsValid(TextBoxIPAddress.Text);
                validInputs &= CheckIfIntervalInputIsValid(TextBoxSampleIntervall.Text);
                validInputs &= CheckIfPortInputIsValid(TextBoxPort.Text);
                validInputs &= CheckIfPLCTypeInputIsValid(ComboBoxPlcType.Text);

                return validInputs;
            }

            private bool CheckIfPLCNameInputIsValid(string plcName) 
            {
                if (string.IsNullOrEmpty(plcName) ||!(Encoding.UTF8.GetByteCount(plcName).Equals(plcName.Length))  )
                {
                    MessageBox.Show("Der PLC Name ist Leer oder enthält Sonderzeichen", "Fehler PLC Name");
                    return false;
                }

                if(plcName.Length > 20)
                {
                    MessageBox.Show("Der PLC Name ist zu lang", "Fehler PLC Name");
                    return false;
                }
                return true;
            }

            private bool CheckIfIPAdressInputIsValid(string ipAdress) 
            {
                ipAdress = TextBoxIPAddress.Text;

                return CheckIFStringIsAIPAdress(ipAdress);
                
            }

            private bool CheckIfIntervalInputIsValid(string intervall) 
            {
                int resultTryParse;
                bool isNumber = int.TryParse(intervall, out resultTryParse);

                if (resultTryParse > 86400000 || intervall.Length >= 9)   //Limit Sample Intervall = 86400000 ms = 1Day
                {
                    MessageBox.Show("Das Intervall ist zu groß. max. 1Tag", "Fehler Intervallwert");
                }

                if (!isNumber)
                {
                    MessageBox.Show("Das Intervall ist kein ganzzahliger Wert ", "Fehler Intervallwert");
                    return false;
                }
                
                return true;
            }

            private bool CheckIfPortInputIsValid(string port) 
            {
                int resultTryParse;
                bool isNumber = int.TryParse(port, out resultTryParse);

                if (!isNumber)
                {
                    MessageBox.Show("Der Port muss aus einer ganzen Zahl bestehen", "Fehler Eingabe PORT");
                    return false;
                }
                if (resultTryParse <= 0 || resultTryParse > 65535)   // available Ports 0 - 65535
                {
                    MessageBox.Show("Dieser Port steht nicht zur Verfügung. Es stehen nur Ports von 0 - 65535 zur Verfügung", "Fehler Eingabe PORT");
                }
                return true;
            }

            private bool CheckIfPLCTypeInputIsValid(string plcInputType) 
            {
                if (string.IsNullOrEmpty(plcInputType))
                {
                    MessageBox.Show("Es wurde kein PLC-Typ ausgewählt", "Fehler PLC-Typ");
                    return false;
                }
                return true;
            }

            private bool CheckIFStringIsAIPAdress(string ipAdress)
        {
            char point = '.';
            bool isValid = false;
            int i, resultTryParse;
            string[] adressParts = ipAdress.Split('.');

            for (i = 0; i < adressParts.Length; i++)
            {
                if (adressParts.Length == 4 && int.TryParse(adressParts[i],out resultTryParse) && resultTryParse <255 )
                {
                    isValid = true;
                }
                else 
                {
                    isValid = false;
                }
                if (!isValid)
                {
                    MessageBox.Show("IP-Adresse wurde falsch eingegeben!! ", "Fehler IP-Adresse");
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

                if (IsConfigForUpdate)
                {
                     dbHandler.UpdatePLCConfigInDB(loadedPLCConfigFromDB);
                     IsConfigForUpdate = false;
                }
                else
                {
                    dbHandler.WritePLCConfigToDB(plcConfig);
                }
            }

        }

        private void ButtonDischarge_Click(object sender, RoutedEventArgs e)
        {
            IsConfigForUpdate = false;
            CleanConfigTextBoxes();
        }

        private void ButtonChangeConfig_Click(object sender, RoutedEventArgs e)
        {
            //Check if lbItem is not null
            FillTheTextBoxesWithTheConfigToChange((ListBoxItem)ListBoxConfigs.SelectedItem);

            IsConfigForUpdate = true;
            //todo
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
