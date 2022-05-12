using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model;

public class PlcTwinCat : IPlc
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public PlcProjekt PlcProjekte { get; set; }

    private readonly MainWindow _mainWindow;
    private readonly Ordner _ordnerStruktur;

    public PlcTwinCat(MainWindow mainWindow, Ordner ordnerStrukturen)
    {
        _mainWindow = mainWindow;
        _ordnerStruktur = ordnerStrukturen;
        var pfad = Path.Combine(_ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TwinCat].Source, "TwinCatProjektliste.json");

        try
        {
            Log.Debug(pfad);
            PlcProjekte = JsonConvert.DeserializeObject<PlcProjekt>(File.ReadAllText(pfad));
        }
        catch (Exception e)
        {
            Log.Debug(e.ToString());
            MessageBox.Show(e.ToString());
            throw;
        }

        PlcProjekte?.AufFehlerTesten();
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
            plcProjekt.OrdnerstrukturDestinationProjekt = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TwinCat].Destination;
            plcProjekt.OrdnerstrukturSourceProjekt = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.TwinCat].Source;
            plcProjekt.OrdnerstrukturDestinationDigitalTwin = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.DigitalTwin].Source;
            plcProjekt.OrdnerstrukturDestinationFactoryIo = _ordnerStruktur.OrdnerBezeichnungen[(int)OrdnerBezeichnungen.FactoryIo].Source;

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
    public void StrukturTesten()
    {
        foreach (var projekte in PlcProjekte.PlcProjektliste)
        {
            if (string.IsNullOrEmpty(projekte.Bezeichnung)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Bezeichnung fehlt!");
            if (string.IsNullOrEmpty(projekte.Kommentar)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Kommentar fehlt!");
            if (projekte.SoftwareVersion == 0) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "SoftwareVersion fehlt!");

            if (string.IsNullOrEmpty(projekte.OrdnerTwinCatTemplate)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Ordner TwinCatTemplate fehlt!");
            if (string.IsNullOrEmpty(projekte.OrdnerPlc)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Ordner Plc fehlt!");
            if (string.IsNullOrEmpty(projekte.OrdnerTemplateDigitalTwin)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Ordner TemplateDigitalTwin fehlt!");
            if (string.IsNullOrEmpty(projekte.OrdnerDeltaDigitalTwin)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Ordner DeltaDigitalTwin fehlt!");
            if (string.IsNullOrEmpty(projekte.OrdnerFactoryIo)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Ordner FactoryIo fehlt!");

            if (string.IsNullOrEmpty(projekte.ProgrammDigitalTwin)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "ProgrammDigitalTwinfehlt!");
            if (projekte.OrdnerDeltaDigitalTwin == "-" && projekte.ProgrammDigitalTwin != "-") FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "DigitalTwin: Ordner und exe stimmen nicht überein!");

            if (projekte.Sprache == 0) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Sprache fehlt!");
            if (projekte.Kategorie == 0) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Kategorie fehlt!");
            if (projekte.Jobs.Length < 2) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Jobs fehlen!");
            if (projekte.Textbausteine.Length == 0) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Textbausteine fehlen!");

            foreach (var textbaustein in projekte.Textbausteine)
            {
                if (textbaustein.PrefixH1 == null) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Prefix H1 fehlt!");
                if (textbaustein.PrefixH2 == null) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Prefix H2 fehlt!");
            }
        }
    }
    private static void FehlerAnzeigen(string bezeichnung, string kommentar, string fehlermeldung) => MessageBox.Show($"TwinCAT: {bezeichnung} - {kommentar} -->  {fehlermeldung}");
}