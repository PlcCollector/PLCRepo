using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace PLCLayer
{
    public class Siemens300seriesOverTCP : AbstractPLC 
    {
        private libnodave.daveConnection plcConnection;

        public Siemens300seriesOverTCP(PLCConfig plcConfig) : base(plcConfig)
        {
        }

        public Siemens300seriesOverTCP(int plcID) : base(plcID)
        {
        }      
                    
        #region Public Functions
              

        public override void ConnectToPLC()
        {
            libnodave.daveOSserialType fds;
            libnodave.daveInterface plcInterface;
            int isConnected;

            fds.rfd = libnodave.openSocket(plcConfig.plcPort, plcConfig.ipAddress);
            fds.wfd = fds.rfd;

            plcInterface = new libnodave.daveInterface(fds, plcConfig.plcName, 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
            plcInterface.setTimeout(1000000);

            //Connect to PLC
            this.plcConnection = new libnodave.daveConnection(plcInterface, 0, plcConfig.plcRack, plcConfig.plcSlot);
            isConnected = this.plcConnection.connectPLC();
            
            if (isConnected != 0) throw new ApplicationException("Could not establish connection");
        }

        public override void DisConnectFromPLC()
        {
            libnodave.closePort(plcConfig.plcPort);
            this.plcConnection.disconnectPLC();
            this.plcConnection = null;
        }

        public override List<PLCData> GetAllDataFromPLC()
        {
            this.ConnectToPLC();

            List<PLCVariableConfig> allVariables = this.GetListOfPLCVariables();
            List<PLCData> allData = new List<PLCData>();
            
            foreach(PLCVariableConfig var in allVariables) 
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

            this.DisConnectFromPLC();
            return null;
        }

        public PLCData ReadIntFromPLC(PLCVariableConfig varConfig)
        {
            byte[] varAsByte = new byte[varConfig.variableLenght];
            int readStatus = plcConnection.readBytes(libnodave.daveDB, 1, varConfig.startByte, varConfig.variableLenght, varAsByte);

            if (readStatus != 0) throw new ApplicationException("Could not read Data");
            Array.Reverse(varAsByte);
            int varAsInt = BitConverter.ToInt32(varAsByte, 0);
            PLCData readedData = new PLCData(varConfig.plcID, varConfig.variableId, varConfig.type, varAsInt);

            return readedData;
        }
      
        public Byte[] ReadBitsFromPLC(PLCVariableConfig varConfig)
        {
            return null;
        }

        #endregion

        #region protectedFunctions
              

        protected override int GetNextReadingTime()
        {
            //TODO not implemented
            return 1;
        }

        #endregion

        #region Private Functions

        private List<PLCVariableConfig> GetListOfPLCVariables()
        {
            return null;
        }
        
        private PLCData ReadBitFromPLC(PLCVariableConfig varConfig)
        {
            return null;
        }

        private PLCData ReadFloatFromPLC(PLCVariableConfig varConfig)
        {
            return null;
        }      

        

        #endregion
    }
}
