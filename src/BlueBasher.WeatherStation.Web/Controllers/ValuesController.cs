namespace BlueBasher.WeatherStation.Web.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Models;
    using Microsoft.WindowsAzure.Storage;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IConfigurationRoot _configuration;

        public ValuesController(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Location> Get()
        {
            var storageAccount = CloudStorageAccount.Parse(_configuration["Storage:ConnectionString"]);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("WeatherStationDevices");
            var devices =
                (from device in table.CreateQuery<WeatherStationDevice>()
                 where device.PartitionKey == "Location"
                 select device)
                .ToList();

            return devices.Select(d =>
                new Location
                {
                    DeviceId = d.RowKey,
                    Latitude = d.latitude,
                    Longitude = d.longitude,
                    Altitude = d.altitude,
                    Pressure = d.pressure,
                    Temperature = d.temperature
                });
        }
    }
}
