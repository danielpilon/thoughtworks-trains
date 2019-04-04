﻿using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.Trips
{
    public interface ITrip
    {
        Town Origin { get; }

        IEnumerable<Route> Routes { get; }

        int TotalDistance { get; }

        int Stops { get; }
    }
}