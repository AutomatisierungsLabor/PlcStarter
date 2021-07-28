using System;
using PlcStarter.Model;
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
        public void ButtonGeaendert(object obj) => AnzeigeUpdaten(AktuelleSteuerung);

        private void AnzeigeUpdaten(Steuerungen aktuelleSteuerung)
        {

            if (AlleDaten.AlleTabEigenschaften == null) return;

            foreach (var tabEigenschaften in AlleDaten.AlleTabEigenschaften.Where(tabEigenschaften => tabEigenschaften.Steuerungen == aktuelleSteuerung))
            {
                tabEigenschaften.StackPanelBezeichnung?.Children.Clear();

                switch (tabEigenschaften.Steuerungen)
                {
                    case Steuerungen.Logo: AllePlc.PlcLogo.AnzeigeUpdaten(tabEigenschaften); break;
                    case Steuerungen.TiaPortal: AllePlc.PlcTiaPortal.AnzeigeUpdaten(tabEigenschaften); break;
                    case Steuerungen.TwinCat: AllePlc.PlcTwinCat.AnzeigeUpdaten(tabEigenschaften); break;
                    default: throw new ArgumentOutOfRangeException(tabEigenschaften.ToString());
                }

                foreach (var projektEigenschaften in AlleDaten.AlleProjektEigenschaften.Where(projektEigenschaften => projektEigenschaften.Steuerung == aktuelleSteuerung))
                {
                    switch (aktuelleSteuerung)
                    {
                        case Steuerungen.Logo:
                            // AnzeigeUpdatenLogo(tabEigenschaften, projektEigenschaften);
                            break;
                        case Steuerungen.TiaPortal:
                            //AnzeigeUpdatenTiaPortal(tabEigenschaften, projektEigenschaften);
                            break;
                        case Steuerungen.TwinCat:
                            //AnzeigeUpdatenTwinCat(tabEigenschaften, projektEigenschaften);
                            break;
                        default: throw new ArgumentOutOfRangeException(nameof(aktuelleSteuerung), aktuelleSteuerung, null);
                    }
                }
            }


        }
        private void AnzeigeUpdatenTwinCat(TabEigenschaften tabEigenschaften, ProjektEigenschaften projektEigenschaften)
        {
            var als = CheckboxTwinCatAs?.IsChecked != null && (bool)CheckboxTwinCatAs.IsChecked;
            var awl = CheckboxTwinCatAwl?.IsChecked != null && (bool)CheckboxTwinCatAwl.IsChecked;
            var cfc = CheckboxTwinCatCfc?.IsChecked != null && (bool)CheckboxTwinCatCfc.IsChecked;
            var cpp = CheckboxTwinCatCpp?.IsChecked != null && (bool)CheckboxTwinCatCpp.IsChecked;
            var fup = CheckboxTwinCatFup?.IsChecked != null && (bool)CheckboxTwinCatFup.IsChecked;
            var kop = CheckboxTwinCatKop?.IsChecked != null && (bool)CheckboxTwinCatKop.IsChecked;
            var st = CheckboxTwinCatSt?.IsChecked != null && (bool)CheckboxTwinCatSt.IsChecked;

            switch (projektEigenschaften.Programmiersprache)
            {
                case PlcSprachen.As when als:
                case PlcSprachen.Awl when awl:
                case PlcSprachen.Cfc when cfc:
                case PlcSprachen.Cpp when cpp:
                case PlcSprachen.Fup when fup:
                case PlcSprachen.Kop when kop:
                case PlcSprachen.St when st:
                    EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                    break;
                case PlcSprachen.Scl:
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(projektEigenschaften));
            }
        }



        private void EinzelnenTabFuellen(TabEigenschaften tabEigenschaften, ProjektEigenschaften projektEigenschaften)
        {
            if (tabEigenschaften.PlcKategorie != projektEigenschaften.PlcKategorie) return;

            projektEigenschaften.BrowserBezeichnung = tabEigenschaften.BrowserBezeichnung;

            var rdo = new RadioButton
            {
                GroupName = projektEigenschaften.Steuerung.ToString(),
                Name = projektEigenschaften.Bezeichnung,
                FontSize = 14,
                Content = projektEigenschaften.Bezeichnung + " (" + AlleWerte.AlleProgrammiersprachen[projektEigenschaften.Programmiersprache].Anzeige + ")",
                VerticalAlignment = VerticalAlignment.Top,
                Tag = projektEigenschaften
            };

            switch (tabEigenschaften.Steuerungen)
            {
                case Steuerungen.Logo:
                case Steuerungen.TwinCat:
                case Steuerungen.TiaPortal:
                    rdo.Checked += RadioButton_Checked;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(projektEigenschaften));
            }

            tabEigenschaften.StackPanelBezeichnung.Children.Add(rdo);
        }
        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton rb)) return;

            switch (rb.Tag)
            {
                case ProjektEigenschaften projektEigenschaften:
                    {
                        // alte Variante
                        ViewModel.ViAnzeige.StartButtonInhalt = "Projekt starten";
                        ViewModel.ViAnzeige.StartButtonFarbe = Brushes.LawnGreen;

                        AktuellesProjekt = projektEigenschaften;
                        var parentDirectory = new DirectoryInfo(AktuellesProjekt.QuellOrdner);
                        var dateiName = $@"{parentDirectory.FullName}\index.html";

                        var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

                        var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
                        var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

                        AktuellesProjekt.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);
                        break;
                    }
                case PlcProjektdaten projektdaten:
                    {
                        // neue Version
                        ViewModel.ViAnzeige.StartButtonInhalt = "Projekt starten";
                        ViewModel.ViAnzeige.StartButtonFarbe = Brushes.LawnGreen;

                        var dateiName = $@"{projektdaten.OrdnerSource}\{projektdaten.OrdnerProjekt}\index.html";

                        var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

                        var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
                        var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);
                        projektdaten.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);

                        projektdaten.ButtonBezeichnung.Tag = projektdaten;
                        break;
                    }
            }
        }
    }
}