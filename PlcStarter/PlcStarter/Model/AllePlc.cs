using System.Collections.Generic;

namespace PlcStarter.Model
{
    public class AllePlc
    {
        public OrdnerStrukturLesen OrdnerStrukturLesen { get; set; }
        public List<TabEigenschaften> AlleTabEigenschaften { get; set; }

        public IPlc PlcLogo;
        public IPlc PlcTiaPortal;
        public IPlc PlcTwinCat;

        public AllePlc()
        {
            AlleTabEigenschaften = new List<TabEigenschaften>();

            OrdnerStrukturLesen = new OrdnerStrukturLesen();
            OrdnerStrukturLesen.GetOrdnerConfig("Einstellungen/Ordner.json");
        }

        public void PlcInitialisieren(MainWindow mw)
        {
            PlcLogo = new PlcLogo(mw, OrdnerStrukturLesen.OrdnerConfig.OrdnerBezeichnungen[2]);
            PlcLogo.TabEigenschaftenHinzufuegen();

            PlcTiaPortal = new PlcTiaPortal(mw, OrdnerStrukturLesen.OrdnerConfig.OrdnerBezeichnungen[3]);
            PlcTiaPortal.TabEigenschaftenHinzufuegen();

            PlcTwinCat = new PlcTwinCat(mw, OrdnerStrukturLesen.OrdnerConfig.OrdnerBezeichnungen[4]);
            PlcTwinCat.TabEigenschaftenHinzufuegen();
        }
    }
}