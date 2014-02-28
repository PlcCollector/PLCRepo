using DataObjects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DBHandler
{
    public class PLCDBHandler
    {
        
        private string myConnectionString = "SERVER=localhost;" + "DATABASE=plcdb;" + "UID=root;" + "PASSWORD='';";
        private MySqlConnection connection;
        private IDbTransaction transaction;
        private bool usesTransaction = false;      
                                                                    
        public PLCDBHandler()
        {
            connection = new MySqlConnection(myConnectionString);
        }

        public PLCDBHandler(bool withTransaction)
        {
            connection = new MySqlConnection(myConnectionString);

            usesTransaction = withTransaction;

            if (withTransaction)
            {
                connection.Open();
                transaction = connection.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (transaction != null)
            {
                transaction.Commit();
            }

            connection.Close();
            this.usesTransaction = false;
        }

        public void RollbackTransaction()
        {
            
            this.connection.Close();
            this.connection.Open();
            

            if (transaction != null)
            {
                transaction.Rollback();
            }

            connection.Close();
            this.usesTransaction = false;
        }

        #region public Functions

        /// <summary>
        /// Reads all Data Settings by the Setting Id
        /// </summary>
        /// <returns>PLCDataSettings</returns>
        public PLCDataSettings ReadDataSettingsBySettingsIdFromDB(int settingsId)
        {
            PLCDataSettings datasettings = new PLCDataSettings();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM settings WHERE settingsId = " + settingsId.ToString(); ;

            MySqlDataReader Reader;

            this.OpenConnection();

            Reader = command.ExecuteReader();
            
            while (Reader.Read())
            {
                datasettings.settingsId = Convert.ToInt32(Reader.GetValue(0));
                datasettings.lowValue = Convert.ToDouble(Reader.GetValue(1));
                datasettings.highValue = Convert.ToDouble(Reader.GetValue(2));
                datasettings.unit = Convert.ToString(Reader.GetValue(3));

            }

            Reader.Close();

            this.CloseConnection();
            
            return datasettings;
        }

        public object GetSingleValueFromDB(string sqlstatement)
        { 
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sqlstatement;
            MySqlDataReader Reader;
            object result;

            this.OpenConnection();

            try
            {
                Reader = command.ExecuteReader();
            }
            catch (MySqlException exeption)
            {
                throw exeption;
            }

            if (Reader.Read())
            {
               result = Reader.GetValue(0);              
            }

            else
            {
                result = null;
            }

            Reader.Close();

            this.CloseConnection();

            return result;
        }

        /// <summary>
        /// returns a list of PLCConfig - Elements from DB 
        /// </summary>
        /// <returns>List of PLCConfig</returns>
        public List<PLCConfig> GetListOfPLCConfigurations()
        {
            List<PLCConfig> listOfPLCConfigs = new List<PLCConfig>();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT* FROM plcconfig";
                     
            MySqlDataReader Reader;
            
            try
            {
                this.OpenConnection();
                Reader = command.ExecuteReader();

                while (Reader.Read())
                {
                    PLCConfig nextPLCConfig = new PLCConfig();
                    nextPLCConfig.plcID = Convert.ToInt32(Reader.GetValue(0));
                    nextPLCConfig.plcName = Reader.GetValue(1).ToString();
                    nextPLCConfig.type = Reader.GetValue(2).ToString();
                    nextPLCConfig.plcPort = (int)Reader.GetValue(3);
                    nextPLCConfig.ipAddress = Reader.GetValue(4).ToString();
                    nextPLCConfig.interval = (int)Reader.GetValue(5);

                    listOfPLCConfigs.Add(nextPLCConfig);
                }
                this.CloseConnection();
            }
            catch (MySqlException exeption)
            {
                throw (exeption);
            }
            return listOfPLCConfigs;
        }

        /// <summary>
        /// Writes a new PLC-Configuration into the DB
        /// </summary>
        /// <param name="plcConfig"></param>
        public void WritePLCConfigToDB(PLCConfig plcConfig) 
        {

            string sqlCommand = "INSERT INTO plcconfig (plcid, name , type, port, ipadress, refreshtime, scanninginterval) VALUES (" + plcConfig.plcID.ToString() + ",'" + plcConfig.plcName +
                                "', '" + plcConfig.type + "'," + plcConfig.plcPort.ToString() + ", '"+ plcConfig.ipAddress +"', "+plcConfig.refreshTime.ToString() + ", "+  plcConfig.interval.ToString() + ")"; 
            try
            {
                ExecuteSQLStatement(sqlCommand);
            }

            catch (MySqlException exeption)
            {
                throw (exeption);
            }

        }

        public void UpdatePLCConfigInDB(PLCConfig plcConfig)
        { 
            //TODO
        }

        /// <summary>
        /// This function writes Process -Datas from the PLC into the Database
        /// </summary>
        /// <param name="plcData"></param>
        public void WritePLCDataToDB(PLCData plcData)
        {
            string sqlCommand = "";

            if (plcData.dataType == "int")   //check if Value is a int
            {
                 sqlCommand = "INSERT INTO data ( datameterinfo , valueint, settingsid, plcid, timestamp) VALUES (" + plcData.dataMeterInfo.ToString() + "," + plcData.intVal.ToString() +
                                    ", " + plcData.settingId.ToString() + "," + plcData.plcID.ToString() + ", '" + plcData.timestamp + "')"; 
            }
            else if (plcData.dataType == "float")  //check if Value is a float
            {
                sqlCommand = "INSERT INTO data ( datameterinfo , valuefloat, settingsid, plcid, timestamp) VALUES (" + plcData.dataMeterInfo.ToString() + "," + plcData.floatVal.ToString() +
                                    ", " + plcData.settingId.ToString() + "," + plcData.plcID.ToString() + ", '" + plcData.timestamp + "')"; 
            }
            else if (plcData.dataType == "bool")
            {
                sqlCommand = "INSERT INTO data ( datameterinfo , valuebool, settingsid, plcid, timestamp) VALUES (" + plcData.dataMeterInfo.ToString() + "," + plcData.bitVal.ToString() +
                                    ", " + plcData.settingId.ToString() + "," + plcData.plcID.ToString() + ", '" + plcData.timestamp + "')"; 
            }

            try
            {
                ExecuteSQLStatement(sqlCommand);
            }

            catch (MySqlException exeption)
            {
                throw (exeption);
                //throw new CustomException("Write Data to the plc is not possible.", exeption); //-------->todo
            }


        }

        /// <summary>
        /// Writes a Variable Configuration in the DB
        /// </summary>
        /// <param name="variableConfig"></param>
        public void WritePLCVariableConfigToDB(PLCVariableConfig variableConfig)
        {

            string sqlCommand = "INSERT INTO plcvariables (types, name, DataBlockNr, startbyte, lenght, plcid) VALUES (" + "'" + variableConfig.type + "', '" + variableConfig.variableName + "'," +
                                variableConfig.dataBlockNr.ToString() + "," + variableConfig.startByte.ToString() + "," + variableConfig.variableLenght.ToString() + "," + variableConfig.plcID.ToString() + ")"; 
            try
            {
                ExecuteSQLStatement(sqlCommand);
            }

            catch (MySqlException exeption)
            {
                throw (exeption);
            }
        }

        /// <summary>
        /// Writes the Settings of the variable in the Db
        /// </summary>
        /// <param name="dataSettings"></param>
        public void WriteDataSettingsToDB(PLCDataSettings dataSettings)
        {

            MySqlCommand cmd = new MySqlCommand("INSERT INTO `settings` (`lowValue`, `highValue`, `unit`) VALUES ( @lowValue, @highValue, @unit)");
          
            cmd.Parameters.Add("@lowValue", MySqlDbType.Double).Value = dataSettings.lowValue;
            cmd.Parameters.Add("@highvalue", MySqlDbType.Double).Value = dataSettings.highValue;
            cmd.Parameters.Add("@unit", MySqlDbType.String).Value = dataSettings.unit;
            cmd.Connection = this.connection;

            ExecuteSQLStatement("",cmd);          

        }

        /// <summary>
        /// Writes a list of PLC-Datas to the Db
        /// </summary>
        /// <param name="plcDatas"></param>
        public void WriteListOfPLCDatasToDB(List<PLCData> plcDatas)
        {
            foreach(PLCData plcData in plcDatas)
            {
                WritePLCDataToDB(plcData);
            }
        }

        public void WriteErrorMessageToDb(string messageText, string errorInformation)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `logging` (`messagetext`, `errorinformation`) VALUES (@messagetext, @errorinformation)");

            cmd.Parameters.Add("@messagetext", MySqlDbType.String).Value = messageText;
            cmd.Parameters.Add("@errorinformation", MySqlDbType.String).Value = errorInformation;
            cmd.Connection = this.connection;

            ExecuteSQLStatement("",cmd);
        }

        public PLCConfig GetPLCConfigByID(int plcID)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM plcconfig WHERE plcid = @plcID";
            command.Parameters.Add("@plcID", MySqlDbType.Int32).Value = plcID;
         
            MySqlDataReader Reader;

            this.OpenConnection();
            Reader = command.ExecuteReader();
            
            Reader.Read();
            //TODO check if exists
            
            PLCConfig loadedPLCConfig = new PLCConfig();

            loadedPLCConfig.plcID = Convert.ToInt32(Reader.GetValue(0));  
            loadedPLCConfig.plcName = Reader.GetValue(1).ToString();      
            loadedPLCConfig.type = Reader.GetValue(2).ToString();       
            loadedPLCConfig.plcPort = (int)Reader.GetValue(3);          
            loadedPLCConfig.ipAddress = Reader.GetValue(4).ToString();  
            loadedPLCConfig.interval = (int)Reader.GetValue(5);

            this.CloseConnection();

            return loadedPLCConfig;
        }

        /// <summary>
        /// search all variables by plc Id and returns a list of PLC Variable Configs
        /// </summary>
        /// <param name="plcID"></param>
        /// <returns>Returns a list of PLC Variables</returns>
        public List<PLCVariableConfig> GetListOfPLCVariables(int plcID)
        {
            List<PLCVariableConfig> listOfPLCVariables = new List<PLCVariableConfig>();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT* FROM plcvariables WHERE @plcID = plcid";
            command.Parameters.Add("@plcID", MySqlDbType.Int32).Value = plcID;

            MySqlDataReader Reader;

            this.OpenConnection();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                PLCVariableConfig nextPLCVariableConfig = new PLCVariableConfig();
                nextPLCVariableConfig.variableId = Convert.ToInt32(Reader.GetValue(0));
                nextPLCVariableConfig.type = Reader.GetValue(6).ToString();
                nextPLCVariableConfig.variableName = Reader.GetValue(1).ToString();
                nextPLCVariableConfig.dataBlockNr = Convert.ToInt32(Reader.GetValue(2));
                nextPLCVariableConfig.startByte = Convert.ToInt32(Reader.GetValue(3));
                nextPLCVariableConfig.variableLenght = Convert.ToInt32(Reader.GetValue(4));
                nextPLCVariableConfig.plcID = Convert.ToInt32(Reader.GetValue(5));

                listOfPLCVariables.Add(nextPLCVariableConfig);
            }
            this.CloseConnection();

            return listOfPLCVariables;
        }

        public bool GetPLCVariableByName(string Name)
        {
            //TODO
            //MySqlCommand command = connection.CreateCommand();
            //command.CommandText = "SELECT* FROM plcvariables WHERE @plcID = plcid";
            //command.Parameters.Add("@plcID", MySqlDbType.Int32).Value = plcID;

            //MySqlDataReader Reader;

            //this.OpenConnection();
            //Reader = command.ExecuteReader();
            return true;
        }

        public bool checkIfCollectorIsAlreadyUsed(int collectorID, int plcID)
        {
            ulong alreadyActive;

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "checkAndSetActiveCollector";
           
            this.OpenConnection();
            cmd.Parameters.Add("testedCollectorID", MySqlDbType.Int32).Value = collectorID;
            cmd.Parameters.Add("testedPLCID", MySqlDbType.Int32).Value = plcID;

            cmd.Parameters.Add(new MySqlParameter("@alreadyActive", MySqlDbType.Bit));
            cmd.Parameters["@alreadyActive"].Direction = ParameterDirection.Output;
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException exeption)
            {
                throw (exeption);
            }

            alreadyActive = (ulong)cmd.Parameters["@alreadyActive"].Value;
            this.CloseConnection();

            if (alreadyActive == 0) return false;
            else return true;
        }

        /// <summary>
        /// This function open the Database-Connection and executes the SQL-Statement
        /// </summary>
        /// <param name="sqlCommand"></param>
        public void ExecuteSQLStatement(string sqlCommand)
        {
            MySqlCommand command = new MySqlCommand(sqlCommand, connection);
            
            this.OpenConnection();
            command.ExecuteNonQuery();
            this.CloseConnection();
        }

        /// <summary>
        /// This function executes an SQL Statement
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteSQLStatement(string sqlCommand, MySqlCommand command)
        {
            this.OpenConnection();

            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException exeption)
            {
                throw (exeption);
            }

            this.CloseConnection();
        }

        #endregion


        #region private Functions


        /// <summary>
        /// Creates a Instance of a  MySqlConnection - Object for the membervariable connection
        /// </summary>
        /// <param name="connectionString"></param>
        private void ConnectToDB(string connectionString)
        {
            this.connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// open the SQL-Connection, execute a SELECT- Statement and write them in a string
        /// </summary>
        /// <param name="mySqlCommand"></param>
        /// <returns>string</returns>
        private string ReadFromDB(string mySqlCommand)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = mySqlCommand;

            MySqlDataReader Reader;
            OpenConnection();
            Reader = command.ExecuteReader();
            string row = "";
            while (Reader.Read())
            {              
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    row += Reader.GetValue(i).ToString() + ", ";
                } 
            }
            this.CloseConnection();

            return row;
        }

        private void WriteToDb(string mySqlCommand)
        {
            
        }
        //    using MySql.Data.MySqlClient;

        private void OpenConnection()
        {
            if (!this.usesTransaction)
            {
                if (connection != null && connection.State != ConnectionState.Open)
                {
                    this.connection.Open();
                }
            }

        }

        private void CloseConnection()
        {
            if (!this.usesTransaction)
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    this.connection.Close();
                }
            }
        }


        #endregion



    }
}
