using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NGK_WeatherMeasurements.Models;


namespace NGK_WeatherMeasurements.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }



}
    

