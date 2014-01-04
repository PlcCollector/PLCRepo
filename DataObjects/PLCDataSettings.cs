using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PLCDataSettings
    {
        public int settingsId { get; set; }
        public double lowValue { get; set; }
        public double highValue { get; set; }
        public String unit { get; set; }
    }
}
