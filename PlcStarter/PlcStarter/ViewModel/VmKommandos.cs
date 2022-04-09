using System;
using Microsoft.Toolkit.Mvvm.Input;
using PlcStarter.Model;

namespace PlcStarter.ViewModel;

public partial class VmPlcStarter
{
    [ICommand]
    private void ButtonStarten()
    {
        foreach (var job in _mainWindow.PlcProjektdaten.Jobs) AllePlcJobs.PlcJobAusfuehren(job, _mainWindow.PlcProjektdaten, this);
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