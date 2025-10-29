using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3
{
    public class Container
    {
        public string Name { get; set; } = string.Empty;
        public double VolumeMl { get; set; }

        public Container(string name, double volumeMl)
        {
            Name = name;
            VolumeMl = volumeMl;
        }

        public override string ToString()
        {
            if (VolumeMl > 0)
                return $"{Name} ({VolumeMl} ml)";
            return Name;
        }
    }
}
