using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailroadWeb.Entities
{
    public struct Track
    {
        public string Station1;
        public string Station2;
        //public int Distance;

        //public Rail(string station1,string station2,int distance)
        //{
        //    Station1 = station1;
        //    Station2 = station2;
        //  //  Distance = distance;
        //}

        public Track(string station1, string station2)
        {
            Station1 = station1;
            Station2 = station2;
           // Distance = 0;
        }


        //public override bool Equals(object obj)
        //{
        //    if(obj is Rail)
        //    {
        //        var t1 = (Rail)obj;
        //        return t1.Station1.Equals(Station1) && t1.Station2.Equals(Station2) || t1.Station1.Equals(Station2) && t1.Station2.Equals(Station1);

        //    }
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return Station1.GetHashCode() + Station2.GetHashCode();

        //    //return base.GetHashCode();
        //}

        public bool IsOpposite(Track anotherTrack)
        {
           return anotherTrack.Station1.Equals(Station2) && anotherTrack.Station2.Equals(Station1);
        }
        public Track GetOpposite()
        {
            return new Track(Station2, Station1);
        }

        public override string ToString()
        {
            return $"{Station1}-{Station2}";
        }


    }
}
