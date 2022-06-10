using System;
using System.ComponentModel;


namespace RailroadWeb.Entities
{
    /// <summary>
    /// The class describes a rail with two stations.
    /// </summary>
    [TypeConverter(typeof(RailTypeConverter))]
    public struct  Rail
    {
        public string Station1;
        public string Station2;
        
        public Rail(string station1, string station2)
        {
            if(string.IsNullOrWhiteSpace(station1) || string.IsNullOrWhiteSpace(station2) || string.Equals(station1,station2,StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Arguments for Rail is invalid");
            }

            Station1 = station1;
            Station2 = station2;          
        }

        public override bool Equals(object obj)
        {
            if(obj is Rail)
            {
                var t1 = (Rail)obj;
                return t1.Station1.Equals(Station1,StringComparison.OrdinalIgnoreCase) && t1.Station2.Equals(Station2, StringComparison.OrdinalIgnoreCase) || t1.Station1.Equals(Station2, StringComparison.OrdinalIgnoreCase) && t1.Station2.Equals(Station1, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Station1.ToUpper().GetHashCode() + Station2.ToUpper().GetHashCode();

        }

        public override string ToString()
        {
            return $"{Station1}-{Station2}";
        }
    }
}
