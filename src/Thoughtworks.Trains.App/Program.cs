using System;
using System.Linq;
using Thoughtworks.Trains.App.Extensions;
using Thoughtworks.Trains.Application.Paths;
using Thoughtworks.Trains.Application.RailwaySystems;
using Thoughtworks.Trains.Application.Trips;
using Thoughtworks.Trains.Domain.Railway;
using Thoughtworks.Trains.Domain.Towns.Exceptions;

namespace Thoughtworks.Trains.App
{
    class Program
    {
        private const string SampleInput = "AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7";

        static void Main(string[] args)
        {
            var railwaySystemBuilder = new RailwaySystemBuilder();
            try
            {
                ConsoleWriter.WriteColoredLine("Welcome to Kiwiland Railway System!\n", ConsoleColor.Green);
                ConsoleWriter.WriteColoredLine("Select an option to continue:", ConsoleColor.Magenta);
                ConsoleWriter.WriteColoredLine($"1) Use sample input {SampleInput}.", ConsoleColor.Magenta);
                ConsoleWriter.WriteColoredLine("2) Use custom input.", ConsoleColor.Magenta);
                while (true)
                {
                    ConsoleWriter.WriteColored("\nEnter option: ", ConsoleColor.Yellow);
                    var option = Console.ReadLine();
                    if (option == "1")
                    {
                        var railwaySystem = railwaySystemBuilder.Build(SampleInput);
                        WriteOutput(railwaySystem);

                    }
                    else if (option == "2")
                    {
                        ConsoleWriter.WriteColoredLine("\nRoutes are represented by the origin and destination town, as well as the distance between them.", ConsoleColor.Yellow);
                        ConsoleWriter.WriteColoredLine("For example, a route between two towns (A to B) with a distance of 5 is represented as AB5.", ConsoleColor.Yellow);
                        ConsoleWriter.WriteColored("Please input a comma-separated set of routes: ", ConsoleColor.Yellow);
                        var railwaySystem = railwaySystemBuilder.Build(Console.ReadLine()?.Replace(" ", string.Empty));
                        WriteOutput(railwaySystem);
                    }
                    else
                    {
                        ConsoleWriter.WriteColoredLine($"{option} is not a valid option. Please try again.", ConsoleColor.Red);
                        continue;
                    }
                    break;
                }

            }
            catch (Exception e)
            {
                ConsoleWriter.WriteColoredLine($"An unexpected exception occurred: {e}", ConsoleColor.Red);
            }

            ConsoleWriter.WriteColoredLine("\nPress any key to close the program.", ConsoleColor.Yellow);
            Console.ReadKey();
        }

        private static void WriteOutput(RailwaySystem railwaySystem)
        {
            var tripService = new TripService();
            var pathBuilder = new PathBuilder();
            Console.WriteLine();
            SafeWriteOutput(1, () => tripService.ResolveDistance(pathBuilder.Build(railwaySystem, "A", "B", "C")).ToString());
            SafeWriteOutput(2, () => tripService.ResolveDistance(pathBuilder.Build(railwaySystem, "A", "D")).ToString());
            SafeWriteOutput(3, () => tripService.ResolveDistance(pathBuilder.Build(railwaySystem, "A", "D", "C")).ToString());
            SafeWriteOutput(4, () => tripService.ResolveDistance(pathBuilder.Build(railwaySystem, "A", "E", "B", "C", "D")).ToString());
            SafeWriteOutput(5, () => tripService.ResolveDistance(pathBuilder.Build(railwaySystem, "A", "E", "D")).ToString());
            Console.WriteLine($"Output #6: {tripService.Search(railwaySystem.GetTownByName("C"), railwaySystem.GetTownByName("C"), trip => trip.Stops > 3).Count()}");
            Console.WriteLine($"Output #7: {tripService.Search(railwaySystem.GetTownByName("A"), railwaySystem.GetTownByName("C"), trip => trip.Stops > 4, trip => trip.Stops == 4).Count()}");
            SafeWriteOutput(8, () => tripService.FindShortest(railwaySystem, railwaySystem.GetTownByName("A"), railwaySystem.GetTownByName("C")).TotalDistance.ToString());
            SafeWriteOutput(9, () => tripService.FindShortest(railwaySystem, railwaySystem.GetTownByName("B"), railwaySystem.GetTownByName("B")).TotalDistance.ToString());
            Console.WriteLine($"Output #10: {tripService.Search(railwaySystem.GetTownByName("C"), railwaySystem.GetTownByName("C"), trip => trip.TotalDistance >= 30).Count()}");
            Console.WriteLine();
        }

        private static void SafeWriteOutput(int outputIndex, Func<string> output)
        {
            try
            {
                Console.WriteLine($"Output #{outputIndex}: {output()}");
            }
            catch (InvalidRouteException)
            {
                Console.WriteLine($"Output #{outputIndex}: NO SUCH ROUTE");
            }
        }
    }
}