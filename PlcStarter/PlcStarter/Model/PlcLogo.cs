using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace PlcStarter.Model
{
    public class PlcLogo : IPlc
    {

        public Logo8Projekt Logo8Projekte { get; set; }

        private readonly MainWindow _mainWindow;
        private readonly string _ordnerSource;
        private readonly string _ordnerDestination;

        public PlcLogo(MainWindow mainWindow, OrdnerDaten ordnerDaten)
        {
            _mainWindow = mainWindow;
            _ordnerSource = ordnerDaten.Source;
            _ordnerDestination = ordnerDaten.Destination;

            Logo8Projekte =
                JsonConvert.DeserializeObject<Logo8Projekt>(
                    File.ReadAllText(_ordnerSource + "/Logo8Projektliste.json"));
        }

        public void TabEigenschaftenHinzufuegen()
        {
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.Plc, Steuerungen.Logo, _mainWindow.WebLogoPlc, _mainWindow.StackPanelLogoPlc, _mainWindow.ButtonStartenLogoPlc));

            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.Bug, Steuerungen.Logo, _mainWindow.WebLogoPlcBugs, _mainWindow.StackPanelLogoPlcBugs, _mainWindow.ButtonStartenLogoPlcBugs));
        }

        public void AnzeigeUpdaten(TabEigenschaften tabEigenschaften)
        {
            var fup = _mainWindow.CheckboxLogoFup?.IsChecked != null && (bool)_mainWindow.CheckboxLogoFup.IsChecked;
            var kop = _mainWindow.CheckboxLogoKop?.IsChecked != null && (bool)_mainWindow.CheckboxLogoKop.IsChecked;

            foreach (var logoProjekt in Logo8Projekte.Logo8Projektliste)
            {
                if (tabEigenschaften.PlcKategorie != logoProjekt.Kategorie) continue;

                if (!fup && logoProjekt.Sprache == PlcSprachen.Fup) continue;
                if (!kop && logoProjekt.Sprache == PlcSprachen.Kop) continue;

                logoProjekt.ButtonBezeichnung = tabEigenschaften.ButtonBezeichnung;
                logoProjekt.BrowserBezeichnung = tabEigenschaften.BrowserBezeichnung;
                logoProjekt.OrdnerSource = _ordnerSource;
                logoProjekt.OrdnerDestination = _ordnerDestination;

                var rdo = new RadioButton
                {
                    GroupName = "Logo",
                    Name = logoProjekt.Bezeichnung,
                    FontSize = 14,
                    Content = logoProjekt.Bezeichnung + " (" + logoProjekt.Kommentar + " / " + logoProjekt.Sprache + ")",
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = logoProjekt
                };

                rdo.Checked += _mainWindow.RadioButton_Checked;

                tabEigenschaften.StackPanelBezeichnung.Children.Add(rdo);
            }
        }

        public void ProjektStarten(ViewModel.ViewModel viewModel, Button btn)
        {
            switch (btn.Tag)
            {
                case Logo8Projektdaten projektdaten:
                    LogoJobAusfuehren(projektdaten.Jobs[0], projektdaten, viewModel);
                    LogoJobAusfuehren(projektdaten.Jobs[1], projektdaten, viewModel);
                    LogoJobAusfuehren(projektdaten.Jobs[2], projektdaten, viewModel);
                    break;
            }
        }
        internal void LogoJobAusfuehren(PlcJobs job, Logo8Projektdaten projektdaten, ViewModel.ViewModel viewModel)
        {
            switch (job)
            {
                case PlcJobs.None: break;
                case PlcJobs.SorceOrdnerErstellen:
                    AllePlcJobs.DestinationOrdnerErstellen(viewModel, projektdaten.OrdnerDestination, projektdaten.OrdnerProjekt);
                    break;
                case PlcJobs.ProjektKopieren:
                    AllePlcJobs.ProjektOrdnerKopieren(viewModel, projektdaten.OrdnerSource, projektdaten.OrdnerDestination, projektdaten.OrdnerProjekt);
                    break;
                case PlcJobs.CmdDateiAusfuehren:
                    AllePlcJobs.ProjektStarten(viewModel, projektdaten.OrdnerDestination, projektdaten.OrdnerProjekt);
                    break;
            }
        }
    }
}