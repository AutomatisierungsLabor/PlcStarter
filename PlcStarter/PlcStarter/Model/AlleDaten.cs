using System.Collections.Generic;
using System.IO;

namespace PlcStarter.Model
{
    public class AlleDaten
    {
      
        public List<ProjektEigenschaften> AlleProjektEigenschaften { get; set; }
        public List<TabEigenschaften> AlleTabEigenschaften { get; set; }


        private readonly MainWindow _mainWindow;

        public AlleDaten(MainWindow mw)
        {
            _mainWindow = mw;


        
            
            AlleProjektEigenschaften = new List<ProjektEigenschaften>();
            AlleTabEigenschaften = new List<TabEigenschaften>();

            var einstellungen = EinstellungenOrdnerLesen.FromJson(File.ReadAllText(@"Einstellungen.json"));

            OrdnerEinlesen(_mainWindow, einstellungen.Logo.Source, einstellungen.Logo.Destination, Steuerungen.Logo);
            OrdnerEinlesen(_mainWindow, einstellungen.TiaPortal.Source, einstellungen.TiaPortal.Destination, Steuerungen.TiaPortal);
            OrdnerEinlesen(_mainWindow, einstellungen.TwinCat.Source, einstellungen.TwinCat.Destination, Steuerungen.TwinCat);

            TabEigenschaftenEinlesenTiaPortal();
            TabEigenschaftenEinlesenTwinCat();
        }

        public void TabEigenschaftenEinlesenTiaPortal()
        {
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Plc, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlc, _mainWindow.StackPanelTiaPortalPlc, _mainWindow.ButtonStartenTiaPortalPlc));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Hmi, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcHmi, _mainWindow.StackPanelTiaPortalPlcHmi, _mainWindow.ButtonStartenTiaPortalPlcHmi));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.FactoryIo, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcFio, _mainWindow.StackPanelTiaPortalPlcFio, _mainWindow.ButtonStartenTiaPortalPlcFio));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.DigitalTwin, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcDt, _mainWindow.StackPanelTiaPortalPlcDt, _mainWindow.ButtonStartenTiaPortalPlcDt));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Snap7, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcSnap7, _mainWindow.StackPanelTiaPortalPlcSnap7, _mainWindow.ButtonStartenTiaPortalPlcSnap7));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.SoftwareTests, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcTests, _mainWindow.StackPanelTiaPortalPlcTests, _mainWindow.ButtonStartenTiaPortalPlcTests));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Bug, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcBugs, _mainWindow.StackPanelTiaPortalPlcBugs, _mainWindow.ButtonStartenTiaPortalPlcBugs));
        }

        public void TabEigenschaftenEinlesenTwinCat()
        {
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Plc, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlc, _mainWindow.StackPanelTwinCatPlc, _mainWindow.ButtonStartenTwinCatPlc));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Visu, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcVisu, _mainWindow.StackPanelTwinCatPlcVisu, _mainWindow.ButtonStartenTwinCatPlcVisu));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Nc, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcNc, _mainWindow.StackPanelTwinCatPlcNc, _mainWindow.ButtonStartenTwinCatPlcNc));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.DigitalTwin, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcDt, _mainWindow.StackPanelTwinCatPlcDt, _mainWindow.ButtonStartenTwinCatPlcDt));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.AdsRemote, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcAds, _mainWindow.StackPanelTwinCatPlcAds, _mainWindow.ButtonStartenTwinCatPlcAds));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.SoftwareTests, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcTests, _mainWindow.StackPanelTwinCatPlcTests, _mainWindow.ButtonStartenTwinCatPlcTests));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Bug, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcBugs, _mainWindow.StackPanelTwinCatPlcBugs, _mainWindow.ButtonStartenTwinCatPlcBugs));
        }

        private void OrdnerEinlesen(MainWindow mw, string source, string destination, Steuerungen steuerungen)
        {
            var parentDirectory = new DirectoryInfo(source);

            foreach (var ordnerInfo in parentDirectory.GetDirectories())
            {
                if ((ordnerInfo.Attributes & FileAttributes.Directory) != 0 && ordnerInfo.Name != ".git" && ordnerInfo.Name != "_SharedDll")
                {
                    AlleProjektEigenschaften.Add(new ProjektEigenschaften(mw, steuerungen, ordnerInfo.FullName, destination));
                }
            }
        }
    }
}