using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace PlcStarter.Model
{
    public class PlcTiaPortal : IPlc
    {
        public PlcProjekt PlcProjekte { get; set; }

        private readonly MainWindow _mainWindow;
        private readonly string _ordnerSource;
        private readonly string _ordnerDestination;

        public PlcTiaPortal(MainWindow mainWindow, OrdnerDaten ordnerDaten)
        {
            _mainWindow = mainWindow;
            _ordnerSource = ordnerDaten.Source;
            _ordnerDestination = ordnerDaten.Destination;

            PlcProjekte =
                JsonConvert.DeserializeObject<PlcProjekt>(
                    File.ReadAllText(_ordnerSource + "/TiaPortalProjektliste.json"));
        }

        public void TabEigenschaftenHinzufuegen()
        {
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.Plc, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlc, _mainWindow.StackPanelTiaPortalPlc, _mainWindow.ButtonStartenTiaPortalPlc));
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.Hmi, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcHmi, _mainWindow.StackPanelTiaPortalPlcHmi, _mainWindow.ButtonStartenTiaPortalPlcHmi));
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.FactoryIo, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcFio, _mainWindow.StackPanelTiaPortalPlcFio, _mainWindow.ButtonStartenTiaPortalPlcFio));
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.DigitalTwin, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcDt, _mainWindow.StackPanelTiaPortalPlcDt, _mainWindow.ButtonStartenTiaPortalPlcDt));
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.Snap7, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcSnap7, _mainWindow.StackPanelTiaPortalPlcSnap7, _mainWindow.ButtonStartenTiaPortalPlcSnap7));
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.AutoTests, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcTests, _mainWindow.StackPanelTiaPortalPlcTests, _mainWindow.ButtonStartenTiaPortalPlcTests));
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(
                new TabEigenschaften(PlcKategorie.Bug, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcBugs, _mainWindow.StackPanelTiaPortalPlcBugs, _mainWindow.ButtonStartenTiaPortalPlcBugs));

        }

        public void AnzeigeUpdaten(TabEigenschaften tabEigenschaften)
        {
            var fup = _mainWindow.CheckboxTiaPortalFup?.IsChecked != null && (bool)_mainWindow.CheckboxTiaPortalFup.IsChecked;
            var kop = _mainWindow.CheckboxTiaPortalKop?.IsChecked != null && (bool)_mainWindow.CheckboxTiaPortalKop.IsChecked;
            var scl = _mainWindow.CheckboxTiaPortalScl?.IsChecked != null && (bool)_mainWindow.CheckboxTiaPortalScl.IsChecked;

            foreach (var plcProjekt in PlcProjekte.PlcProjektliste)
            {
                if (tabEigenschaften.PlcKategorie != plcProjekt.Kategorie) continue;

                if (!fup && plcProjekt.Sprache == PlcSprachen.Fup) continue;
                if (!kop && plcProjekt.Sprache == PlcSprachen.Kop) continue;
                if (!scl && plcProjekt.Sprache == PlcSprachen.Scl) continue;

                plcProjekt.BrowserBezeichnung = tabEigenschaften.BrowserBezeichnung;
                plcProjekt.OrdnerSource = _ordnerSource;
                plcProjekt.OrdnerDestination = _ordnerDestination;

                var rdo = new RadioButton
                {
                    GroupName = "TiaPortal",
                    Name = plcProjekt.Bezeichnung,
                    FontSize = 14,
                    Content = plcProjekt.Bezeichnung + " (" + plcProjekt.Kommentar + " / " + plcProjekt.Sprache + ")",
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = plcProjekt
                };

                rdo.Checked += _mainWindow.RadioButton_Checked;

                tabEigenschaften.StackPanelBezeichnung.Children.Add(rdo);
            }
        }

        public void ProjektStarten(ViewModel.ViewModel viewModel, Button btn)
        {
            switch (btn.Tag)
            {
                case PlcProjektdaten projektdaten:
                    foreach (var job in projektdaten.Jobs)
                    {
                        PlcJobAusfuehren(job, projektdaten, viewModel);
                    }
                    break;
            }
        }
        internal void PlcJobAusfuehren(PlcJobs job, PlcProjektdaten projektdaten, ViewModel.ViewModel viewModel)
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
                case PlcJobs.CmdDateiProjektStarten:
                    AllePlcJobs.ProjektStarten(viewModel, projektdaten.OrdnerDestination, projektdaten.OrdnerProjekt);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(job), job, null);
            }
        }
    }
}