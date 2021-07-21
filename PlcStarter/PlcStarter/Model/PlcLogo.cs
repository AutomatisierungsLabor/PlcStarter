﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Automation;
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
            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Plc, Steuerungen.Logo,
                _mainWindow.WebLogoPlc, _mainWindow.StackPanelLogoPlc, _mainWindow.ButtonStartenLogoPlc));

            _mainWindow.AlleDaten.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Bug, Steuerungen.Logo,
                _mainWindow.WebLogoPlcBugs, _mainWindow.StackPanelLogoPlcBugs, _mainWindow.ButtonStartenLogoPlcBugs));
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
                logoProjekt.Source = _ordnerSource;
                logoProjekt.Destination = _ordnerDestination;

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
            if (btn.Tag is Logo8Projektdaten projektdaten)
            {
                switch (projektdaten.Job1)
                {
                    case PlcJobs.None: break;
                    case PlcJobs.OrdnerInhaltKopieren: AllePlcJobs.OrdnerKopieren(viewModel, projektdaten.Source, projektdaten.Destination, projektdaten.Ordner); break;
                    case PlcJobs.CmdDateiAusfuehren: break;
                    default: break;
                }

                switch (projektdaten.Job2)
                { 
                    case PlcJobs.None: break;
                    case PlcJobs.OrdnerInhaltKopieren:  break;
                   case PlcJobs.CmdDateiAusfuehren: AllePlcJobs.ProjektStarten(viewModel, projektdaten.Ordner);break;
                    default: break;
                }
            }
        }
    }
}