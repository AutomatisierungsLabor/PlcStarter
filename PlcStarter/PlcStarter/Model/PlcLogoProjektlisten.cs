using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PlcStarter.Model
{
    public class Logo8Projekt
    {
        public Logo8Projekt(ObservableCollection<Logo8Projektdaten> logo8Projektliste)
        {
            Logo8Projektliste = logo8Projektliste;
        }

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
        public PlcJobs Job1 { get; set; }
        public PlcJobs Job2 { get; set; }
        public PlcJobs Job3 { get; set; }
        public Logo8Projektdaten()
        {
            LaufendeNummer = 0;
            Bezeichnung = "";
            Kommentar = "";
            SoftwareVersion = PlcSoftwareVersion.None;
            ButtonBezeichnung = null;
            BrowserBezeichnung = null;
            OrdnerSource = null;
            OrdnerDestination = null;
            OrdnerProjekt = null;
            Sprache = PlcSprachen.Kop;
            Kategorie = PlcKategorie.Plc;
            Job1 = PlcJobs.None;
            Job2 = PlcJobs.None;
            Job3 = PlcJobs.None;
        }
    }
}
