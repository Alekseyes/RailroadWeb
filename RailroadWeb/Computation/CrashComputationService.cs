using RailroadWeb.Entities;
using RailroadWeb.Enum;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RailroadWeb.Computation
{
    /// <summary>
    /// Service for computation of crash
    /// </summary>
    public class CrashComputationService
    {
      
        private Web WebForComputation { get; set; }

        /// <summary>
        /// The dictionary contains intervals of time, which spends for tracks
        /// </summary>
        private Dictionary<Track, List<Tuple<int, int>>> TimeRails { get; set; }
        
        /// <summary>
        /// Time of object (train) staying
        /// </summary>
        private Dictionary<string, List<int>> TimeStations { get; set; }

        public CrashComputationService(Web web)
        {
            WebForComputation = web;
            TimeRails = new Dictionary<Track, List<Tuple<int, int>>>();
            TimeStations = new Dictionary<string, List<int>>();
        }

        /// <summary>
        /// Compute the possibility of crash for the route and adding time of trip for the next routes computation
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public ComputationResponse ComputeForRoute(Route route)
        {
            var time = 0;

            if(route.Stations.Count <= 1)
            {
                return ComputationResponse.InvalidInputData;
            }

            for (int i = 0; i < route.Stations.Count(); i++)
            {
                if (String.IsNullOrWhiteSpace(route.Stations[i]))
                {
                    return ComputationResponse.InvalidInputData;
                }
                if (i > 0)
                {
                    if (!WebForComputation.RailRoads.TryGetValue(new Rail(route.Stations[i - 1], route.Stations[i]), out var tripTime))
                    {
                        return ComputationResponse.InvalidInputData;                                           
                    }

                    if(ComputeCrashForTrackOfRoute(route.Stations[i - 1], route.Stations[i], time, time + tripTime))
                    {
                        return ComputationResponse.Crash;
                    }
                    time += tripTime;                   
                }
            }
            return ComputationResponse.Success;
        }

        private bool ComputeCrashForTrackOfRoute(string startStation, string finishStation, int startTime, int finishTime)
        {            
            lock (this)
            {
                var tripTrack = new Track(startStation, finishStation);
                AddTimeForTrack(tripTrack, startTime, finishTime);

                return CheckCrashForTrack(tripTrack) || AddTimeForStation(finishStation, finishTime);              
            }
        }

        private void AddTimeForTrack(Track tripTrack,int startTime,int finishTime)
        {
            if (!TimeRails.ContainsKey(tripTrack))
            {
                TimeRails[tripTrack] = new List<Tuple<int, int>>();
            }
            TimeRails[tripTrack].Add(new Tuple<int, int>(startTime, finishTime));
        }

        private bool CheckCrashForTrack(Track tripTrack)
        {
            if (TimeRails.TryGetValue(tripTrack.GetOpposite(), out var oppositeTrackTime))
            {
                var trackTime = TimeRails[tripTrack].Last();
                foreach (var oppositeTrack in oppositeTrackTime)
                {
                    if ((oppositeTrack.Item1 <= trackTime.Item2 && oppositeTrack.Item1 >= trackTime.Item1) 
                        || (oppositeTrack.Item2 <= trackTime.Item2 && oppositeTrack.Item2 >= trackTime.Item1))
                    {
                        return true; // It is Crash!
                    }
                }
            }
            return false;
        }

        private bool AddTimeForStation(string finishStation,int timeOfStation)
        {
            if (TimeStations.TryGetValue(finishStation, out var timesOfStation))
            {
                if (timesOfStation.Contains(timeOfStation))
                {
                    return true; // It is Crash!
                    
                }
                timesOfStation.Add(timeOfStation);
            }
            else
            {
                TimeStations[finishStation] = new List<int>() { timeOfStation };
            }
            return false;
        }
    }
}
