using System;

namespace Thoughtworks.Trains.Domain.Towns.Exceptions
{
    public class InvalidRouteException : Exception
    {
        public InvalidRouteException(string s) : base(s)
        {

        }
    }
}