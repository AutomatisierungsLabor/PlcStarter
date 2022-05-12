using PlcStarter.Model;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlcStarter;

public partial class MainWindow
{
    public void AnzeigeUpdaten(Steuerungen aktuelleSteuerung)
    {
        if (AllePlc.AlleTabEigenschaften == null) return;

        foreach (var tabEigenschaften in AllePlc.AlleTabEigenschaften.Where(tabEigenschaften => tabEigenschaften.Steuerungen == aktuelleSteuerung))
        {
            tabEigenschaften.StackPanelBezeichnung?.Children.Clear();

            switch (tabEigenschaften.Steuerungen)
            {
                case Steuerungen.Logo: AllePlc.Logo.AnzeigeUpdaten(tabEigenschaften); break;
                case Steuerungen.TiaPortal: AllePlc.TiaPortal.AnzeigeUpdaten(tabEigenschaften); break;
                case Steuerungen.TwinCat: AllePlc.TwinCat.AnzeigeUpdaten(tabEigenschaften); break;
                default: throw new ArgumentOutOfRangeException(tabEigenschaften.ToString());
            }
        }
    }
    public void RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is not RadioButton { Tag: PlcProjektdaten projektdaten }) return;

        VmPlcStarter.StringStartButton = "Projekt starten";
        VmPlcStarter.BrushStartButton = Brushes.LawnGreen;

        LadeAlleTextbausteine(projektdaten);

        projektdaten.ButtonBezeichnung.Tag = projektdaten;
        PlcProjektdaten = projektdaten;
    }
    private void LadeAlleTextbausteine(PlcProjektdaten plcProjektdaten)
    {
        var html = new StringBuilder();
        foreach (var textbausteine in plcProjektdaten.Textbausteine)
        {
            var einLehrstoffTextbaustein = LehrstoffTextbausteine.GetTextbaustein(textbausteine.BausteinId);
            var inhalt = Encoding.UTF8.GetString(Convert.FromBase64String(einLehrstoffTextbaustein.Inhalt));

            switch (textbausteine.WasAnzeigen)
            {
                case TextbausteineAnzeigen.NurInhalt:
                    html.Append(inhalt);
                    break;

                case TextbausteineAnzeigen.H1Inhalt:
                    html.Append("<H1>" + textbausteine.PrefixH1 + einLehrstoffTextbaustein.UeberschriftH1 + "</H1>");
                    html.Append(inhalt);
                    break;

                case TextbausteineAnzeigen.H1H2Inhalt:
                    html.Append("<H1>" + textbausteine.PrefixH1 + einLehrstoffTextbaustein.UeberschriftH1 + "</H1>");
                    html.Append("<H2>" + textbausteine.PrefixH2 + einLehrstoffTextbaustein.UnterUeberschriftH2 + "</H2>");
                    html.Append(inhalt);
                    break;

                case TextbausteineAnzeigen.H2Inhalt:
                    html.Append("<H2>" + textbausteine.PrefixH2 + einLehrstoffTextbaustein.UnterUeberschriftH2 + "</H2>");
                    html.Append(inhalt);
                    break;

                case TextbausteineAnzeigen.H1H2TestInhalt:
                    html.Append("<H1>" + textbausteine.PrefixH1 + einLehrstoffTextbaustein.UeberschriftH1 + "</H1>");
                    html.Append("<H2>" + textbausteine.PrefixH2 + einLehrstoffTextbaustein.UnterUeberschriftH2 + "</H2>");
                    html.Append("<H2> #" + textbausteine.Test + "</H2>");
                    html.Append(inhalt);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(textbausteine.WasAnzeigen));
            }
        }

        plcProjektdaten.BrowserBezeichnung.NavigateToString(html.ToString());
    }
}