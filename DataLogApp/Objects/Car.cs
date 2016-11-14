using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogApp.Objects
{
    public class Car
    {
        public int Year { get; set; }
        public RegionInfo Region { get; set; }
        public Modifications Mods { get; set; }
    }
}
