
# Velozient Drone Coding Test

# Tools
  
  Visual Studio 2022, Net6.0, Linq
  
# Summary

The Velozient Drone Coding Test is a coding test designed to evaluate a candidate's ability to solve a problem related to assigning deliveries to drones in an efficient and optimal way. The code is separated into three main steps

**Step 1: Reading the input file and generating the Drone and Location classes**

The first step is to read an input file that contains information about the drones and the deliveries that need to be made. This information is used to create instances of the Drone and Location classes, which store the relevant details about each drone and delivery.

  

**Step 2: Generating permutations and removing duplicates**

The second step is to generate all possible combinations of deliveries that a drone can carry, based on its maximum weight capacity. This is done by calling the GetPermutations method, which generates all possible permutations and removes any duplicates.

  

**Step 3: Finding the optimal solution**

The final step is to find the optimal solution for selecting a set of trips (combinations of deliveries) that will satisfy all the deliveries in the list of remaining deliveries. This is done by calling the OptimalSolution method, which uses a recursive approach to explore all possible combinations of trips and determine the optimal solution.

  

# Conclusion

The Velozient Drone Coding Test is a good example of how to solve the problem of assigning deliveries to drones in an efficient and optimal way. The code is well-organized, easy to understand and it's a good approach to solve the problem. It's worth noting that there are different approaches to solving this problem, such as using algorithms like the knapsack problem or the traveling salesman problem. However, these approaches can be much more complex and time-consuming to implement.

This solution is relatively simple and easy to understand, making it a good starting point for further development and improvement.  

Please note that the Velozient Drone Coding Test is a coding test, it's not a final product or tool ready for production use.
