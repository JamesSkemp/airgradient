namespace AirGradientWebApi.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public string Wifi { get; set; } = string.Empty;
        public string? Co2 { get; set; }
        public string? Pm02 { get; set; }
        public string Temperature { get; set; } = string.Empty;
        public string? Humidity { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
