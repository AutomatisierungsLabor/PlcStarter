namespace PlcStarter.Model
{
    public class AllePlc
    {
        public OrdnerStrukturLesen OrdnerStrukturLesen { get; set; }

        public IPlc PlcLogo;


        private readonly MainWindow _mainWindow;

        public AllePlc(MainWindow mw)
        {
            _mainWindow = mw;

            OrdnerStrukturLesen = new OrdnerStrukturLesen();
            OrdnerStrukturLesen.GetOrdnerConfig("Einstellungen/Ordner.json");

            PlcLogo = new PlcLogo(_mainWindow,  OrdnerStrukturLesen.OrdnerConfig.OrdnerBezeichnungen[1]);
            PlcLogo.TabEigenschaftenHinzufuegen();

        }
    }
}