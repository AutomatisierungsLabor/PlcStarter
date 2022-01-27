using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model;

public class PlcTiaPortal : IPlc
{
    public PlcProjekt PlcProjekte { get; set; }

    private readonly MainWindow _mainWindow;
    private readonly Ordner _ordnerStruktur;

    public PlcTiaPortal(MainWindow mainWindow, Ordner ordnerStrukturen)
    {
        _mainWindow = mainWindow;
        _ordnerStruktur = ordnerStrukturen;

        PlcProjekte = JsonConvert.DeserializeObject<PlcProjekt>(File.ReadAllText(_ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TiaPortal].Source + "\\TiaPortalProjektliste.json"));
        PlcProjekte?.AufFehlerTesten();
    }

    public void TabEigenschaftenHinzufuegen()
    {
        _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Plc, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlc, _mainWindow.StackPanelTiaPortalPlc, _mainWindow.ButtonStartenTiaPortalPlc));
        _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Hmi, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcHmi, _mainWindow.StackPanelTiaPortalPlcHmi, _mainWindow.ButtonStartenTiaPortalPlcHmi));
        _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.FactoryIo, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcFio, _mainWindow.StackPanelTiaPortalPlcFio, _mainWindow.ButtonStartenTiaPortalPlcFio));
        _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.AutoTests, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcTests, _mainWindow.StackPanelTiaPortalPlcTests, _mainWindow.ButtonStartenTiaPortalPlcTests));
        _mainWindow.AllePlc.AlleTabEigenschaften.Add(new TabEigenschaften(PlcKategorie.Bug, Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcBugs, _mainWindow.StackPanelTiaPortalPlcBugs, _mainWindow.ButtonStartenTiaPortalPlcBugs));
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
            plcProjekt.ButtonBezeichnung = tabEigenschaften.ButtonBezeichnung;
            
            plcProjekt.OrdnerstrukturSourceProjekt = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TiaPortal].Source;
            plcProjekt.OrdnerstrukturSourceDigitalTwin= _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.DigitalTwin].Source;
            plcProjekt.OrdnerstrukturSourceFactoryIo = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.FactoryIo].Source;
            
            plcProjekt.OrdnerstrukturDestinationProjekt = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TiaPortal].Destination;
            plcProjekt.OrdnerstrukturDestinationDigitalTwin = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.DigitalTwin].Destination;
            plcProjekt.OrdnerstrukturDestinationFactoryIo = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.FactoryIo].Destination;

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

            _ = tabEigenschaften.StackPanelBezeichnung.Children.Add(rdo);
        }
    }
}