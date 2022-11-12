using System.ComponentModel.DataAnnotations.Schema;

namespace AirGradientWebApi.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public int Wifi { get; set; }
        public int? Co2 { get; set; }
        public int? Pm02 { get; set; }
        public decimal Temperature { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public int? Humidity { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
