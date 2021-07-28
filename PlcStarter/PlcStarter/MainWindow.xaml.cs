using System.Windows.Controls;
using PlcStarter.Model;

namespace PlcStarter
{
    public partial class MainWindow
    {
        public AllePlc AllePlc { get; set; }
        public AlleDaten AlleDaten { get; set; }
        public AlleWerte AlleWerte { get; set; }
        public ProjektEigenschaften AktuellesProjekt { get; set; }
        public Steuerungen AktuelleSteuerung { get; set; }

        public ViewModel.ViewModel ViewModel { get; set; }

        public MainWindow()
        {
            AktuelleSteuerung = Steuerungen.Logo;

            ViewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = ViewModel;


            AlleWerte = new AlleWerte();
            AlleDaten = new AlleDaten(this);

            AllePlc = new AllePlc(this);

            AnzeigeUpdaten(Steuerungen.Logo);
            AnzeigeUpdaten(Steuerungen.TwinCat);
            AnzeigeUpdaten(Steuerungen.TiaPortal);
        }

        private void ButtonStartenPlc_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!(sender is Button {Tag: PlcProjektdaten projektdaten})) return;

            foreach (var job in projektdaten.Jobs)
            {
                AllePlcJobs.PlcJobAusfuehren(job, projektdaten, ViewModel);
            }
        }
    }
}