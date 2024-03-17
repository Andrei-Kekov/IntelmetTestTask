using Newtonsoft.Json;

namespace IntelmetTestTask
{
    public class Measure
    {
        [JsonProperty("Distances")]
        public List<DistanceEntry> Distances { get; set; }
    }
}
