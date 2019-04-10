# Thoughtworks-Trains

This project is a solution for the Trains coding challenge, proposed by the Thoughtworks hiring process.

# Table of Contents
1. [Installing .NET Core](#netcore)
2. [Running the application](#running)
3. [Running the tests](#tests)
4. [Solution overview](#overview)
5. [Next steps](#next)
6. [Evidences](#evidences)

## Installing .NET Core <a name="netcore"></a>

The project was developed using .NET Core 2.2.0, SDK version 2.2.1.

To install .NET Core on the Linux distribution of your choice, please refer to the [installation guide](https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-2.2.101).

To install .NET Core on MacOS, the installer can be downloaded [here](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-macos-x64-installer).

To install .NET Core on Windows, the installer can be downloaded [here](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.101-windows-x64-installer).

## Running the application <a name="running"></a>

At the root folder of the project, navigate to /src/Thoughtworks.Trains.App/ and run the following command:

```
dotnet restore
```

This will restore any missing dependency for the application to startup correctly.

After the previous command finishes, run the following:

```
dotnet run
```

If everything went well, the console will output the instructions to start using the application, just like the box below:

```
Welcome to Kiwiland Railway System!

Select an option to continue:
1) Use sample input AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7.
2) Use custom input.

Enter option:
```

The option 1 will continue with the sample data as given in the exercise instructions (AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7).

The option 2 will ask an input in the same format as the sample input, a comma-separated list of values determining the routes.

And that's it, you are ready to go.

## Running the tests <a name="tests"></a>

The tests are separated in two different projects, one for the Domain, and other for the Application tests. Both are located under the /tests/ folder.

To execute the tests for both projects, the following command should be executed on the root folder of the project:

```
dotnet test Thoughtworks.Trains.sln
```

## Solution overview <a name="overview"></a>

### Architecture

The project was designed using some of the DDD concepts. The Thoughtworks.Trains.Domain assembly holds the model based in the business description of the problem.

The Thoughtworks.Trains.Application assembly provides services that allows the usage of the domain model to define the Trips. It's separated by the Railway System and the Towns. This model was created using the [Graph Theory](https://en.wikipedia.org/wiki/Graph_theory). The Railway System is the graph itself, holding all the vertices, that are represented by the Towns. The towns holds the routes to other towns, using the concepts of an adjacency list for that.

### Algorithms

To solve the proposed problem, the application would need to be able to provide three different types of information out of the Railway System:

1. **The distance for a given route**: Given no discovery was required to determine the distance, a simple [Linear Search](https://en.wikipedia.org/wiki/Linear_search) on the Towns adjacent Routes was enough to determine the total distance. The edge case of the route not being valid was covered in this case and an exception is thrown when it happens.
2. **The number of different routes between two towns**: To solve this problem, it was considered both [DFS](https://en.wikipedia.org/wiki/Depth-first_search) and [BFS](https://en.wikipedia.org/wiki/Breadth-first_search). Since all the towns are due to be visited as deep as the route is until a given condition is met, DFS was the chosen one. Clients are required to provide a stop condition in order for the search to stop, and optionally can include another condition to consider the trip as valid.
3. **The shortest route between two towns**: This problem was solved based on the [Dijkstra's algorithm](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm). This is one of the most famous and used algorithms to find shortest path in a graph, even considering a weighted one. For this problem, the same edge case of a trip not being possible to be determined exists and it was handled using an specific exception as well.

All of those are covered by automated tests, created following a TDD approach.

## Next steps <a name="next"></a>

The biggest caveat that remained was on the Dijkstra's algorithm implementation. .NET doesn't provide a default implementation for Binary Heap (like the PriorityQueue in Java, or heapq in Python), and this would be way to go avoid sorting for every distance check. Even though the OrderBy function implements an stable Quick Sort, which is efficient, for a real production application, a Binary Heap would be much more.

The application doesn't implement any dependency injection concept, given the restrictions to not use any extra library besides the test containers. However, the immutability and the encapsulation are setup in a way that makes it easier to consider one in the future.

## Evidences <a name="evidences"></a>

The box below displays an evidence that, for the sample input provided, the application displayed the outputs as expected:

```
Welcome to Kiwiland Railway System!

Select an option to continue:
1) Use sample input AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7.
2) Use custom input.

Enter option: 1

Output #1: 9
Output #2: 5
Output #3: 13
Output #4: 22
Output #5: NO SUCH ROUTE
Output #6: 2
Output #7: 3
Output #8: 9
Output #9: 9
Output #10: 7

Press any key to close the program.
```