using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model
{
    public class PlcTwinCat : IPlc
    {
        public PlcProjekt PlcProjekte { get; set; }

        private readonly MainWindow _mainWindow;

        public PlcTwinCat(MainWindow mainWindow, Ordner ordnerStrukturen)
        {
            _mainWindow = mainWindow;

            PlcProjekte = JsonConvert.DeserializeObject<PlcProjekt>(File.ReadAllText(ordnerStrukturen.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TwinCat].Source + "\\TwinCatProjektliste.json"));
        }

        public void TabEigenschaftenHinzufuegen()
        {
            _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Plc, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlc, _mainWindow.StackPanelTwinCatPlc, _mainWindow.ButtonStartenTwinCatPlc));
            _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Visu, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcVisu, _mainWindow.StackPanelTwinCatPlcVisu, _mainWindow.ButtonStartenTwinCatPlcVisu));
            _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Nc, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcNc, _mainWindow.StackPanelTwinCatPlcNc, _mainWindow.ButtonStartenTwinCatPlcNc));
            _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.AutoTests, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcTests, _mainWindow.StackPanelTwinCatPlcTests, _mainWindow.ButtonStartenTwinCatPlcTests));
            _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Bug, Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcBugs, _mainWindow.StackPanelTwinCatPlcBugs, _mainWindow.ButtonStartenTwinCatPlcBugs));
        }

        public void AnzeigeUpdaten(TabEigenschaften tabEigenschaften)
        {
            var als = _mainWindow.CheckboxTwinCatAs?.IsChecked != null && (bool)_mainWindow.CheckboxTwinCatAs.IsChecked;
            var awl = _mainWindow.CheckboxTwinCatAwl?.IsChecked != null && (bool)_mainWindow.CheckboxTwinCatAwl.IsChecked;
            var cfc = _mainWindow.CheckboxTwinCatCfc?.IsChecked != null && (bool)_mainWindow.CheckboxTwinCatCfc.IsChecked;
            var cpp = _mainWindow.CheckboxTwinCatCpp?.IsChecked != null && (bool)_mainWindow.CheckboxTwinCatCpp.IsChecked;
            var fup = _mainWindow.CheckboxTwinCatFup?.IsChecked != null && (bool)_mainWindow.CheckboxTwinCatFup.IsChecked;
            var kop = _mainWindow.CheckboxTwinCatKop?.IsChecked != null && (bool)_mainWindow.CheckboxTwinCatKop.IsChecked;
            var st = _mainWindow.CheckboxTwinCatSt?.IsChecked != null && (bool)_mainWindow.CheckboxTwinCatSt.IsChecked;

            foreach (var plcProjekt in PlcProjekte.PlcProjektliste)
            {
                if (tabEigenschaften.PlcKategorie != plcProjekt.Kategorie) continue;

                if (!als && plcProjekt.Sprache == PlcSprachen.As) continue;
                if (!awl && plcProjekt.Sprache == PlcSprachen.Awl) continue;
                if (!cfc && plcProjekt.Sprache == PlcSprachen.Cfc) continue;
                if (!cpp && plcProjekt.Sprache == PlcSprachen.Cpp) continue;
                if (!fup && plcProjekt.Sprache == PlcSprachen.Fup) continue;
                if (!kop && plcProjekt.Sprache == PlcSprachen.Kop) continue;
                if (!st && plcProjekt.Sprache == PlcSprachen.St) continue;

                plcProjekt.BrowserBezeichnung = tabEigenschaften.BrowserBezeichnung;
                plcProjekt.ButtonBezeichnung = tabEigenschaften.ButtonBezeichnung;

                var rdo = new RadioButton
                {
                    GroupName = "TwinCAT",
                    Name = plcProjekt.Bezeichnung,
                    FontSize = 14,
                    Content = plcProjekt.Bezeichnung + " (" + plcProjekt.Kommentar + " / " + plcProjekt.Sprache + ")",
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = plcProjekt
                };

                rdo.Checked += _mainWindow.RadioButton_Checked;

                _ = tabEigenschaften.StackPanelBezeichnung.Children.Add(rdo);
            }
        }
    }
}