using System;
using DBHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace DBHandlerTest
{
    [TestClass]
    public class PLCDBHandlerTest
    {
        private PLCDBHandler dbHandler;
        private int collectorID = 999;
        private int plcID = 888;
        private Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
      
        private DateTime currentDateTime = DateTime.Now;
        private string currentDateString;

        private string valideDateString;

        [TestInitialize]
        public void TestSetup()
        {
            this.dbHandler = new PLCDBHandler(true);
            this.ClearTableActivethreads();
            this.ClearTableSettings();
        }


        [TestCleanup]
        public void TestTearDown()
        {
            this.dbHandler.RollbackTransaction();
        }

        #region TestMethods

            #region GetSingleValueFromDB

            [TestMethod]
            [ExpectedException(typeof(MySqlException))]
            public void GetSingleValueFromDB_InvalidSQLStatement()
            {
                dbHandler.ExecuteSQLStatement("INSERT INT settings (settingsid, lowValue, highValue, unit) Valuess (10, 999.9769, 1515.1515, 'hugo')");
            }

            [TestMethod]
            public void GetSingleValueFromDB_TryToConvertValues()
            {
                double lowValue, highValue;
                string unit;

                dbHandler.ExecuteSQLStatement("INSERT INTO settings (settingsid, lowValue, highValue, unit) Values (10, 999.9769, 1515.1515, 'hugo')");

                lowValue = Convert.ToDouble(dbHandler.GetSingleValueFromDB("SELECT lowValue FROM settings WHERE settingsid = 10"));
                highValue = Convert.ToDouble(dbHandler.GetSingleValueFromDB("SELECT highValue FROM settings WHERE settingsid = 10"));
                unit = Convert.ToString(dbHandler.GetSingleValueFromDB("SELECT unit FROM settings WHERE settingsid = 10"));

                Assert.AreEqual(999.9769, lowValue);
                Assert.AreEqual(1515.1515, highValue);
                Assert.AreEqual("hugo", unit);
            }

            #endregion

            #region CheckIfCollectorIsAlreadyUsed

            [TestMethod]
            public void CheckIfCollectorIsAlreadyUsed_FirstEntry()
            {
                Boolean testResults;
            
                testResults = dbHandler.checkIfCollectorIsAlreadyUsed(this.collectorID, this.plcID);

                Assert.IsFalse(testResults);
            }

            [TestMethod]
            public void CheckIfCollectorIsAlreadyUsed_IsCollectorIDMyID()
            {
                InitializeDateStrings();
                Boolean testResults;

                dbHandler.ExecuteSQLStatement("INSERT INTO activethreads (plcid, threadid, timeout) Values (" + plcID.ToString() + "," + collectorID + ", '" + valideDateString + "')");

                testResults = dbHandler.checkIfCollectorIsAlreadyUsed(this.collectorID, this.plcID);

                Assert.IsFalse(testResults);
            
            }

            [TestMethod]
            public void CheckIfCollectorIsAlreadyUsed_ExpiredEntry()
            {
                InitializeDateStrings();
                Boolean testResults;
                int expiredThreadID = 777;
                DateTime expiredDate = currentDateTime.AddMinutes(-10);
                string expiredDateString = expiredDate.ToString("yyyy-MM-dd HH:mm:ss");


                dbHandler.ExecuteSQLStatement("INSERT INTO activethreads (plcid, threadid, timeout) Values (" + plcID.ToString() + "," + expiredThreadID.ToString() + ", '" + expiredDateString + "' )");

                testResults = dbHandler.checkIfCollectorIsAlreadyUsed(this.collectorID, this.plcID);

                Assert.IsFalse(testResults);

            }

            [TestMethod]
            public void CheckIfCollectorIsAlreadyUsed_AlreadOtherThreadActive()
            {
                InitializeDateStrings();
                Boolean testResults;
                int expiredThreadID = 777;

                dbHandler.ExecuteSQLStatement("INSERT INTO activethreads (plcid, threadid, timeout) Values (" + plcID.ToString() + "," + expiredThreadID + ", '" + valideDateString + "')");

                testResults = dbHandler.checkIfCollectorIsAlreadyUsed(this.collectorID, this.plcID);

                Assert.IsTrue(testResults);

            }

            #endregion

            #region ReadDataSettingsBySettingsIdFromDB

            [TestMethod]
            public void ReadDataSettingsBySettingsIdFromDB_ValueExists()
            {          
                PLCDataSettings datasettingsRead = new PLCDataSettings();
                PLCDataSettings datasettingsWrite = new PLCDataSettings();
           
                datasettingsWrite.lowValue = 99.99;
                datasettingsWrite.highValue = 111.11;
                datasettingsWrite.unit = "°C";
                int settingsId; 

                dbHandler.WriteDataSettingsToDB(datasettingsWrite);

                settingsId = Convert.ToInt32(dbHandler.GetSingleValueFromDB("SELECT MAX(settingsid) From settings"));        

                datasettingsRead = dbHandler.ReadDataSettingsBySettingsIdFromDB(settingsId);

                Assert.AreEqual(settingsId, datasettingsRead.settingsId);
                Assert.AreEqual(datasettingsWrite.lowValue, datasettingsRead.lowValue);
                Assert.AreEqual(datasettingsWrite.highValue, datasettingsRead.highValue);
                Assert.AreEqual(datasettingsWrite.unit, datasettingsRead.unit);           

            }

            [TestMethod]
            public void ReadDataSettingsBySettingsIdFromDB_InvalidId()
            {
                PLCDataSettings datasettingsRead = new PLCDataSettings();

                datasettingsRead = dbHandler.ReadDataSettingsBySettingsIdFromDB(-1);

                Assert.AreEqual(0,datasettingsRead.settingsId);
                Assert.AreEqual(0,datasettingsRead.lowValue);
                Assert.AreEqual(0,datasettingsRead.highValue);
                Assert.IsNull(datasettingsRead.unit);         
            }

            #endregion


        #endregion

        #region private Functions

        private void ClearTableActivethreads()
        {
            dbHandler.ExecuteSQLStatement("DELETE FROM activethreads");
        }
        private void ClearTableSettings()
        {
            dbHandler.ExecuteSQLStatement("DELETE FROM settings");
        }

        private void InitializeDateStrings()
        {
            this.currentDateString = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime valideTime = currentDateTime.AddMinutes(10);
            this.valideDateString = valideTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        #endregion
    }
}
