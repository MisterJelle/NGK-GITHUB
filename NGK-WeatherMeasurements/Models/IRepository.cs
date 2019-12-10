using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGK_WeatherMeasurements.Models
{
    interface IRepository
    {
        IEnumerable<Measurement> Measurements { get; }
        Measurement this[long id] { get; }
        Measurement AddMeasurement(Measurement measurement);
        Measurement UpdateMeasurement(Measurement measurement);
        void DeleteMeasurement(long id);
    }
}
