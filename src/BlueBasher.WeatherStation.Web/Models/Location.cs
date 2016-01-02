namespace BlueBasher.WeatherStation.Web.Models
{
    public class Location
    {
        public string DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Pressure { get; set; }
        public double Temperature { get; set; }
    }
}
