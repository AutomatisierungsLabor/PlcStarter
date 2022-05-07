using PlcStarter.Model;
using PlcStarter.ViewModel;
using System.Windows.Media;

namespace PlcStarter;

public partial class MainWindow
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public AllePlc AllePlc { get; set; }
    public Steuerungen AktuelleSteuerung { get; set; }
    public VmPlcStarter VmPlcStarter { get; set; }
    public PlcProjektdaten PlcProjektdaten { get; set; }
    public LibTextbausteine.GetTextbausteine GetTextbausteine { get; set; }


    public MainWindow()
    {
        Log.Debug("Konstruktor - startet");

        AktuelleSteuerung = Steuerungen.Logo;
        PlcProjektdaten = new PlcProjektdaten();
        GetTextbausteine = new LibTextbausteine.GetTextbausteine();
        GetTextbausteine.SetServerUrl("https://linderonline.at/fk/GetLehrstoffTextbausteine.php");

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