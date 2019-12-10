using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NGK_WeatherMeasurements.Models
{
    public class Measurement
    {
        public long MeasurementID { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }
        public long LocationId { get; set; }
        public double Temp { get; set; }
        public int Humidity { get; set; }
        public double Pressure { get; set; }
    }
}
