namespace PlcStarter.Model;

public class Ordner
{
    public OrdnerDaten[] OrdnerBezeichnungen { get; set; }
}
public class OrdnerDaten
{
    public string Steuerung { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
    public string Startprogramm { get; set; }

    public OrdnerDaten()
    {
        Steuerung = "";
        Source = "";
        Destination = "";
        Startprogramm = "";
    }
}