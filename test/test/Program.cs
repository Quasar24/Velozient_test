using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using test;
using test.Classes;
using test.Helper;
using test.Logic;

class DroneDelivery
{

    static void Main(string[] args)
    {
        // Create a new list to store Drone objects
        var drones = new List<Drone>();
        // Create a new list to store Delivery objects
        var locations = new List<Delivery>();

        //read InputFile        
        TxtHandler.txtInput("..//..//..//input.txt", out drones, out locations);
        // Sort the drones by their maximum weight capacity
        drones.OrderBy(x => x.MaxWeight);
        // Sort the deliveries by their weight
        locations.OrderBy(x => x.Weight);
        // Call the GetOptimalSolution method, passing in the drones and locations lists
        DeliveryService.GetOptimalSolution(drones, locations);
    }




}



