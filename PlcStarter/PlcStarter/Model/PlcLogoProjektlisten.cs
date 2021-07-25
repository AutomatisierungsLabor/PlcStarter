using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PlcStarter.Model
{
    public class Logo8Projekt
    {
        public Logo8Projekt(ObservableCollection<Logo8Projektdaten> logo8Projektliste) => Logo8Projektliste = logo8Projektliste;
        public ObservableCollection<Logo8Projektdaten> Logo8Projektliste { get; set; }
    }

    public class Logo8Projektdaten
    {
        public int LaufendeNummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }
        public PlcSoftwareVersion SoftwareVersion { get; set; }
        public Button ButtonBezeichnung { get; set; }
        public WebBrowser BrowserBezeichnung { get; set; }
        public  string OrdnerSource { get; set; }
        public string OrdnerDestination { get; set; }
        public string OrdnerProjekt { get; set; }
        public PlcSprachen Sprache { get; set; }
        public PlcKategorie Kategorie { get; set; }
        public PlcJobs[] Jobs { get; set; }
      }
}
