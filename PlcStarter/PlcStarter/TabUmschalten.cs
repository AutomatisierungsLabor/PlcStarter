using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlcStarter
{
    public partial class MainWindow
    {
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is TabControl tabControl) || !(tabControl.SelectedValue is TabItem item)) return;

            _viewModel.ViAnzeige.StartButtonInhalt = "Bitte ein Projekt auswählen";
            _viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;

            HtmlFensterLoeschen();

            AktuelleSteuerung = item.Header.ToString() switch
            {
                "Logo8" => Model.PlcStarter.Steuerungen.Logo,
                "TiaPortal" => Model.PlcStarter.Steuerungen.TiaPortal,
                "TwinCAT" => Model.PlcStarter.Steuerungen.TwinCat,
                _ => AktuelleSteuerung
            };
            AnzeigeUpdaten(AktuelleSteuerung);
        }

        private static void HtmlFensterLoeschen()
        {
        //kurt    foreach (var tabEigenschaften in Model.AlleDaten.AlleTabEigenschaften) tabEigenschaften.BrowserBezeichnung.Navigate((Uri)null);
        }
    }
}