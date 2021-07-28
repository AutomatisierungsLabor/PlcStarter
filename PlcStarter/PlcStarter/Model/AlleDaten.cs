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