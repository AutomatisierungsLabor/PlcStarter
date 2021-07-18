using PlcStarter.Model;

namespace PlcStarter
{
    public partial class MainWindow
    {
        public AlleDaten AlleDaten { get; set; }
        public AlleWerte AlleWerte { get; set; }
        public ProjektEigenschaften AktuellesProjekt { get; set; }
        public Steuerungen AktuelleSteuerung { get; set; }

        private readonly ViewModel.ViewModel _viewModel;

        public MainWindow()
        {
            AktuelleSteuerung = Steuerungen.Logo;

            _viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = _viewModel;

            AlleWerte = new AlleWerte();
            AlleDaten = new AlleDaten(this);

            AnzeigeUpdaten(Steuerungen.Logo);
            AnzeigeUpdaten(Steuerungen.TwinCat);
            AnzeigeUpdaten(Steuerungen.TiaPortal);
        }
    }
}