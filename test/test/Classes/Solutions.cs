using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Classes;

namespace test
{
    public class Solutions
    {
        public Solutions(List<Trip> tripList)
        {
            TripList = tripList;
        }

        public List<Trip> TripList { get; set; }
    }
}
