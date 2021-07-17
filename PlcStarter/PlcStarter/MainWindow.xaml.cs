using PlcStarter.Model;

namespace PlcStarter
{
    public partial class MainWindow
    {
        public AlleDaten AlleDaten { get; set; }
        public AlleWerte AlleWerte { get; set; }
        public ProjektEigenschaften AktuellesProjekt { get; set; }
        public Model.PlcStarter.Steuerungen AktuelleSteuerung { get; set; }

        private readonly ViewModel.ViewModel _viewModel;

        public MainWindow()
        {
            AktuelleSteuerung = Model.PlcStarter.Steuerungen.Logo;

            AlleWerte = new AlleWerte();
            AlleDaten = new AlleDaten(this);

            _viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = _viewModel;

            AnzeigeUpdaten(Model.PlcStarter.Steuerungen.Logo);
            AnzeigeUpdaten(Model.PlcStarter.Steuerungen.TwinCat);
            AnzeigeUpdaten(Model.PlcStarter.Steuerungen.TiaPortal);
        }
    }
}