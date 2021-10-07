using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PlcStarter.Model
{
    public class PlcProjekt
    {
        public PlcProjekt(ObservableCollection<PlcProjektdaten> plcProjektliste) => PlcProjektliste = plcProjektliste;
        public ObservableCollection<PlcProjektdaten> PlcProjektliste { get; set; }
    }

    public class PlcProjektdaten
    {
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }
        public PlcSoftwareVersion SoftwareVersion { get; set; }
        public Button ButtonBezeichnung { get; set; }
        public WebBrowser BrowserBezeichnung { get; set; }
        public string OrdnerStrukturDestination { get; set; }
        public string OrdnerStrukturPlc { get; set; }
        public string OrdnerStrukturDigitalTwin { get; set; }
        public string OrdnerStrukturFactoryIo { get; set; }
        public string OrdnerTemplate { get; set; }
        public string OrdnerPlc { get; set; }
        public string OrdnerDigitalTwin { get; set; }
        public string OrdnerFactoryIo { get; set; }
        public PlcSprachen Sprache { get; set; }
        public PlcKategorie Kategorie { get; set; }
        public PlcJobs[] Jobs { get; set; }
    }
}