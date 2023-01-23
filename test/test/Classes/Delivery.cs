using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Classes
{
    public class Delivery
    {
        public string Location { get; set; }
        public int Weight { get; set; }

        public Delivery(string location, int weight)
        {
            Location = location;
            Weight = weight;
        }
    }
}
