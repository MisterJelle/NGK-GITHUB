using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using NGK_WeatherMeasurements.Controllers;
using NGK_WeatherMeasurements.Data;
using NGK_WeatherMeasurements.Hub;
using NGK_WeatherMeasurements.Models;
using Xunit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NGK_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanChangeMeasurementHumidity()
        {
            Measurement m = new Measurement
            {
                Humidity = 25
            };
            m.Humidity = 11;

            Assert.Equal(11, m.Humidity);
        }

        [Fact]
        public async System.Threading.Tasks.Task CanChangeMeasuremenatHumidityAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test")
                .Options;
            using var db = new ApplicationDbContext(options);
            Location l = new Location
            {
                Latitude = 12.2,
                Longitude = 192.1,
                Name = "test1"
            };
            LocationsController _uut = new LocationsController(db);
            await _uut.PostLocation(l);
            var data = await _uut.GetLocations();
            Assert.NotEmpty(data.Value);
        }

        [Fact]
        public async System.Threading.Tasks.Task CanChangeMeasuremenaatHumidityAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test")
                .Options;
            using var db = new ApplicationDbContext(options);
            Location l = new Location
            {
                Latitude = 12.2,
                Longitude = 192.1,
                Name = "test1",
                MeasurementLocationId = 1
            };
            LocationsController _uut = new LocationsController(db);
            await _uut.PostLocation(l);
            l.MeasurementLocationId = 2;
            await _uut.PostLocation(l);
            var data = await _uut.GetLocations();
            Assert.Equal(2, data.Value.Count());
        }


        //[Fact]
        //public async System.Threading.Tasks.Task CanChangeAsync()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase("test")
        //        .Options;
        //    using var db = new ApplicationDbContext(options);
        //    MeasurementHub hub = new MeasurementHub();
        //    MeasurementsController _uut = new MeasurementsController(db, hub);
        //    Measurement m = new Measurement
        //    {
        //        Temp = 12.5,
        //        Humidity = 45,
        //        Date = DateTime.Now,
        //        Pressure = 22
        //    };
        //    await _uut.PostMeasurement(m);
        //    var data = await _uut.GetIdSpecificMeasurement(m.MeasurementID);
        //    Assert.Equal(m, data.Value);
        //}
    }
}
