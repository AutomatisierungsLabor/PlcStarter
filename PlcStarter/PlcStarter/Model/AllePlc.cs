using System.Collections.Generic;

namespace PlcStarter.Model;

public class AllePlc
{
    public OrdnerStrukturLesen OrdnerStrukturLesen { get; set; }
    public List<TabEigenschaften> AlleTabEigenschaften { get; set; }

    public IPlc Logo { get; set; }
    public IPlc TiaPortal { get; set; }
    public IPlc TwinCat { get; set; }

    public AllePlc()
    {
        AlleTabEigenschaften = new List<TabEigenschaften>();

        OrdnerStrukturLesen = new OrdnerStrukturLesen();
        OrdnerStrukturLesen.GetOrdnerConfig("Einstellungen/Ordner.json");
    }

    public void PlcInitialisieren(MainWindow mw)
    {
        Logo = new PlcLogo(mw, OrdnerStrukturLesen.OrdnerStrukturen);
        Logo.TabEigenschaftenHinzufuegen();

        TiaPortal = new PlcTiaPortal(mw, OrdnerStrukturLesen.OrdnerStrukturen);
        TiaPortal.TabEigenschaftenHinzufuegen();

        TwinCat = new PlcTwinCat(mw, OrdnerStrukturLesen.OrdnerStrukturen);
        TwinCat.TabEigenschaftenHinzufuegen();
    }
}