using PlcStarter.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlcStarter;

public partial class MainWindow
{
    private class LehrstoffTextbaustein
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int Id { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Bezeichnung { get; set; }
        public string UeberschriftH1 { get; set; }
        public string UnterUeberschriftH2 { get; set; }
        public string Inhalt { get; set; }
    }

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

        LadeAlleTextbausteine(projektdaten).GetAwaiter();

        projektdaten.ButtonBezeichnung.Tag = projektdaten;
        PlcProjektdaten = projektdaten;
    }

    private async Task LadeAlleTextbausteine(PlcProjektdaten plcProjektdaten)
    {
        var html = new StringBuilder();
        foreach (var textbausteine in plcProjektdaten.Textbausteine)
        {
            var b = await TextbasteineLaden(textbausteine.BausteinId);

            switch (textbausteine.WasAnzeigen)
            {
                case TextbausteineAnzeigen.NurInhalt:
                    html.Append(b.Inhalt);
                    break;

                case TextbausteineAnzeigen.H1Inhalt:
                    html.Append("<H1>" + textbausteine.PrefixH1 + b.UeberschriftH1 + "</H1>");
                    html.Append(b.Inhalt);
                    break;

                case TextbausteineAnzeigen.H1H2Inhalt:
                    html.Append("<H1>" + textbausteine.PrefixH1 + b.UeberschriftH1 + "</H1>");
                    html.Append("<H2>" + textbausteine.PrefixH2 + b.UnterUeberschriftH2 + "</H2>");
                    html.Append(b.Inhalt);
                    break;

                case TextbausteineAnzeigen.H2Inhalt:
                    html.Append("<H2>" + textbausteine.PrefixH2 + b.UnterUeberschriftH2 + "</H2>");
                    html.Append(b.Inhalt);
                    break;

                case TextbausteineAnzeigen.H1H2TestInhalt:
                    html.Append("<H1>" + textbausteine.PrefixH1 + b.UeberschriftH1 + "</H1>");
                    html.Append("<H2>" + textbausteine.PrefixH2 + b.UnterUeberschriftH2 + "</H2>");
                    html.Append("<H2> #" + textbausteine.Test + "</H2>");
                    html.Append(b.Inhalt);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(textbausteine.WasAnzeigen));
            }
        }

        plcProjektdaten.BrowserBezeichnung.NavigateToString(html.ToString());

        await File.WriteAllTextAsync("browser.html", html.ToString());

    }
    private async Task<LehrstoffTextbaustein> TextbasteineLaden(int id)
    {
        var baustein = new LehrstoffTextbaustein();
        await GetTextbausteine.ReadTextbaustein(id.ToString());
        baustein.Id = id;
        baustein.Bezeichnung = GetTextbausteine.GetBezeichnung();
        baustein.UeberschriftH1 = GetTextbausteine.GetUeberschriftH1();
        baustein.UnterUeberschriftH2 = GetTextbausteine.GetUnterUeberschriftH2();
        baustein.Inhalt = GetTextbausteine.GetInhalt();

        return baustein;
    }
}