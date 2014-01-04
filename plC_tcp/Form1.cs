using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBHandler;
using DataObjects;

namespace plC_tcp
{
    public partial class Form1 : Form
    {
        private string ConnectionState = "......";
        private PLC_DATAS plcDatas;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnectPLC_Click(object sender, EventArgs e)
        {
            plcDatas = new PLC_DATAS();

            PLCDBHandler plcDbHandler = new PLCDBHandler();
            List<PLCConfig> plcConfigs;     //---->it works          
            //PLCDataSettings dataSettings = new PLCDataSettings();

            #region plc config tests

            //PLCConfig plcConfig = new PLCConfig();
            //plcConfig.ipAddress = "10.10.10.10";
            //plcConfig.plcID = 11;
            //plcConfig.plcName = "testname";
            //plcConfig.type = "Siemens300";
            //plcConfig.interval = 100;
            //plcConfig.refreshTime = 200;
            //plcConfig.plcPort = 102; 

            //plcDbHandler.WritePLCConfigToDB(plcConfig);
            #endregion

            #region plcDatatest
            //PLCData plcData = new PLCData();
            //plcData.floatVal = 20;
            //plcData.intVal = 100;
            //plcData.plcID = 10;
            //plcData.settingId = 15;
            //plcData.timestamp = "2013-11-25 00:52:10";
            //plcData.dataType = "float";

            //plcDbHandler.WritePLCDataToDB(plcData);
            #endregion

            #region variable config test
            //PLCVariableConfig variableConfig = new PLCVariableConfig();
            //variableConfig.type = "int";
            //variableConfig.plcID = 2;
            //variableConfig.startByte = 10;
            //variableConfig.dataBlockNr = 1;
            //variableConfig.variableLenght = 9;
            //variableConfig.variableName = "VariableTemperatur";

            //plcDbHandler.WritePLCVariableConfigToDB(variableConfig);
            #endregion

            #region data settings test

            //PLCDataSettings dataSettings = new PLCDataSettings();
            //dataSettings.highValue = 11.1;
            //dataSettings.lowValue = 1.3;
            //dataSettings.settingsId = 3;
            //dataSettings.unit = "°C";
            //plcDbHandler.WriteDataSettingsToDB(dataSettings);

            #endregion

            plcConfigs = plcDbHandler.GetListOfPLCConfigurations();

            //dataSettings = plcDbHandler.ReadDataSettingsBySettingsIdFromDB(3);
            ConnectionState = plcDatas.ConnectPLC(txtIPAdress.Text, 0, 2);
            
            

            if (ConnectionState == "PLC connected")
            {
                lblCurrPLCConnectionState.BackColor = Color.Green;
                lblCurrPLCConnectionState.Text = ConnectionState;
            }
            else 
            {
                lblCurrPLCConnectionState.BackColor = Color.IndianRed;
                lblCurrPLCConnectionState.Text = ConnectionState;
            }
        }

        private void btnReadDB_Click(object sender, EventArgs e)
        {
            //lblValue.Text = Convert.ToString(plcDatas.ReadBytes());
        }
    }
}
