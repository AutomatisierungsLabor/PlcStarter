using PlcStarter.Model;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlcStarter
{
    public partial class MainWindow
    {
        public AllePlc AllePlc { get; set; }
        public Steuerungen AktuelleSteuerung { get; set; }
        public ViewModel.ViewModel ViewModel { get; set; }

        public MainWindow()
        {
            AktuelleSteuerung = Steuerungen.Logo;

            ViewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = ViewModel;
                         
            if (System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains("kurt.linder")) GridAlles.Background = new SolidColorBrush(Colors.Orange);
            AllePlc = new AllePlc();
            AllePlc.PlcInitialisieren(this);

            AnzeigeUpdaten(Steuerungen.Logo);
            AnzeigeUpdaten(Steuerungen.TwinCat);
            AnzeigeUpdaten(Steuerungen.TiaPortal);
        }
        private void ButtonStartenPlc_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is not Button { Tag: PlcProjektdaten projektdaten }) return;

            foreach (var job in projektdaten.Jobs) AllePlcJobs.PlcJobAusfuehren(job, projektdaten, ViewModel);
        }       
    }
}