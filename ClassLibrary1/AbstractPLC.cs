using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DBHandler;

namespace PLCLayer
{
    public abstract class AbstractPLC : PLCInterface
    {
        protected PLCConfig plcConfig;

        public AbstractPLC(int plcID)
        {
            plcConfig = loadData(plcID);
        }

        public AbstractPLC(PLCConfig plcConfig)
        {
            this.plcConfig = plcConfig;
        }

        public PLCConfig GetPLCConfig() 
        {
            return this.plcConfig;
        }

        public int GetSampleIntervall()
        {
            return this.plcConfig.interval;
        }
       
        abstract protected int GetNextReadingTime();
       
       
        abstract public void ConnectToPLC();
        abstract public void DisConnectFromPLC();
        abstract public List<PLCData> GetAllDataFromPLC();
       
            

        protected PLCConfig loadData(int PLCID)
        {
            PLCDBHandler dbHandler = new PLCDBHandler();
            return dbHandler.GetPLCConfigByID(PLCID);         
        }
       
    }
}
