using Newtonsoft.Json;

namespace PlcStarter.Model
{
    public class Logo
    {
        [JsonProperty("Source")]
        public string Source { get; set; }

        [JsonProperty("Destination")]
        public string Destination { get; set; }
    }
}