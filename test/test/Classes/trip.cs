using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Classes
{
    public class Trip
    {
        public Drone Drone { get; set; }
        public List<Delivery> Deliveries { get; set; }

        public string Locations
        {
            get
            {
                return string.Join(", ", Deliveries.Select(d => d.Location));
            }
        }




        public Trip(Drone drone)
        {
            Drone = drone;
            Deliveries = new List<Delivery>();
        }

        public Trip(Drone drone, List<Delivery> list)
        {
            Drone = drone;
            Deliveries = list;
        }
    }
}