using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using PLCLayer;
using System.Threading;
using DBHandler;

namespace DataCollectors
{
    public class DataCollector
    {
        private PLCInterface plcInterface;
        private PLCDBHandler dbHandler;
       
        public DataCollector( PLCInterface plcInterface)
        {          
            //read DB-Config from Txt-file
           this.plcInterface = plcInterface;
           this.dbHandler = new PLCDBHandler();
        }

        #region public Functions

        public void run()
        {
            int threadID = this.CreateThreadID();  
                  
             List<PLCData> plcDatas;
         
             Boolean threadContinue = true;


             while (threadContinue)
             {
             
                 if (this.dbHandler.checkIfCollectorIsAlreadyUsed(threadID, plcInterface.GetPLCConfig().plcID))
                 {                 
                     System.Threading.Thread.Sleep(PLCConstants.DefaultRetryWaitingTime);
                     continue;
                 }

                 try
                 {
                     plcDatas = plcInterface.GetAllDataFromPLC();

                     dbHandler.WriteListOfPLCDatasToDB(plcDatas);
                 }
                 catch (Exception ex)
                 {
                     dbHandler.WriteErrorMessageToDb(ex.Message.ToString(), this.ToErrorString());
                 }

                 plcDatas = null;

                 System.Threading.Thread.Sleep(plcInterface.GetSampleIntervall());
             
             }
        }

        #endregion


        #region private Functions

        private int CreateThreadID()
        {
            Random ramdomNumber = new Random();
            int threadID = ramdomNumber.Next(1, int.MaxValue-10000);

            threadID += plcInterface.GetPLCConfig().plcID;

            return threadID;
        }

        private string ToErrorString()
        {
            return plcInterface.GetPLCConfig().ToErrorString();
        }

        #endregion
    }
}
