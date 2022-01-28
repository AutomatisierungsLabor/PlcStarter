using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model;

public class PlcProjekt
{
    public PlcProjekt(ObservableCollection<PlcProjektdaten> plcProjektliste) => PlcProjektliste = plcProjektliste;
    public ObservableCollection<PlcProjektdaten> PlcProjektliste { get; set; }

    public void AufFehlerTesten()
    {
        foreach (var plcProjektdaten in PlcProjektliste)
        {
            if (plcProjektdaten.Jobs.Length == 0)
            {
                MessageBox.Show("json Problem:" + "keine Jobs vorhanden!" + plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion);
            }

            foreach (var plcJob in plcProjektdaten.Jobs)
            {
                switch (plcJob)
                {
                    case PlcJobs.None: break;

                    case PlcJobs.ProjektKopieren:
                        if (plcProjektdaten.OrdnerPlc is { Length: >= 2 }) continue;
                        MessageBox.Show("json Problem (SorceOrdner):" + "OrdnerPlc fehlt/leer " + "+ plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion");
                        break;
                    case PlcJobs.ProjektStarten: break;
                    case PlcJobs.DigitalTwinKopieren:
                        if (plcProjektdaten.OrdnerTemplateDigitalTwin is { Length: >= 2 }) continue;
                        if (plcProjektdaten.OrdnerDeltaDigitalTwin is { Length: >= 2 }) continue;
                        MessageBox.Show("json Problem (Digital Twin):" + "OrdnerDigitalTwin fehlt/leer " + "+ plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion");
                        break;
                    case PlcJobs.DigitalTwinStarten: break;
                    case PlcJobs.FactoryIoKopieren:
                        if (plcProjektdaten.OrdnerFactoryIo is { Length: >= 2 }) continue;
                        MessageBox.Show("json Problem (Factory I/O):" + "OrdnerFactoryIo fehlt/leer " + "+ plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion");
                        break;
                    case PlcJobs.FactoryIoStarten: break;
                    case PlcJobs.TemplateOrdnerKopieren:
                        if (plcProjektdaten.OrdnerTwinCatTemplate is { Length: >= 2 }) continue;
                        MessageBox.Show("json Problem (Template):" + "OrdnerTemplate fehlt/leer " + "+ plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion");
                        break;
                    case PlcJobs.DeltaOrdnerKopieren: break;
                    default: throw new ArgumentOutOfRangeException(plcJob.ToString());
                }
            }
        }
    }
}

public class PlcProjektdaten
{
    public string Bezeichnung { get; set; }
    public string Kommentar { get; set; }
    public PlcSoftwareVersion SoftwareVersion { get; set; }
    public Button ButtonBezeichnung { get; set; }
    public WebBrowser BrowserBezeichnung { get; set; }

    public string OrdnerstrukturSourceProjekt { get; set; }
    public string OrdnerstrukturSourceDigitalTwin { get; set; }
    public string OrdnerstrukturSourceFactoryIo { get; set; }

    public string OrdnerstrukturDestinationProjekt { get; set; }
    public string OrdnerstrukturDestinationDigitalTwin { get; set; }
    public string OrdnerstrukturDestinationFactoryIo { get; set; }

    public string OrdnerTwinCatTemplate { get; set; }
    public string OrdnerPlc { get; set; }
    public string OrdnerTemplateDigitalTwin { get; set; }
    public string OrdnerDeltaDigitalTwin { get; set; }
    public string OrdnerFactoryIo { get; set; }
    public PlcSprachen Sprache { get; set; }
    public PlcKategorie Kategorie { get; set; }
    public PlcJobs[] Jobs { get; set; }
}