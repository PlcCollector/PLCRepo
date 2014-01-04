using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PLCConfig
    {
        public String plcName { get; set; }
        public int plcPort { get; set; }
        public String type { get; set; }
        public String ipAddress { get; set; }
        public int interval { get; set; }
        public int plcID { get; set; }
        public int refreshTime { get; set; }
        public int plcRack { get; set; }
        public int plcSlot { get; set; }

        public string ToErrorString()
        {
            return "Name: " + plcName.ToString() + "Type: " + type.ToString() + "IPAddr: " + ipAddress.ToString() + "ID: " + plcID.ToString() ;
        }
    }
}
