using System.Collections.ObjectModel;

namespace PlcStarter.Model
{
    public class Ordner
    {
        public Ordner(ObservableCollection<OrdnerDaten> ordnerBezeichnungen)
        {
            OrdnerBezeichnungen = ordnerBezeichnungen;
        }

        public ObservableCollection<OrdnerDaten> OrdnerBezeichnungen { get; set; }
    }

    public class OrdnerDaten
    {
        public string Steuerung { get; set; }
        public string Source { get; set; }
        public  string Destination { get; set; }

        public OrdnerDaten()
        {
            Steuerung = "";
            Source= "";
            Destination = "";
        }
    }
}