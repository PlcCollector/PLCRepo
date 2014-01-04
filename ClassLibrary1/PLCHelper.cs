using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DBHandler;
using System.Windows;
using System.Windows.Input;


namespace PLCLayer
{
    public static class PLCHelper
    {
        public static List<PLCInterface> LoadAllPLCsFromDB() 
        {
            PLCDBHandler plcDBHandler = new PLCDBHandler();
           
            List<PLCConfig> listOfPLCConfigs = new List<PLCConfig>();
            List<PLCInterface> listOfPLCInterfaces = new List<PLCInterface>();

            listOfPLCConfigs = plcDBHandler.GetListOfPLCConfigurations();          

            foreach (PLCConfig plcConfig in listOfPLCConfigs)
            {
                listOfPLCInterfaces.Add(PLCHelper.LoadPLCByConfig(plcConfig));
            }

            return listOfPLCInterfaces;
        }

        public static PLCInterface LoadPLCByID(int plcID)
        {
            return null;
        }

        public static PLCInterface LoadPLCByConfig(PLCConfig plcConfig)
        {
            switch (plcConfig.type)
            {
                case "siemens300":
                    return new Siemens300seriesOverTCP(plcConfig);
                case "PLCSimulator":
                    return new PLCSimualtor(plcConfig);
                default:
                    throw new ArgumentException("sadf", "asdf");
            }
        }
                
    }
}
