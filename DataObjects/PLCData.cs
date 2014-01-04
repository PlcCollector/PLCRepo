using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PLCData
    {
        public int plcID { get; set; }
        public int variableId { get; set; }
        public string timestamp { get; set; }
        public string dataType { get; set; }
        public int dataMeterInfo { get; set;}
        public int settingId { get; set; }  //connection to the settings Table in the DB
        public int intVal { get; set; }
        public float floatVal { get; set; }
        public bool bitVal { get; set; }

        public PLCData(int plcID, int variableId, string dataType, int intVal)
        {
            this.plcID = plcID;
            this.variableId = variableId;            
            this.dataType = dataType;

            DateTime currentTime = new DateTime();
            this.timestamp = currentTime.ToString("yyyyMMddHHmmssffff");

            this.intVal = intVal;
        }

        public PLCData(int plcID, int variableId, string dataType, bool bitVal)
        {
            this.plcID = plcID;
            this.variableId = variableId;
            this.dataType = dataType;

            DateTime currentTime = new DateTime();
            this.timestamp = currentTime.ToString("yyyyMMddHHmmssffff");

            this.bitVal = bitVal;
        }

        public PLCData(int plcID, int variableId, string dataType, float floatVal)
        {
            this.plcID = plcID;
            this.variableId = variableId;
            this.dataType = dataType;

            DateTime currentTime = new DateTime();
            this.timestamp = currentTime.ToString("yyyyMMddHHmmssffff");

            this.floatVal = floatVal;
        }
    }
}
