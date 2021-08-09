using Newtonsoft.Json;
using System;
using System.IO;

namespace PlcStarter.Model
{
    public class OrdnerStrukturLesen
    {
        public Ordner OrdnerConfig { get; set; }
        internal void GetOrdnerConfig(string pfad)
        {
            OrdnerConfig = JsonConvert.DeserializeObject<Ordner>(File.ReadAllText(pfad));
            if (OrdnerConfig != null && OrdnerConfig.OrdnerBezeichnungen[0].Steuerung != "DigitalTwin") throw new Exception("Ordner in der falschen Reihenfolge !");
            if (OrdnerConfig != null && OrdnerConfig.OrdnerBezeichnungen[1].Steuerung != "FactoryIO") throw new Exception("Ordner in der falschen Reihenfolge !");
            if (OrdnerConfig != null && OrdnerConfig.OrdnerBezeichnungen[2].Steuerung != "Logo") throw new Exception("Ordner in der falschen Reihenfolge !");
            if (OrdnerConfig != null && OrdnerConfig.OrdnerBezeichnungen[3].Steuerung != "TIAPortal") throw new Exception("Ordner in der falschen Reihenfolge !");
            if (OrdnerConfig != null && OrdnerConfig.OrdnerBezeichnungen[4].Steuerung != "TwinCAT") throw new Exception("Ordner in der falschen Reihenfolge !");
        }
    }
}