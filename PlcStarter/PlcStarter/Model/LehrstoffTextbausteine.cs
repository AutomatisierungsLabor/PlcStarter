using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace PlcStarter.Model;

public class RootAlleTextbausteine
{
    public EinLehrstoffTextbaustein[] AlleTextbausteine { get; set; }
}
public class EinLehrstoffTextbaustein
{
    public int Id { get; set; }
    // ReSharper disable once UnusedMember.Global
    public string Bezeichnung { get; set; }
    public string UeberschriftH1 { get; set; }
    public string UnterUeberschriftH2 { get; set; }
    public string Inhalt { get; set; }
}

public class LehrstoffTextbausteine
{
    private const string TempJsonFile = "TempTextbausteine.json";
    private readonly EinLehrstoffTextbaustein[] _alletextbausteines;

    public LehrstoffTextbausteine(string jsonZip)
    {
        try
        {
            var zip = ZipFile.OpenRead(jsonZip);
            var zipEntry = zip.Entries[0];
            if (zipEntry.FullName != "json") return;
            if (File.Exists(TempJsonFile)) File.Delete(TempJsonFile);

            zipEntry.ExtractToFile(TempJsonFile);
            var temp = JsonConvert.DeserializeObject<RootAlleTextbausteine>(File.ReadAllText(TempJsonFile));
            _alletextbausteines = temp!.AlleTextbausteine;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public EinLehrstoffTextbaustein GetTextbaustein(int id)
    {
        if (id == 0)
        {
            MessageBox.Show("Textbaustein mit ID=" + id);
            return null;
        }
        if (id > _alletextbausteines.Length)
        {
            MessageBox.Show("Textbaustein mit ID=" + id + " > Länge der Textliste: " + _alletextbausteines.Length);
            return null;
        }

        if (id == _alletextbausteines[id - 1].Id) return _alletextbausteines[id - 1];

        MessageBox.Show("Textbaustein mit falscher ID=" + id + " > Textliste[].id: " + _alletextbausteines[id - 1].Id);
        return null;
    }
}