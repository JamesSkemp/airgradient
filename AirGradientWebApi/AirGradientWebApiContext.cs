#nullable disable
using AirGradientWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AirGradientWebApi {
    public class AirGradientWebApiContext : DbContext
    {
        public AirGradientWebApiContext(DbContextOptions<AirGradientWebApiContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Device { get; set; }
        public DbSet<Measurement> Measurement { get; set; }
    }
}
