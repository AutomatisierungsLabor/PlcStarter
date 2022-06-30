using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Media;
using PlcStarter.Model;

namespace PlcStarter.ViewModel;

public partial class VmPlcStarter : ObservableObject
{

    private readonly MainWindow _mainWindow;

    public VmPlcStarter(MainWindow mw)
    {
        _mainWindow = mw;
        BrushStartButton = Brushes.LightGray;
        StringStartButton = "Bitte ein Projekt auswählen";

        StringButtonLoesungen = "Lösungen anzeigen";

        System.Threading.Tasks.Task.Run(PlcStarterTask);
    }

    private void PlcStarterTask()
    {
        while (true)
        {
            VisibilityLoesungAnzeigen = _mainWindow.SourceAnzeigen ? Visibility.Visible : Visibility.Hidden;

            Thread.Sleep(100);
        }
        // ReSharper disable once FunctionNeverReturns
    }
    public void NeueLoesungAnzeigen(PlcProjektdaten plcProjektdaten) => _mainWindow.Loesungen.NeueLoesungLaden(Path.Combine(plcProjektdaten.OrdnerstrukturSourceProjekt, plcProjektdaten.OrdnerPlc));
}