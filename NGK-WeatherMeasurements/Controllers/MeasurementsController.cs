using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NGK_WeatherMeasurements.Data;
using NGK_WeatherMeasurements.Hub;
using NGK_WeatherMeasurements.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NGK_WeatherMeasurements.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeasurementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<MeasurementHub> _measurementHub;

        //public MeasurementsController(ApplicationDbContext context, IHubContext<MeasurementHub> measurementHub)
        //{
        //    _measurementHub = measurementHub;
        //    _context = context;
        //}

        public MeasurementsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Measurements
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetMeasurements()
        {
            return await _context.Measurements.ToListAsync();
        }

        // GET: api/Measurements/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Measurement>> GetIdSpecificMeasurement(long id)
        {
            var measurement = await _context.Measurements.FindAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }

            return measurement;
        }

        // PUT: api/Measurements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurement(long id, Measurement measurement)
        {
            if (id != measurement.MeasurementID)
            {
                return BadRequest();
            }

            _context.Entry(measurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasurementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await _measurementHub.Clients.All.SendAsync("SendMeasurement");
            return NoContent();
        }

        // POST: api/Measurements
        [HttpPost]
        public async Task<ActionResult<Measurement>> PostMeasurement(Measurement measurement)
        {
            measurement.Date = DateTime.Now;
            _context.Measurements.Add(measurement);
            //var location = await _context.Locations.Where(l => l.MeasurementLocationId == measurement.LocationId).Take(1).ToListAsync();
            //location[0].MeasurementsList.Add(measurement);
            //_context.Locations.Update(location[0]);
            await _context.SaveChangesAsync();
            //await _measurementHub.Clients.All.SendAsync("SendMeasurement");
            return CreatedAtAction("GetMeasurements", new { id = measurement.MeasurementID }, measurement);
        }

        // POST: api/Measurements
        [HttpPost("{Backdate}")]
        public async Task<ActionResult<Measurement>> PostBackdatedMeasurement(Measurement measurement)
        {
            //DateTime s = DateTime.Now ;
            //s = new DateTime(2019, month, day, 2,2,2);
            //measurement.Date = s;

            _context.Measurements.Add(measurement);
            //var location = await _context.Locations.Where(l => l.MeasurementLocationId == measurement.LocationId).Take(1).ToListAsync();
            //location[0].MeasurementsList.Add(measurement);
            //_context.Locations.Update(location[0]);
            await _context.SaveChangesAsync();

            await _measurementHub.Clients.All.SendAsync("SendMeasurement");
            return CreatedAtAction("GetMeasurements", new { id = measurement.MeasurementID }, measurement);
        }

        // DELETE: api/Measurements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Measurement>> DeleteMeasurement(long id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();

            return measurement;
        }

        private bool MeasurementExists(long id)
        {
            return _context.Measurements.Any(e => e.MeasurementID == id);
        }


        [HttpGet("DateRange/{startDate}/{endDate}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Measurement>>> GetDateRangeMeasurement(string startDate, string endDate)
        {
            DateTime obj1 =  DateTime.ParseExact(startDate, "yy-MM-dd", CultureInfo.InvariantCulture);
            DateTime obj2 = DateTime.ParseExact(endDate, "yy-MM-dd", CultureInfo.InvariantCulture);


            var measurementList = await _context.Measurements.Where(m => m.Date >= obj1 && m.Date <= obj2).ToListAsync();

            if (measurementList == null)
            {
                return NotFound();
            }

            return measurementList;
        }

        [HttpGet("DateSpecific/{date}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Measurement>>> GetDateSpecificMeasurement(DateTime date)
        
        {
            //var date = DateTime.ParseExact(dateSpecific, "yyyy-MM-dd", CultureInfo.InvariantCulture);


            var measurement = await _context.Measurements.Where(m => m.Date == date).ToListAsync();

            if (measurement == null)
            {
                return NotFound();
            }
            await _measurementHub.Clients.All.SendAsync("SendMeasurement");
            return measurement;
        }

        [HttpGet("Recent")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetRecentMeasurement()
        {
            var measurement = await _context.Measurements.OrderByDescending(m => m.Date).Take(3).ToListAsync();

            if (measurement == null)
            {
                return NotFound();
            }

            return measurement;
        }


    }
}
