using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NGK_WeatherMeasurements.Models
{
    public class Location
    {
        [Key]
        public long MeasurementLocationId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonIgnore]
        public List<Measurement> MeasurementsList { get; set; }
    }
}
