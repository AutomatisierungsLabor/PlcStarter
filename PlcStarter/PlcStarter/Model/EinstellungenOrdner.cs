using Newtonsoft.Json;

namespace PlcStarter.Model
{
    // https://app.quicktype.io/#l=cs&r=json2csharp

    public partial class EinstellungenOrdnerLesen
    {
        public static EinstellungenOrdnerLesen FromJson(string json) => JsonConvert.DeserializeObject<EinstellungenOrdnerLesen>(json, Converter.Settings);
    }
}
