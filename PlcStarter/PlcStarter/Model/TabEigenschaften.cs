using System.Windows.Controls;

namespace PlcStarter.Model;

public class TabEigenschaften
{
    public PlcKategorie PlcKategorie { get; set; }
    public Steuerungen Steuerungen { get; set; }
    public WebBrowser BrowserBezeichnung { get; set; }
    public StackPanel StackPanelBezeichnung { get; set; }
    public Button ButtonBezeichnung { get; set; }

    public TabEigenschaften(PlcKategorie plcKategorie, Steuerungen steuerungen, WebBrowser browserBezeichnung, StackPanel stackPanelBezeichnung, Button buttonBezeichnung)
    {
        PlcKategorie = plcKategorie;
        Steuerungen = steuerungen;
        BrowserBezeichnung = browserBezeichnung;
        StackPanelBezeichnung = stackPanelBezeichnung;
        ButtonBezeichnung = buttonBezeichnung;
    }
}