using System;

namespace Thoughtworks.Trains.Domain.Towns
{
    public struct Route
    {
        public Route(Town from, Town to, int distance)
        {
            Distance = distance > 0 ? distance : throw new ArgumentException($"Argument {nameof(distance)} value cannot be lower than 1.");
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
        }

        public int Distance { get; set; }

        public Town From { get; }

        public Town To { get; }
    }
}