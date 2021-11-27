using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model
{
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
                    MessageBox.Show("json Problem:" + "keine Jobs vorhanden!"
                                                     + plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion);
                }

                foreach (var plcJob in plcProjektdaten.Jobs)
                {
                    switch (plcJob)
                    {
                        case PlcJobs.None: break;
                        case PlcJobs.SorceOrdnerErstellen:
                            if (plcProjektdaten.OrdnerPlc is { Length: >= 2 }) continue;

                            MessageBox.Show("json Problem (SorceOrdner):" + "OrdnerPlc fehlt/leer " +
                                            "+ plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion");
                            break;
                        case PlcJobs.ProjektKopieren: break;
                        case PlcJobs.CmdDateiProjektStarten: break;
                        case PlcJobs.DigitalTwinKopieren:
                            if (plcProjektdaten.OrdnerDigitalTwin is { Length: >= 2 }) continue;

                            MessageBox.Show("json Problem (Digital Twin):" + "OrdnerDigitalTwin fehlt/leer " +
                                            "+ plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion");
                            break;
                        case PlcJobs.DigitalTwinStarten: break;
                        case PlcJobs.FactoryIoKopieren:
                            if (plcProjektdaten.OrdnerFactoryIo is { Length: >= 2 }) continue;

                            MessageBox.Show("json Problem (Factory I/O):" + "OrdnerFactoryIo fehlt/leer " +
                                                "+ plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion");
                            break;
                        case PlcJobs.FactoryIoStarten: break;
                        case PlcJobs.TemplateOrdnerKopieren:
                            if (plcProjektdaten.OrdnerTemplate is { Length: >= 2 }) continue;

                            MessageBox.Show("json Problem (Template):" + "OrdnerTemplate fehlt/leer " +
                                            "+ plcProjektdaten.Bezeichnung + plcProjektdaten.SoftwareVersion");
                            break;
                        case PlcJobs.DiffOrdnerKopieren: break;
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
        public string OrdnerStrukturDestination { get; set; }
        public string OrdnerStrukturPlc { get; set; }
        public string OrdnerStrukturDigitalTwin { get; set; }
        public string OrdnerStrukturFactoryIo { get; set; }
        public string OrdnerTemplate { get; set; }
        public string OrdnerPlc { get; set; }
        public string OrdnerDigitalTwin { get; set; }
        public string OrdnerFactoryIo { get; set; }
        public PlcSprachen Sprache { get; set; }
        public PlcKategorie Kategorie { get; set; }
        public PlcJobs[] Jobs { get; set; }
    }
}