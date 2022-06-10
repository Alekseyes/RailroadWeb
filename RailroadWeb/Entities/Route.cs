using System.Collections.Generic;

namespace RailroadWeb.Entities
{

    public class Route
    {
        public List<string> Stations { get; set; }

        public Route()
        {
            Stations = new List<string>();
        }

        /// <summary>
        /// The constructor for testing purpose
        /// </summary>
        /// <param name="stations"></param>
        public Route(params string[] stations)
        {
            Stations = new List<string>(stations);
        }
    }
}
