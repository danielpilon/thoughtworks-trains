using System.Collections.Generic;
using Thoughtworks.Trains.Domain.Towns;

namespace Thoughtworks.Trains.Application.Paths
{
    public interface IPath
    {
        IEnumerable<Town> Towns { get; }
    }
}