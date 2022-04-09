using PlcStarter.Model;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlcStarter;

public partial class MainWindow
{
    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not TabControl { SelectedValue: TabItem item }) return;

        VmPlcStarter.StringStartButton = "Bitte ein Projekt auswählen";
        VmPlcStarter.BrushStartButton = Brushes.LightGray;

        HtmlFensterLoeschen();

        AktuelleSteuerung = item.Header.ToString() switch
        {
            "Logo8" => Steuerungen.Logo,
            "TiaPortal" => Steuerungen.TiaPortal,
            "TwinCAT" => Steuerungen.TwinCat,
            _ => AktuelleSteuerung
        };
        AnzeigeUpdaten(AktuelleSteuerung);
    }
    private void HtmlFensterLoeschen()
    {
        foreach (var tabEigenschaften in AllePlc.AlleTabEigenschaften) tabEigenschaften.BrowserBezeichnung.Navigate((Uri)null);
    }
}