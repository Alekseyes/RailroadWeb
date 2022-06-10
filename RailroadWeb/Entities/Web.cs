using System.Collections.Generic;

namespace RailroadWeb.Entities
{
    public class Web
    {
     
        public Dictionary<Rail,int> RailRoads;

        public Web()
        {
            RailRoads = new Dictionary<Rail, int>();
        }
    }
}
