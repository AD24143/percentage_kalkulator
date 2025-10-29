C:\path\to\repo\lab_3\Models\Container.cs
using System;

namespace WpfApp3.Models
{
    public class Container
    {
        public string Name { get; }
        public double VolumeMl { get; }

        public Container(string name, double volumeMl)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            VolumeMl = volumeMl;
        }

        public override string ToString() => Name;
    }
}