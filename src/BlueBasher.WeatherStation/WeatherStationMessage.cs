using System;

namespace BlueBasher.WeatherStation
{
    internal class WeatherStationMessage
    {
        public string DeviceId { get; set; }
        public DateTime PreciseTime { get; set; }
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float Altitude { get; set; }
        public double Latitude{ get; set; }
        public double Longitude { get; set; }
    }
}
