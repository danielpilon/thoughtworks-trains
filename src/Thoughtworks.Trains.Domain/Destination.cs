namespace Thoughtworks.Trains.Domain
{
    public struct Destination
    {
        public Destination(Station to, int distance)
        {
            To = to;
            Distance = distance;
        }

        public int Distance { get; set; }

        public Station To { get; }
    }
}