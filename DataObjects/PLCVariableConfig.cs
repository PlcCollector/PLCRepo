using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PLCVariableConfig
    {
        public String variableName { get; set; }
        public int variableId { get; set;  }
        public int dataBlockNr { get; set; }
        public int startByte { get; set; }
        public int startBit { get; set; }
        public int variableLenght { get; set; }
        public int plcID { get; set; }
        public String type { get; set; }
    }
}
