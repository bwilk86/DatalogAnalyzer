using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogApp.Objects
{
    public class Modifications
    {
        public bool BlackHaloCoils { get; set; }
        public bool CatalyticConverterDelet { get; set; }
        public bool AirPumpDelete { get; set; }
        public bool ForcedInduction { get; set; }
        public bool Differential { get; set; }
        public int Series { get; set; }
        public bool Fan { get; set; }
        public bool Transmission { get; set; }
    }
}
