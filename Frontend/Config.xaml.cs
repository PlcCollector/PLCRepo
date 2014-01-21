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
                bool validInputsMem = true;
                string errorMessage;
                //TODO
                validInputs &= ValidateInput.CheckIfPLCNameInputIsValid(TextBoxPLCName.Text, out errorMessage);
                if(!validInputs){MessageBox.Show(errorMessage,"ERROR PLC NAME");}
                validInputsMem = ValidateInput.CheckIFStringIsValidIPAdress(TextBoxIPAddress.Text, out errorMessage);
                if (!validInputsMem) { MessageBox.Show(errorMessage, "ERROR IP ADRESS"); }
                validInputs &= validInputsMem;
                validInputs &= ValidateInput.CheckIfIntervalInputIsValid(TextBoxSampleIntervall.Text, out errorMessage);
                if (!validInputs) { MessageBox.Show(errorMessage, "ERROR INTERVALL"); }

                validInputs &= ValidateInput.CheckIfPortInputIsValid(TextBoxPort.Text, out errorMessage);
                if (!validInputs) { MessageBox.Show(errorMessage, "ERROR PORT"); }

                validInputs &= ValidateInput.CheckIfPLCTypeInputIsValid(ComboBoxPlcType.Text, out errorMessage );
                if (!validInputs) { MessageBox.Show(errorMessage, "ERROR PLC TYPE"); }

                return validInputs;
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

        private void TabControlConfig_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateListBoxConfigs(ListBoxPLCConfigInVariableTabItem);
        }

       
      
    }
}
