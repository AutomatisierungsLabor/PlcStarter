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

            PlcLogo = new PlcLogo();
            PlcLogo.setOrdnerSource(OrdnerStrukturLesen.OrdnerConfig.OrdnerBezeichnungen[1].Source);
            PlcLogo.setOrdnerDestination(OrdnerStrukturLesen.OrdnerConfig.OrdnerBezeichnungen[1].Destination);

        }

    }
}