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
        public int LaufendeNummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }
        public PlcSoftwareVersion SoftwareVersion { get; set; }
        public Button ButtonBezeichnung { get; set; }
        public WebBrowser BrowserBezeichnung { get; set; }
        public string OrdnerSource { get; set; }
        public string OrdnerDestination { get; set; }
        public string OrdnerProjekt { get; set; }
        public string OrdnerDigitalTwin { get; set; }
        public PlcSprachen Sprache { get; set; }
        public PlcKategorie Kategorie { get; set; }
        public PlcJobs[] Jobs { get; set; }
    }
}
