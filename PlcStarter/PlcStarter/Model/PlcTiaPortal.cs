﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model;

public class PlcTiaPortal : IPlc
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public PlcProjekt PlcProjekte { get; set; }

    private readonly MainWindow _mainWindow;
    private readonly OrdnerDaten[] _ordnerDaten;

    public PlcTiaPortal(MainWindow mainWindow, OrdnerDaten[] ordnerDaten)
    {
        _mainWindow = mainWindow;
        _ordnerDaten=ordnerDaten;

        var pfad = Path.Combine(_ordnerDaten[(int)OrdnerBezeichnungen.TiaPortal].Source, "TiaPortalProjektliste.json");
        Log.Debug(pfad);

        try
        {
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

            plcProjekt.OrdnerstrukturSourceProjekt = _ordnerDaten[(int)OrdnerBezeichnungen.TiaPortal].Source;
            plcProjekt.OrdnerstrukturSourceDigitalTwin = _ordnerDaten[(int)OrdnerBezeichnungen.DigitalTwin].Source;
            plcProjekt.OrdnerstrukturSourceFactoryIo = _ordnerDaten[(int)OrdnerBezeichnungen.FactoryIo].Source;

            plcProjekt.OrdnerstrukturDestinationProjekt = _ordnerDaten[(int)OrdnerBezeichnungen.TiaPortal].Destination;
            plcProjekt.OrdnerstrukturDestinationDigitalTwin = _ordnerDaten[(int)OrdnerBezeichnungen.DigitalTwin].Destination;
            plcProjekt.OrdnerstrukturDestinationFactoryIo = _ordnerDaten[(int)OrdnerBezeichnungen.FactoryIo].Destination;

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
    public void StrukturTesten()
    {
        foreach (var projekte in PlcProjekte.PlcProjektliste)
        {
            if (string.IsNullOrEmpty(projekte.Bezeichnung)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Bezeichnung fehlt!");
            if (string.IsNullOrEmpty(projekte.Kommentar)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Kommentar fehlt!");
            if (string.IsNullOrEmpty(projekte.ProjektDatei)) FehlerAnzeigen(projekte.Bezeichnung, projekte.ProjektDatei, "ProjektDatei fehlt!");
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
    private static void FehlerAnzeigen(string bezeichnung, string kommentar, string fehlermeldung) => MessageBox.Show($"TiaPortal: {bezeichnung} - {kommentar} -->  {fehlermeldung}");
}