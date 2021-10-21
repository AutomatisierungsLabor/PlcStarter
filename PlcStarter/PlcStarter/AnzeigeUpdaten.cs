using PlcStarter.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlcStarter
{
    public partial class MainWindow
    {
        public void ButtonGeaendert(object _) => AnzeigeUpdaten(AktuelleSteuerung);
        private void AnzeigeUpdaten(Steuerungen aktuelleSteuerung)
        {
            if (AllePlc.AlleTabEigenschaften == null) return;

            foreach (var tabEigenschaften in AllePlc.AlleTabEigenschaften.Where(tabEigenschaften => tabEigenschaften.Steuerungen == aktuelleSteuerung))
            {
                tabEigenschaften.StackPanelBezeichnung?.Children.Clear();

                switch (tabEigenschaften.Steuerungen)
                {
                    case Steuerungen.Logo: AllePlc.Logo.AnzeigeUpdaten(tabEigenschaften); break;
                    case Steuerungen.TiaPortal: AllePlc.TiaPortal.AnzeigeUpdaten(tabEigenschaften); break;
                    case Steuerungen.TwinCat: AllePlc.TwinCat.AnzeigeUpdaten(tabEigenschaften); break;
                    default: throw new ArgumentOutOfRangeException(tabEigenschaften.ToString());
                }
            }
        }
        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is not RadioButton { Tag: PlcProjektdaten projektdaten }) return;

            ViewModel.ViAnzeige.StartButtonInhalt = "Projekt starten";
            ViewModel.ViAnzeige.StartButtonFarbe = Brushes.LawnGreen;

            var dateiName = $@"{projektdaten.OrdnerStrukturPlc}\{projektdaten.OrdnerPlc}\index.html";

            var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

            var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
            var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);
            projektdaten.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);

            projektdaten.ButtonBezeichnung.Tag = projektdaten;
        }
    }
}