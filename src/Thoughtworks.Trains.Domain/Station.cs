using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Thoughtworks.Trains.Domain
{
    public class Station
    {
        public Station(string name)
        {
            Name = name;
        }

        private Dictionary<string, Destination> DestinationsByName { get; } = new Dictionary<string, Destination>();

        public string Name { get; }

        public IEnumerable<Destination> Destinations => DestinationsByName.Values;

        public void AddDestination(Destination destination) => DestinationsByName.Add(destination.To.Name, destination);

        public Destination GetDestinationByName(string name) =>
            DestinationsByName.ContainsKey(name) ? DestinationsByName[name] : throw new InvalidDestinationException($"There's no such route that passes via '{name}' station.");

        public bool HasDestination(string name) => DestinationsByName.ContainsKey(name);
    }

    public class InvalidDestinationException : Exception
    {
        public InvalidDestinationException(string s) : base(s)
        {

        }
    }
}