using Newtonsoft.Json;

namespace PlcStarter.Model
{
    public partial class EinstellungenOrdnerLesen
    {
        [JsonProperty("DigitalTwin")]
        public Logo DigitalTwin { get; set; }

        [JsonProperty("Logo")]
        public Logo Logo { get; set; }

        [JsonProperty("TiaPortal")]
        public Logo TiaPortal { get; set; }

        [JsonProperty("TwinCAT")]
        public Logo TwinCat { get; set; }
    }
}