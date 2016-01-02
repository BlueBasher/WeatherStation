namespace BlueBasher.WeatherStation.Web.Models
{
    using Microsoft.WindowsAzure.Storage.Table;

    public class WeatherStationDevice : TableEntity
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public double pressure { get; set; }
        public double temperature { get; set; }
    }
}
