using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadWeb.Entities
{
    public class Route
    {
        public List<string> Stations { get; set; }

        public Route()
        {
            Stations = new List<string>();
        }

       

    }
}
