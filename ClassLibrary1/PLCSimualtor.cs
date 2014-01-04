using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DBHandler;

namespace PLCLayer
{
    public class PLCSimualtor : AbstractPLC
    {  
    
        public PLCSimualtor(PLCConfig plcConfig) : base(plcConfig)
        {
        }

        public PLCSimualtor(int plcID) : base(plcID)
        {
        }    

        protected override int GetNextReadingTime()
        {
            return 1;
        }

        public override List<PLCData> GetAllDataFromPLC()
        {
            List<PLCVariableConfig> allVariables = this.GetListOfPLCVariables();
            List<PLCData> allData = new List<PLCData>();

            foreach (PLCVariableConfig var in allVariables)
            {
                PLCData readedData;
                switch (var.type)
                {
                    case "int":
                        readedData = this.ReadIntFromPLC(var);
                        break;
                    case "float":
                        readedData = this.ReadFloatFromPLC(var);
                        break;
                    default:
                        readedData = this.ReadBitFromPLC(var);
                        break;
                }

                allData.Add(readedData);
            }

            return allData;
        }

        private List<PLCVariableConfig> GetListOfPLCVariables()
        {
            PLCDBHandler dbHandler = new PLCDBHandler();
            return dbHandler.GetListOfPLCVariables(plcConfig.plcID);
        }

        private PLCData ReadIntFromPLC(PLCVariableConfig plcVar)
        {
            Random rnd = new Random();
            int randomValue = rnd.Next(1, 666);
            return new PLCData(plcVar.plcID, plcVar.variableId, plcVar.type, randomValue);
        }

        private PLCData ReadFloatFromPLC(PLCVariableConfig plcVar)
        {
            Random rnd = new Random();

            float randomValue = Convert.ToSingle(rnd.NextDouble()*666);
            return new PLCData(plcVar.plcID, plcVar.variableId, plcVar.type, randomValue);
        }

        private PLCData ReadBitFromPLC(PLCVariableConfig plcVar)
        {
            Random rnd = new Random();
            int randomValue = rnd.Next(0, 1);
            bool randomResult;

            if (randomValue == 0) randomResult = false;
            else randomResult = true;

            return new PLCData(plcVar.plcID, plcVar.variableId, plcVar.type, randomResult);
        }

        public override void ConnectToPLC()
        {
            return;
        }

        public override void DisConnectFromPLC()
        {
            return;
        }

       
    }

}
