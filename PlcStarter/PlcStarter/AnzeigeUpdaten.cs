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

        private void AnzeigeUpdaten(Model.PlcStarter.Steuerungen aktuelleSteuerung)
        {

            if (AlleDaten.AlleTabEigenschaften == null) return;

            foreach (var tabEigenschaften in AlleDaten.AlleTabEigenschaften.Where(tabEigenschaften =>
                tabEigenschaften.Steuerungen == aktuelleSteuerung))
            {
                tabEigenschaften.StackPanelBezeichnung?.Children.Clear();

                foreach (var projektEigenschaften in AlleDaten.AlleProjektEigenschaften.Where(projektEigenschaften =>
                    projektEigenschaften.Steuerung == aktuelleSteuerung))
                {
                    switch (aktuelleSteuerung)
                    {
                        case Model.PlcStarter.Steuerungen.Logo:
                            AnzeigeUpdatenLogo(tabEigenschaften, projektEigenschaften);
                            break;
                        case Model.PlcStarter.Steuerungen.TiaPortal:
                            AnzeigeUpdatenTiaPortal(tabEigenschaften, projektEigenschaften);
                            break;
                        case Model.PlcStarter.Steuerungen.TwinCat:
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
                case Model.PlcStarter.PlcSprachen.Fup when fup:
                case Model.PlcStarter.PlcSprachen.Kop when kop:
                    EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                    break;
                case Model.PlcStarter.PlcSprachen.As:
                    break;
                case Model.PlcStarter.PlcSprachen.Awl:
                    break;
                case Model.PlcStarter.PlcSprachen.Cfc:
                    break;
                case Model.PlcStarter.PlcSprachen.Cpp:
                    break;
                case Model.PlcStarter.PlcSprachen.Scl:
                    break;
                case Model.PlcStarter.PlcSprachen.Stl:
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
                case Model.PlcStarter.PlcSprachen.As when als:
                case Model.PlcStarter.PlcSprachen.Awl when awl:
                case Model.PlcStarter.PlcSprachen.Cfc when cfc:
                case Model.PlcStarter.PlcSprachen.Cpp when cpp:
                case Model.PlcStarter.PlcSprachen.Fup when fup:
                case Model.PlcStarter.PlcSprachen.Kop when kop:
                case Model.PlcStarter.PlcSprachen.Stl when st:
                    EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                    break;
                case Model.PlcStarter.PlcSprachen.Scl:
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
                case Model.PlcStarter.PlcSprachen.Fup when fup:
                case Model.PlcStarter.PlcSprachen.Kop when kop:
                case Model.PlcStarter.PlcSprachen.Scl when scl:
                    EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                    break;
                case Model.PlcStarter.PlcSprachen.As:
                    break;
                case Model.PlcStarter.PlcSprachen.Awl:
                    break;
                case Model.PlcStarter.PlcSprachen.Cfc:
                    break;
                case Model.PlcStarter.PlcSprachen.Cpp:
                    break;
                case Model.PlcStarter.PlcSprachen.Stl:
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
                //kurt    Content = projektEigenschaften.Bezeichnung + " (" +Model.AlleWerte.AlleProgrammiersprachen[projektEigenschaften.Programmiersprache].Anzeige + ")",
                VerticalAlignment = VerticalAlignment.Top,
                Tag = projektEigenschaften
            };

            switch (tabEigenschaften.Steuerungen)
            {
                case Model.PlcStarter.Steuerungen.Logo:
                case Model.PlcStarter.Steuerungen.TwinCat:
                case Model.PlcStarter.Steuerungen.TiaPortal:
                    rdo.Checked += RadioButton_Checked;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(projektEigenschaften));
            }

            tabEigenschaften.StackPanelBezeichnung.Children.Add(rdo);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton rb) || !(rb.Tag is ProjektEigenschaften projektEigenschaften)) return;

            _viewModel.ViAnzeige.StartButtonInhalt = "Projekt starten";
            _viewModel.ViAnzeige.StartButtonFarbe = Brushes.LawnGreen;

            AktuellesProjekt = projektEigenschaften;
            var parentDirectory = new DirectoryInfo(AktuellesProjekt.QuellOrdner);
            var dateiName = $@"{parentDirectory.FullName}\index.html";

            var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

            var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
            var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            AktuellesProjekt.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);
        }
    }
}