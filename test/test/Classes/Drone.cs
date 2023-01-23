using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Classes;

namespace test
{
    public class Drone
    {
        public string Name { get; set; }
        public int MaxWeight { get; set; }
        public int CurrentWeight { get; set; }
        public HashSet<Delivery> Deliveries { get; set; }

        public Drone(string name, int maxWeight)
        {
            Name = name;
            MaxWeight = maxWeight;
            CurrentWeight = 0;
            Deliveries = new HashSet<Delivery>();
        }
    }
}
