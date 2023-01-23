using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Classes;

namespace test.Helper
{
    public static class TxtHandler
    {
        /// <summary>
        /// read inputfile and return drones and locations
        /// </summary>
        /// <param name="input"></param>
        /// <param name="drones"></param>
        /// <param name="locations"></param>
        public static void txtInput(string input, out List<Drone> drones, out List<Delivery> locations)
        {
            string[] lines = File.ReadAllLines(input);
            drones = new List<Drone>();
            locations = new List<Delivery>();

            // Parse input data and create Drone and Location objects
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (i == 0)
                {
                    // Create Drone objects
                    for (int j = 0; j < parts.Length; j += 2)
                    {
                        string value = parts[j + 1].Replace('[', ' ').Replace(']', ' ').Trim();
                        Drone drone = new Drone(parts[j].Trim(), int.Parse(value));

                        drone.Deliveries = new HashSet<Delivery>();
                        drone.CurrentWeight = 0;
                        drones.Add(drone);
                    }
                }
                else
                {
                    // Create Location objects
                    Delivery location = new Delivery(parts[0].Trim(), int.Parse(parts[1].Replace('[', ' ').Replace(']', ' ').Trim()));
                    locations.Add(location);
                }
            }
        }
    }
}
