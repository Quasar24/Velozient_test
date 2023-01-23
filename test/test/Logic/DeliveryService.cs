using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Classes;

namespace test.Logic
{
    public static class DeliveryService
    {
        public static List<Solutions> solutions = new List<Solutions>();
        static List<Trip> completeList = new List<Trip>();

        /// <summary>
        /// Finds an optimal solution for assigning the deliveries to the drones in order to make the most deliveries possible.
        /// </summary>
        /// <param name="drones">List of available drones</param>
        /// <param name="locations">List of delivery locations</param>
        public static void GetOptimalSolution(List<Drone> drones, List<Delivery> locations)
        {
            // Iterate through the list of drones
            foreach (var drone in drones)
            {
                // Create a new list of possible packages (deliveries) that the drone can carry based on its maximum weight capacity
                List<Delivery> posiblePackages = locations.Where(x => x.Weight <= drone.MaxWeight).ToList();
                // Call the GetPermutations method, passing in the current drone, the list of possible packages, and an empty list
                GetPermutations(drone, posiblePackages, new List<Delivery>());
            }
            // Assign the completeList to a variable
            var a = completeList;

            // Sort the permutations by location
            for (var i = 0; i < completeList.Count; i++)
            {
                List<Delivery> deliveries = completeList[i].Deliveries.OrderBy(x => x.Location).ToList();
                completeList[i].Deliveries = deliveries;
            }

            // Remove any duplicate permutations and sort the result by descending order of deliveries count and descending order of drone's max weight
            var distinctTrips = RemoveDuplicates(completeList).OrderByDescending(x => x.Deliveries.Count).OrderByDescending(x => x.Drone.MaxWeight).ToList();

            // Call the OptimalSolution method, passing in the distinctTrips, an empty list, and the list of locations
            OptimalSolution(distinctTrips, new List<Trip>(), locations);


            // Print the optimal solution by displaying the name of the drone and the details of each trip assigned to that drone
            foreach (var trip in solutions.OrderBy(x => x.TripList.Count()).First().TripList.GroupBy(x => x.Drone))
            {
                Console.WriteLine(trip.Key.Name);
                for (int i = 0; i < trip.Count(); i++)
                {
                    Console.WriteLine("Trip #" + (i + 1));
                    Console.WriteLine(trip.ElementAt(i).Locations);
                }
            }
        }

       
        /// <summary>
        /// Generates all possible permutations of deliveries that a drone can carry
        /// </summary>
        /// <param name="drone">The current drone</param>
        /// <param name="deliveries">The list of available deliveries</param>
        /// <param name="currentPermutation">The current permutation being built</param>
        static void GetPermutations(Drone drone, List<Delivery> deliveries, List<Delivery> currentPermutation)
        {
            // Base case: if there are no more deliveries, add the current permutation to the completeList and return
            if (deliveries.Count == 0)
            {
                var trip = new Trip(drone);
                trip.Deliveries = currentPermutation;
                completeList.Add(trip);
                return;
            }
            else
            {
                // If the current permutation is valid (not exceeding the max weight of the drone) add it to the completeList
                if (currentPermutation.Sum(d => d.Weight) <= drone.MaxWeight)
                {
                    var trip = new Trip(drone);
                    trip.Deliveries = currentPermutation;
                    if (currentPermutation.Count != 0)
                        completeList.Add(trip);

                }
                else
                {
                    return;
                }
                // Iterate through the list of deliveries
                for (int i = 0; i < deliveries.Count; i++)
                {
                    var delivery = deliveries[i];
                    // Create a new list of remaining deliveries that excludes the current delivery
                    var remainingDeliveries = deliveries.Where((val, idx) => idx != i).ToList();
                    // Create a new list for the new permutation
                    var newPermutation = new List<Delivery>(currentPermutation);
                    // Add the current delivery to the new permutation
                    newPermutation.Add(delivery);
                    // Recursively call the GetPermutations method if the new permutation does not exceed the max weight
                    if (currentPermutation.Sum(d => d.Weight) + delivery.Weight <= drone.MaxWeight)
                    {
                        GetPermutations(drone, remainingDeliveries, newPermutation);
                    }
                }
            }
        }


        /// <summary>
        /// Removes duplicate trips from the input list of trips
        /// </summary>
        /// <param name="trips">List of trips</param>
        /// <returns>List of distinct trips</returns>
        static List<Trip> RemoveDuplicates(List<Trip> trips)
        {
            // Use LINQ to group the trips by drone name and trip locations,
            // then select the first trip in each group (which will be the distinct trip)
            // and return the result as a list
            return trips.GroupBy(x => new { x.Drone.Name, x.Locations })
                .Select(g => g.First())
                .ToList();
        }

        /// <summary>
        /// Finds an optimal solution for selecting a set of trips that will satisfy all the deliveries in the remainingDeliveries list.
        /// The selected trips will be added to the selectedTrips list.
        /// </summary>
        /// <param name="allTrips">List of all available trips</param>
        /// <param name="selectedTrips">List to store the selected trips</param>
        /// <param name="remainingDeliveries">List of deliveries that have not been assigned to a trip yet</param>
        static void OptimalSolution(List<Trip> allTrips, List<Trip> selectedTrips, List<Delivery> remainingDeliveries)
        {
            // Create a new list of remainingTrips as a copy of allTrips
            List<Trip> remainingTrips = new List<Trip>(allTrips);

            // Create a new list of tempSelectedTrips as a copy of selectedTrips
            List<Trip> tempSelectedTrips = new List<Trip>(selectedTrips);

            // Base case: if there are no more remaining deliveries, add the current tempSelectedTrips to the solutions list and return
            if (remainingDeliveries.Count() == 0)
            {
                solutions.Add(new Solutions(tempSelectedTrips)); return;
            }

            // Base case: if there are no more trips, return
            if (allTrips.Count == 0) { return; }

            // Create a new list to store the tempDeliveries
            List<Delivery> tempDeliveries = new List<Delivery>();

            // Iterate through all available trips
            foreach (var trip in allTrips)
            {
                // Create a new list of availableNewLocations as a copy of remainingDeliveries
                List<Delivery> availableNewLocations = new List<Delivery>(remainingDeliveries);

                // Check if any of the remaining deliveries match any of the deliveries in the current trip
                if (remainingDeliveries.Any(x => trip.Deliveries.Any(y => y.Location == x.Location)))
                {

                    // If there is a match, add the current trip to the tempSelectedTrips list
                    tempSelectedTrips.Add(trip);

                    // Iterate through the deliveries in the current trip
                    foreach (var location in trip.Deliveries)
                    {

                        // Remove the current location from the availableNewLocations list
                        availableNewLocations.Remove(location);

                        // Add the current location to the tempDeliveries list
                        tempDeliveries.Add(location);

                        // Remove all trips that contain the current location
                        remainingTrips.RemoveAll(x => x.Deliveries.Any(y => y.Location == location.Location));
                    }
                    // Recursively call the optimalSolution method with the updated remainingTrips, tempSelectedTrips, and availableNewLocations lists
                    OptimalSolution(remainingTrips, tempSelectedTrips, availableNewLocations);
                }
                // Remove the current trip from the tempSelectedTrips list
                tempSelectedTrips.Remove(trip);
            }
            return;
        }
    }
}
