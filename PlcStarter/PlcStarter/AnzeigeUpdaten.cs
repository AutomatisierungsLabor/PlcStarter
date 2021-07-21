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

                    default: break;
                }

                foreach (var projektEigenschaften in AlleDaten.AlleProjektEigenschaften.Where(projektEigenschaften => projektEigenschaften.Steuerung == aktuelleSteuerung))
                {
                    switch (aktuelleSteuerung)
                    {
                        case Steuerungen.Logo:
                            // AnzeigeUpdatenLogo(tabEigenschaften, projektEigenschaften);
                            break;
                        case Steuerungen.TiaPortal:
                            AnzeigeUpdatenTiaPortal(tabEigenschaften, projektEigenschaften);
                            break;
                        case Steuerungen.TwinCat:
                            AnzeigeUpdatenTwinCat(tabEigenschaften, projektEigenschaften);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(aktuelleSteuerung), aktuelleSteuerung, null);
                    }
                }
            }


        }

        private void AnzeigeUpdatenLogo(TabEigenschaften tabEigenschaften, ProjektEigenschaften projektEigenschaften)
        {
            var fup = CheckboxLogoFup?.IsChecked != null && (bool)CheckboxLogoFup.IsChecked;
            var kop = CheckboxTwinCatKop?.IsChecked != null && (bool)CheckboxTwinCatKop.IsChecked;

            switch (projektEigenschaften.Programmiersprache)
            {
                case PlcSprachen.Fup when fup:
                case PlcSprachen.Kop when kop:
                    EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                    break;
                case PlcSprachen.As:
                    break;
                case PlcSprachen.Awl:
                    break;
                case PlcSprachen.Cfc:
                    break;
                case PlcSprachen.Cpp:
                    break;
                case PlcSprachen.Scl:
                    break;
                case PlcSprachen.Stl:
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(projektEigenschaften));
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
                case PlcSprachen.Stl when st:
                    EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                    break;
                case PlcSprachen.Scl:
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(projektEigenschaften));
            }
        }

        private void AnzeigeUpdatenTiaPortal(TabEigenschaften tabEigenschaften, ProjektEigenschaften projektEigenschaften)
        {
            var fup = CheckboxTiaPortalFup?.IsChecked != null && (bool)CheckboxTiaPortalFup.IsChecked;
            var kop = CheckboxTiaPortalKop?.IsChecked != null && (bool)CheckboxTiaPortalKop.IsChecked;
            var scl = CheckboxTiaPortalScl?.IsChecked != null && (bool)CheckboxTiaPortalScl.IsChecked;

            switch (projektEigenschaften.Programmiersprache)
            {
                case PlcSprachen.Fup when fup:
                case PlcSprachen.Kop when kop:
                case PlcSprachen.Scl when scl:
                    EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                    break;
                case PlcSprachen.As:
                    break;
                case PlcSprachen.Awl:
                    break;
                case PlcSprachen.Cfc:
                    break;
                case PlcSprachen.Cpp:
                    break;
                case PlcSprachen.Stl:
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

            if (rb.Tag is ProjektEigenschaften projektEigenschaften)
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
            }

            if (rb.Tag is Logo8Projektdaten projektdaten)
            {
                // neue Version
                ViewModel.ViAnzeige.StartButtonInhalt = "Projekt starten";
                ViewModel.ViAnzeige.StartButtonFarbe = Brushes.LawnGreen;

                var dateiName = $@"{projektdaten.Source}\{projektdaten.Ordner}\index.html";

                var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

                var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
                var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);
                projektdaten.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);

                projektdaten.ButtonBezeichnung.Tag = projektdaten;
            }


        }
    }
}