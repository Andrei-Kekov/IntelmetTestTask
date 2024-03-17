using Newtonsoft.Json;

namespace IntelmetTestTask
{
    public class DistanceEntry
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Time")]
        public DateTime Time { get; set; }

        [JsonProperty("ElapsedTime")]
        public int ElapsedTime { get; set; }

        [JsonProperty("Speed")]
        public double Speed { get; set; }

        [JsonProperty("SNR1")]
        public double Snr1 { get; set; }

        [JsonProperty("Distance")]
        public double Distance {  get; set; }
    }
}
