using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RailroadWeb.Entities;

namespace RailroadWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Random();
            var alphabet = new List<char>()
            {
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o'
            };
            var stations1 = new List<string>()
            {
            "ПЕТРОЗАВОДСК",
            "ТОМИЦЫ",
            "ШУЙСКАЯ",
            "СУНА",
            "ЗАДЕЛЬЕ",
            "КОНДОПОГА",
            "КЕДРОЗЕРО",
            "НОВЫЙ ПОСЕЛОК",
            "НИГОЗЕРО",
            "КЯППЕСЕЛЬГА",
            "ПЕРГУБА",
            "МЕДВЕЖ ГОРА",
            "ВИЧКА",
            "СЕГЕЖА",
            "НАДВОИЦЫ",
            "КОЧКОМА",
            "ИДЕЛЬ",
            "ЛЕТНИЙ",
            "СОСНОВЕЦ",
            "СУМСКИЙ ПОСАД"
            };

            //for (int i = 0; i < 100; i++)
            //{
            //    t1.Next(0, 19);   
            //}
            var web = new Web();
            foreach (var item in stations1)
            {
                web.Stations.Add(new Station(item));

            }
            foreach (var station in web.Stations)
            {
                foreach (var anotherStation in web.Stations)
                {
                    if(station.Equals(anotherStation))
                    {
                        continue;
                    }
                    //web.RailRoads.Add(new RailRoad(station, anotherStation, t1.Next(2, 100)));
                    web.RailRoads[new Rail(station.Name, anotherStation.Name)] = t1.Next(2, 100);
                }
            }

            //web.Stations.AddRange();
            var jsonObject = JsonConvert.SerializeObject(web);
            //var t1 = new JsonTextReader();
            var timeRails = new Dictionary<Track, List<Tuple<int,int>>>();
            //timeRails[new Rail("A", "B", 30)] = new List<Tuple<int, int>>();
            //timeRails[new Rail("B", "A", 15)] = new List<Tuple<int, int>>() {new Tuple<int, int>(8,10) };
            var timeStations = new Dictionary<string, List<int>>();
            timeStations["Петрозаводск"] = new List<int>();
            Route route;
            //using (var jsonRoute = File.OpenRead("Routes\\RouteTest1.json"))
            using (var stream = new StreamReader("Routes\\RouteTest1.json",Encoding.Default))
            {
               var text = stream.ReadToEnd();
                
                route = JsonConvert.DeserializeObject<Route>(text);
            };

            var time = 0;
            for (int i = 0; i < route.Stations.Count(); i++)
            {
                if(i>0)
                {
                    var tripTime = web.RailRoads[new Rail(route.Stations[i - 1], route.Stations[i])];
                    var tripTrack = new Track(route.Stations[i - 1], route.Stations[i]);
                    if (!timeRails.ContainsKey(tripTrack))
                    {
                        timeRails[tripTrack] = new List<Tuple<int, int>>();
                        
                    }
                    timeRails[tripTrack].Add(new Tuple<int, int>(time, time + tripTime));
                    
                    if(timeRails.TryGetValue(tripTrack.GetOpposite(), out var oppositeTrackTime))
                    {
                        var trackTime = timeRails[tripTrack].Last();
                        foreach (var oppositeTrack in oppositeTrackTime)
                        {
                            if((oppositeTrack.Item1 <= trackTime.Item2 && oppositeTrack.Item1 >=trackTime.Item1) || (oppositeTrack.Item2 <= trackTime.Item2 && oppositeTrack.Item2 >= trackTime.Item1))
                            {
                                Console.WriteLine("Столкновение!");
                            }
                        } 
                    }

                    time += tripTime;
                }

               if(timeStations.TryGetValue(route.Stations[i], out var timesOfStation))
                {
                    timesOfStation.Add(time);
                    if (timesOfStation.Contains(time))
                    {
                        Console.WriteLine($"Столкновение на станции! Станция - {route.Stations[i]}");
                    }
                }
                else
                {
                    timeStations[route.Stations[i]] = new List<int>() {time};
                }
            }
            // route.Stations
                       
                
                //Console.WriteLine(jsonObject);



            Console.ReadLine();
        }
    }
}
