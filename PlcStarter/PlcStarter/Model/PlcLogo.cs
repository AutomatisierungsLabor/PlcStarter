using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model;

public class PlcLogo : IPlc
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public PlcProjekt PlcProjekte { get; set; }
    private readonly OrdnerDaten[] _ordnerDaten;
    private readonly MainWindow _mainWindow;

    public PlcLogo(MainWindow mainWindow, OrdnerDaten[] ordnerDaten)
    {
        _mainWindow = mainWindow;
        _ordnerDaten = ordnerDaten;

        var pfad = Path.Combine(_ordnerDaten[(int)OrdnerBezeichnungen.Logo].Source, "LogoProjektliste.json");
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

        foreach (var plcProjekt in PlcProjekte!.PlcProjektliste)
        {
            plcProjekt.Startprogramm = _ordnerDaten[(int)OrdnerBezeichnungen.Logo].Startprogramm;
        }

        PlcProjekte!.AufFehlerTesten();
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
            plcProjekt.OrdnerstrukturDestinationProjekt = _ordnerDaten[(int)OrdnerBezeichnungen.Logo].Destination;
            plcProjekt.OrdnerstrukturSourceProjekt = _ordnerDaten[(int)OrdnerBezeichnungen.Logo].Source;

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
    public void StrukturTesten()
    {
        foreach (var projekte in PlcProjekte.PlcProjektliste)
        {
            if (string.IsNullOrEmpty(projekte.Bezeichnung)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Bezeichnung fehlt!");
            if (string.IsNullOrEmpty(projekte.Kommentar)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Kommentar fehlt!");
            if (string.IsNullOrEmpty(projekte.ProjektDatei)) FehlerAnzeigen(projekte.Bezeichnung, projekte.ProjektDatei, "ProjektDatei fehlt!");
            if (string.IsNullOrEmpty(projekte.Startprogramm)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Startprogramm, "Startprogramm fehlt!");
            if (projekte.SoftwareVersion == 0) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "SoftwareVersion fehlt!");
            if (string.IsNullOrEmpty(projekte.OrdnerPlc)) FehlerAnzeigen(projekte.Bezeichnung, projekte.Kommentar, "Ordner Plc fehlt!");
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
    private static void FehlerAnzeigen(string bezeichnung, string kommentar, string fehlermeldung) => MessageBox.Show($"Logo: {bezeichnung} - {kommentar} -->  {fehlermeldung}");
}