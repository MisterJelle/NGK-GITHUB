using Microsoft.EntityFrameworkCore;
using System;
using NGK_WeatherMeasurements.Data;
using NGK_WeatherMeasurements.Models;
using Xunit;

namespace NGK_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanChange()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            using var db = new ApplicationDbContext(options);
            Measurement m = new Measurement
            {
                Humidity = 25
            };
            m.Humidity = 11;

            Assert.Equal(11, m.Humidity);
        }
    }
}
