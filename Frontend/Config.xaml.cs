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

            ComboBoxVariableType.Items.Add("INT32");
            ComboBoxVariableType.Items.Add("REAL");
            ComboBoxVariableType.Items.Add("BOOL");
        }

        #region private functions

        private void UpdateListBoxConfigs(ListBox ListBox)
        {
            listOfPlcConfig = dbHandler.GetListOfPLCConfigurations();
            ListBox.Items.Clear();

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

            if (CheckPLCConfigInputInPLCConfigItem())
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

        private PLCVariableConfig CreateVariableConfigWithInputData()
        {
            PLCVariableConfig variableConfig = new PLCVariableConfig();

            variableConfig.dataBlockNr = Convert.ToInt32(TextBoxDataBlock.Text);
            variableConfig.type = ComboBoxVariableType.Text;
            variableConfig.variableLenght = CreateVariableConfigByType(variableConfig.type);
            //variableConfig.plcID = FindPLCIDByPLCType(ComboBoxVariableType.Text);
            variableConfig.startByte = Convert.ToInt32(TextBoxStartByte.Text);
            variableConfig.startBit = Convert.ToInt32(TextboxStartBit.Text);
            

            return variableConfig;

        }

        private int CreateVariableConfigByType(string variableType)
        {
            if (variableType == "real" || variableType == "integer")
            {
                return 32;
            }
            return 1;
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

            private bool CheckPLCConfigInputInPLCConfigItem()
            {
                bool validInputs = true;
                bool validInputsMem = true;
                string errorMessage;



                validInputsMem = ValidateInput.CheckIfNameInputIsValid(TextBoxPLCName.Text, out errorMessage);
                if (!validInputsMem) { MessageBox.Show(errorMessage, "ERROR PLC NAME"); }
                validInputs &= validInputsMem;

                validInputsMem = ValidateInput.CheckIFStringIsValidIPAdress(TextBoxIPAddress.Text, out errorMessage);
                if (!validInputsMem) { MessageBox.Show(errorMessage, "ERROR IP ADRESS"); }
                validInputs &= validInputsMem;

                validInputsMem = ValidateInput.CheckIfIntervalInputIsValid(TextBoxSampleIntervall.Text, out errorMessage);
                if (!validInputsMem) { MessageBox.Show(errorMessage, "ERROR INTERVALL"); }
                validInputs &= validInputsMem;

                validInputsMem = ValidateInput.CheckIfPortInputIsValid(TextBoxPort.Text, out errorMessage);
                if (!validInputsMem) { MessageBox.Show(errorMessage, "ERROR PORT"); }
                validInputs &= validInputsMem;

                validInputsMem = ValidateInput.CheckIfPLCTypeInputIsValid(ComboBoxPlcType.Text, out errorMessage);
                if (!validInputsMem) { MessageBox.Show(errorMessage, "ERROR PLC TYPE"); }
                validInputs &= validInputsMem;

                return validInputs;
            }

            private bool CheckVariableConfigInputInVariableConfigItem()
            {
                bool validInputs = true;
                bool validInputsMem = true;
                string errorMessage;

                validInputsMem = ValidateInput.CheckIfNameInputIsValid(TextBoxVariableName.Text, out errorMessage);
                if (!validInputs) { MessageBox.Show(errorMessage, "ERROR VARIABLE NAME"); }
                validInputs &= validInputsMem;

                validInputsMem = ValidateInput.CheckIfValueIsIntegerMaxMin(TextBoxDataBlock.Text, out errorMessage, 500, 0);
                if (!validInputs) { MessageBox.Show(errorMessage, "ERROR DB INPUT"); }
                validInputs &= validInputsMem;

                validInputsMem = ValidateInput.CheckIfValueIsIntegerMaxMin(TextBoxStartByte.Text, out errorMessage, 200, 0);
                if (!validInputs) { MessageBox.Show(errorMessage, "ERROR DB startbyte INPUT"); }
                validInputs &= validInputsMem;

                validInputsMem = ValidateInput.CheckIfValueIsIntegerMaxMin(TextboxStartBit.Text, out errorMessage, 7, 0);
                if (!validInputs) { MessageBox.Show(errorMessage, "ERROR DB startbit INPUT"); }
                validInputs &= validInputsMem;

                validInputsMem = ValidateInput.CheckIfVariableTypeInputIsValid(ComboBoxVariableType.Text, out errorMessage);
                if (!validInputs) { MessageBox.Show(errorMessage, "ERROR Variable-Typ INPUT"); }
                validInputs &= validInputsMem;
                //TODO

                return validInputs;
            }

            #endregion

        #endregion

        #region Buttons

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            PLCConfig plcConfig ;

            if (CheckPLCConfigInputInPLCConfigItem())
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

        #region ButtonsVariableConfigTabItem

        private void ButtonAddVariable_Click(object sender, RoutedEventArgs e)
        {
            PLCConfig plcConfig = new PLCConfig();

            if (selectedItemPLCConfigVariableConfigTabItem == null)
            {
                MessageBox.Show("Bitte Wählen sie eine PLC aus", "Fehler PLC auswahl");
                return;
            }

            if (CheckVariableConfigInputInVariableConfigItem())
            { 
            
            }
            // fillVarConfig()
            // Write Config to DB
            // Update Listbox

        }

        private void ButtonRemoveVariable_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #endregion

        #region Listboxes

        private void ListBoxConfigs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItemPLCConfigConfigTabItem = (ListBoxItem)ListBoxConfigs.SelectedItem;
        }

        private void ListBoxPLCConfigInVariableTabItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItemPLCConfigVariableConfigTabItem = (ListBoxItem)ListBoxPLCConfigInVariableTabItem.SelectedItem;
        }

        #endregion

        private void TabControlConfig_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            UpdateListBoxConfigs(ListBoxPLCConfigInVariableTabItem);
        }

        private void ListBoxVariablesInVariableTabItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControlConfig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



       
      
    }
}
