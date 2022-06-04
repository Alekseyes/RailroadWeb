using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadWeb.Entities
{
    
    public class Station
    {
        
        public string Name;

        public Station(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

    }
}
