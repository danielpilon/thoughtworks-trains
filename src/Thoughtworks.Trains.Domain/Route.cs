namespace Thoughtworks.Trains.Domain
{
    public struct Route
    {
        public Route(Town to, int distance)
        {
            To = to;
            Distance = distance;
        }

        public int Distance { get; set; }

        public Town To { get; }
    }
}