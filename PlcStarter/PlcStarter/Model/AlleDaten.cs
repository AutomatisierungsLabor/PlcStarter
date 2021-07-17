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

            AlleProjektEigenschaften = new();
            AlleTabEigenschaften = new();

            var einstellungen = EinstellungenOrdnerLesen.FromJson(File.ReadAllText(@"Einstellungen.json"));

            OrdnerEinlesen(_mainWindow, einstellungen.Logo.Source, einstellungen.Logo.Destination, PlcStarter.Steuerungen.Logo);
            OrdnerEinlesen(_mainWindow, einstellungen.TiaPortal.Source, einstellungen.TiaPortal.Destination, PlcStarter.Steuerungen.TiaPortal);
            OrdnerEinlesen(_mainWindow, einstellungen.TwinCat.Source, einstellungen.TwinCat.Destination, PlcStarter.Steuerungen.TwinCat);

            TabEigenschaftenEinlesenLogo();
            TabEigenschaftenEinlesenTiaPortal();
            TabEigenschaftenEinlesenTwinCat();
        }


        public void TabEigenschaftenEinlesenLogo()
        {
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Plc, PlcStarter.Steuerungen.Logo, _mainWindow.WebLogoPlc, _mainWindow.StackPanelLogoPlc, _mainWindow.ButtonStartenLogoPlc));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Bug, PlcStarter.Steuerungen.Logo, _mainWindow.WebLogoPlcBugs, _mainWindow.StackPanelLogoPlcBugs, _mainWindow.ButtonStartenLogoPlcBugs));
        }

        public void TabEigenschaftenEinlesenTiaPortal()
        {
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Plc, PlcStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlc, _mainWindow.StackPanelTiaPortalPlc, _mainWindow.ButtonStartenTiaPortalPlc));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Hmi, PlcStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcHmi, _mainWindow.StackPanelTiaPortalPlcHmi, _mainWindow.ButtonStartenTiaPortalPlcHmi));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.FactoryIo, PlcStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcFio, _mainWindow.StackPanelTiaPortalPlcFio, _mainWindow.ButtonStartenTiaPortalPlcFio));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.DigitalTwin, PlcStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcDt, _mainWindow.StackPanelTiaPortalPlcDt, _mainWindow.ButtonStartenTiaPortalPlcDt));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Snap7, PlcStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcSnap7, _mainWindow.StackPanelTiaPortalPlcSnap7, _mainWindow.ButtonStartenTiaPortalPlcSnap7));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.SoftwareTests, PlcStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcTests, _mainWindow.StackPanelTiaPortalPlcTests, _mainWindow.ButtonStartenTiaPortalPlcTests));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Bug, PlcStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcBugs, _mainWindow.StackPanelTiaPortalPlcBugs, _mainWindow.ButtonStartenTiaPortalPlcBugs));
        }

        public void TabEigenschaftenEinlesenTwinCat()
        {
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Plc, PlcStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlc, _mainWindow.StackPanelTwinCatPlc, _mainWindow.ButtonStartenTwinCatPlc));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Visu, PlcStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcVisu, _mainWindow.StackPanelTwinCatPlcVisu, _mainWindow.ButtonStartenTwinCatPlcVisu));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Nc, PlcStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcNc, _mainWindow.StackPanelTwinCatPlcNc, _mainWindow.ButtonStartenTwinCatPlcNc));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.DigitalTwin, PlcStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcDt, _mainWindow.StackPanelTwinCatPlcDt, _mainWindow.ButtonStartenTwinCatPlcDt));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.AdsRemote, PlcStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcAds, _mainWindow.StackPanelTwinCatPlcAds, _mainWindow.ButtonStartenTwinCatPlcAds));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.SoftwareTests, PlcStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcTests, _mainWindow.StackPanelTwinCatPlcTests, _mainWindow.ButtonStartenTwinCatPlcTests));
            AlleTabEigenschaften.Add(new TabEigenschaften(PlcStarter.PlcKategorie.Bug, PlcStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcBugs, _mainWindow.StackPanelTwinCatPlcBugs, _mainWindow.ButtonStartenTwinCatPlcBugs));
        }

        private void OrdnerEinlesen(MainWindow mw, string source, string destination, PlcStarter.Steuerungen steuerungen)
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