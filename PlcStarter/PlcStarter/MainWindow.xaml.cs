using PlcStarter.Model;
using System.Windows.Media;
using PlcStarter.ViewModel;

namespace PlcStarter;

public partial class MainWindow
{
    public AllePlc AllePlc { get; set; }
    public Steuerungen AktuelleSteuerung { get; set; }
    public VmPlcStarter VmPlcStarter { get; set; }
    public PlcProjektdaten PlcProjektdaten { get; set; }

    public MainWindow()
    {
        AktuelleSteuerung = Steuerungen.Logo;
        PlcProjektdaten = new PlcProjektdaten();

        VmPlcStarter = new VmPlcStarter(this);

        InitializeComponent();
        DataContext = VmPlcStarter;

        if (System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains("kurt.linder")) GridAlles.Background = new SolidColorBrush(Colors.Orange);
        AllePlc = new AllePlc();
        AllePlc.PlcInitialisieren(this);

        AnzeigeUpdaten(Steuerungen.Logo);
        AnzeigeUpdaten(Steuerungen.TwinCat);
        AnzeigeUpdaten(Steuerungen.TiaPortal);
    }
}