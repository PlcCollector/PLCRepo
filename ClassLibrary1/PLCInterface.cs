using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataObjects;

namespace PLCLayer
{
    public interface PLCInterface
    {
        PLCConfig GetPLCConfig();
        void ConnectToPLC();
        void DisConnectFromPLC();
        List<PLCData> GetAllDataFromPLC();
        int GetSampleIntervall();
       
        
    }
}
