using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.Trips
{
    public interface IPath
    {
        IEnumerable<Town> Towns { get; }
    }
}