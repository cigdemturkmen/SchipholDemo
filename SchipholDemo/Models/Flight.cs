using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchipholDemo.Models
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public string Gate { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleTime { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string FlightDirection { get; set; }
        public string FlightName { get; set; }
        public string Id { get; set; }
        public string MainFlight { get; set; }
        public string PrefixIATA { get; set; }
        public string PrefixICAO { get; set; }
        public int AirlineCode { get; set; }
        public Route Route { get; set; }
    }
}
