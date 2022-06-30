using Microsoft.Toolkit.Mvvm.Input;
using PlcStarter.Model;

namespace PlcStarter.ViewModel;

public partial class VmPlcStarter
{
    [ICommand]
    private void ButtonStarten()
    {
        foreach (var job in _mainWindow.PlcProjektdaten.Jobs) AlleJobs.PlcJobAusfuehren(job, _mainWindow.PlcProjektdaten, this);
    }

    [ICommand]
    private void ButtonLoesungAnzeigen()
    {
        if (_mainWindow.Loesungen.FensterAktiv)
        {
            StringButtonLoesungen = "Lösungen anzeigen";
            _mainWindow.Loesungen.FensterAusblenden();
        }
        else
        {
            StringButtonLoesungen = "Lösungen ausblenden";
            _mainWindow.Loesungen.FensterAnzeigen();
        }
    }

    [ICommand]
    private void CheckBoxHaken(string plc)
    {
        var steuerung = plc switch
        {
            "Steuerungen.Logo" => Steuerungen.Logo,
            "Steuerungen.TiaPortal" => Steuerungen.TiaPortal,
            "Steuerungen.TwinCat" => Steuerungen.TwinCat,
            _ => Steuerungen.Logo
        };
        _mainWindow.AnzeigeUpdaten(steuerung);
    }
}