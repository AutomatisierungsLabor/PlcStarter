using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model
{
    public class PlcLogo : IPlc
    {
        public PlcProjekt PlcProjekte { get; set; }
        private readonly Ordner _ordnerStruktur;
        private readonly MainWindow _mainWindow;

        public PlcLogo(MainWindow mainWindow, Ordner ordnerStrukturen)
        {
            _mainWindow = mainWindow;
            _ordnerStruktur = ordnerStrukturen;

            PlcProjekte = JsonConvert.DeserializeObject<PlcProjekt>(File.ReadAllText(ordnerStrukturen.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.Logo].Source + "\\LogoProjektliste.json"));
        }

        public void TabEigenschaftenHinzufuegen()
        {
            _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Plc, Steuerungen.Logo, _mainWindow.WebLogoPlc, _mainWindow.StackPanelLogoPlc, _mainWindow.ButtonStartenLogoPlc));
            _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Bug, Steuerungen.Logo, _mainWindow.WebLogoPlcBugs, _mainWindow.StackPanelLogoPlcBugs, _mainWindow.ButtonStartenLogoPlcBugs));
        }

        public void AnzeigeUpdaten(TabEigenschaften tabEigenschaften)
        {
            var fup = _mainWindow.CheckboxLogoFup?.IsChecked != null && (bool)_mainWindow.CheckboxLogoFup.IsChecked;
            var kop = _mainWindow.CheckboxLogoKop?.IsChecked != null && (bool)_mainWindow.CheckboxLogoKop.IsChecked;

            foreach (var plcProjekt in PlcProjekte.PlcProjektliste)
            {
                if (tabEigenschaften.PlcKategorie != plcProjekt.Kategorie) continue;

                if (!fup && plcProjekt.Sprache == PlcSprachen.Fup) continue;
                if (!kop && plcProjekt.Sprache == PlcSprachen.Kop) continue;

                plcProjekt.BrowserBezeichnung = tabEigenschaften.BrowserBezeichnung;
                plcProjekt.ButtonBezeichnung = tabEigenschaften.ButtonBezeichnung;
                plcProjekt.OrdnerStrukturDestination = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.Logo].Destination;
                plcProjekt.OrdnerStrukturPlc = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.Logo].Source;

                var rdo = new RadioButton
                {
                    GroupName = "Logo",
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