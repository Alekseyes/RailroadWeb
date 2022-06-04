using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadWeb.Entities
{
    public class RailRoad
    {
        public int Distance { get; private set; }

        public Station Station1 { get; private set; }

        public Station Station2 { get; private set; }

        public RailRoad(Station station1, Station station2, int distance)
        {
            Station1 = station1;
            Station2 = station2;
            Distance = distance;
        }

    }
}
