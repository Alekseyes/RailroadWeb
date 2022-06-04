using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadWeb.Entities
{
    public class Web
    {
        public List<Station> Stations;

        //public List<RailRoad> RailRoads;

        public Dictionary<Rail,int> RailRoads;
        public Web()
        {
            Stations = new List<Station>();
            RailRoads = new Dictionary<Rail, int>();
        }

    }
}
