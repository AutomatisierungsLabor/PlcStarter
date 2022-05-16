using System;
using System.IO;

namespace PlcStarter.Model;

public static partial class AlleJobs
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public static void PlcJobAusfuehren(PlcJobs job, PlcProjektdaten projektdaten, ViewModel.VmPlcStarter viewModel)
    {
        switch (job)
        {
            case PlcJobs.None: break;

            case PlcJobs.ProjektKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceProjekt, projektdaten.OrdnerPlc), Path.Combine(projektdaten.OrdnerstrukturDestinationProjekt), "Projektdateien werden kopiert", "Projekt wurde kopiert");
                break;

            case PlcJobs.ProjektStarten:
                ProjektStarten(viewModel, projektdaten, job);
                break;

            case PlcJobs.DigitalTwinKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceDigitalTwin, projektdaten.OrdnerTemplateDigitalTwin), Path.Combine(projektdaten.OrdnerstrukturDestinationDigitalTwin), "Digital Twin wird kopiert (Template)", "Digital Twin wurde kopiert (Template)");
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceDigitalTwin, projektdaten.OrdnerDeltaDigitalTwin), Path.Combine(projektdaten.OrdnerstrukturDestinationDigitalTwin), "Digital Twin wird kopiert (Delta)", "Digital Twin wurde kopiert (Delta)");
                break;
            case PlcJobs.DigitalTwinStarten:
                ProjektStarten(viewModel, projektdaten, job);
                break;

            case PlcJobs.FactoryIoKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceFactoryIo, projektdaten.OrdnerFactoryIo), Path.Combine(projektdaten.OrdnerstrukturDestinationFactoryIo), "Factory I/O wird kopiert", "Factory I/O wurde kopiert");
                break;
            case PlcJobs.FactoryIoStarten:
                ProjektStarten(viewModel, projektdaten, job);
                break;

            case PlcJobs.TemplateOrdnerKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceProjekt, projektdaten.OrdnerTwinCatTemplate), Path.Combine(projektdaten.OrdnerstrukturDestinationProjekt), "Projekt Template wird kopiert", "Projekt Template wurde kopiert");
                break;
            case PlcJobs.DeltaOrdnerKopieren:
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceProjekt, projektdaten.OrdnerPlc), Path.Combine(projektdaten.OrdnerstrukturDestinationProjekt), "Projekt Delta wird kopiert", "Projekt Delta wurde kopiert");
                break;

            default: throw new ArgumentOutOfRangeException(nameof(job), job, null);
        }
    }
}