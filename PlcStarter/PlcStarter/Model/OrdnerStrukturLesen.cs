using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace PlcStarter.Model;

public class OrdnerStrukturLesen
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
    private OrdnerDaten[] _ordnerDaten;

    internal void GetOrdnerConfig(string pfad)
    {
        Log.Debug(pfad);
        try
        {
            var ordnerStruktur = JsonConvert.DeserializeObject<Ordner>(File.ReadAllText(pfad));
            _ordnerDaten = ordnerStruktur!.OrdnerBezeichnungen;
        }
        catch (Exception e)
        {
            Log.Debug(e.ToString());
            MessageBox.Show(e.ToString());
            throw;
        }

        if (_ordnerDaten != null && _ordnerDaten[(int)OrdnerBezeichnungen.DigitalTwin].Steuerung != "DigitalTwin") throw new Exception("Ordner in der falschen Reihenfolge !");
        if (_ordnerDaten != null && _ordnerDaten[(int)OrdnerBezeichnungen.FactoryIo].Steuerung != "FactoryIO") throw new Exception("Ordner in der falschen Reihenfolge !");
        if (_ordnerDaten != null && _ordnerDaten[(int)OrdnerBezeichnungen.Logo].Steuerung != "Logo") throw new Exception("Ordner in der falschen Reihenfolge !");
        if (_ordnerDaten != null && _ordnerDaten[(int)OrdnerBezeichnungen.TiaPortal].Steuerung != "TiaPortal") throw new Exception("Ordner in der falschen Reihenfolge !");
        if (_ordnerDaten != null && _ordnerDaten[(int)OrdnerBezeichnungen.TwinCat].Steuerung != "TwinCAT") throw new Exception("Ordner in der falschen Reihenfolge !");
    }

    public OrdnerDaten[] GetLogoOrdner() => _ordnerDaten;
    public OrdnerDaten[] GetTiaPortalOrdner() => _ordnerDaten;
    public OrdnerDaten[] GetTwinCatOrdner() => _ordnerDaten;
}