namespace PlcStarter.Model
{
    public class AllePlc
    {
        public OrdnerStrukturLesen OrdnerStrukturLesen { get; set; }

        public IPlc PlcLogo;
        
        public AllePlc(MainWindow mw)
        {
            OrdnerStrukturLesen = new OrdnerStrukturLesen();
            OrdnerStrukturLesen.GetOrdnerConfig("Einstellungen/Ordner.json");

            PlcLogo = new PlcLogo(mw,  OrdnerStrukturLesen.OrdnerConfig.OrdnerBezeichnungen[1]);
            PlcLogo.TabEigenschaftenHinzufuegen();

        }
    }
}