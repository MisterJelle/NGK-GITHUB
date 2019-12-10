using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGK_WeatherMeasurements.Models
{
    public class MemoryRepository : IRepository
    {
        private Dictionary<long, Measurement> items;
        public MemoryRepository()
        {
            items = new Dictionary<long, Measurement>();
            //new List<Reservation> {
            //    new Reservation { Client = "Alice", Location = "Board Room" },
            //    new Reservation { Client = "Bob", Location = "Lecture Hall" },
            //    new Reservation { Client = "Joe", Location = "Meeting Room 1" }
            //    }.ForEach(r => AddReservation(r));
        }
        public Measurement this[long id] { get => items.ContainsKey(id) ? items[id] : null; }

        public IEnumerable<Measurement> Measurements { get => items.Values; }

        public Measurement AddMeasurement(Measurement measurement)
        {
            if (measurement.MeasurementID == 0)
            {
                long key = items.Count;
                while (items.ContainsKey(key)) { key++; };
                measurement.MeasurementID = key;
            }
            items[measurement.MeasurementID] = measurement;
            return measurement;
        }

        public void DeleteMeasurement(long id)
        {
            items.Remove(id);
        }

        public Measurement UpdateMeasurement(Measurement measurement)
        {
            return AddMeasurement(measurement);
        }
    }
}
