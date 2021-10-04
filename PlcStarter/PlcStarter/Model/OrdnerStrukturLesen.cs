using Newtonsoft.Json;
using System;
using System.IO;

namespace PlcStarter.Model
{
    public class OrdnerStrukturLesen
    {
        public Ordner OrdnerStrukturen { get; set; }
        internal void GetOrdnerConfig(string pfad)
        {
            OrdnerStrukturen = JsonConvert.DeserializeObject<Ordner>(File.ReadAllText(pfad));
            if (OrdnerStrukturen != null && OrdnerStrukturen.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.DigitalTwin].Steuerung != "DigitalTwin") throw new Exception("Ordner in der falschen Reihenfolge !");
            if (OrdnerStrukturen != null && OrdnerStrukturen.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.FactoryIo].Steuerung != "FactoryIO") throw new Exception("Ordner in der falschen Reihenfolge !");
            if (OrdnerStrukturen != null && OrdnerStrukturen.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.Logo].Steuerung != "Logo") throw new Exception("Ordner in der falschen Reihenfolge !");
            if (OrdnerStrukturen != null && OrdnerStrukturen.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TiaPortal].Steuerung != "TiaPortal") throw new Exception("Ordner in der falschen Reihenfolge !");
            if (OrdnerStrukturen != null && OrdnerStrukturen.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TwinCat].Steuerung != "TwinCAT") throw new Exception("Ordner in der falschen Reihenfolge !");
        }
    }
}