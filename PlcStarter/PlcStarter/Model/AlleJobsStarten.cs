using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace PlcStarter.Model;

public static partial class AlleJobs
{
    public static void ProjektStarten(ViewModel.VmPlcStarter viewModel, PlcProjektdaten projektdaten, PlcJobs job)
    {
        var fileName = "";
        var workingDirectory = "";

        switch (job)
        {
            case PlcJobs.None: break;
            case PlcJobs.ProjektKopieren: break;
            case PlcJobs.ProjektStarten:
                fileName = "start.cmd";
                workingDirectory = projektdaten.OrdnerstrukturDestinationProjekt;
                break;
            case PlcJobs.DigitalTwinKopieren: break;
            case PlcJobs.DigitalTwinStarten:
                fileName = Path.Combine(projektdaten.OrdnerstrukturDestinationDigitalTwin, projektdaten.ProgrammDigitalTwin);
                workingDirectory = projektdaten.OrdnerstrukturDestinationDigitalTwin;
                break;
            case PlcJobs.FactoryIoKopieren: break;
            case PlcJobs.FactoryIoStarten:
                fileName = Path.Combine(projektdaten.OrdnerstrukturDestinationFactoryIo, "FactoryIOStarten.cmd");
                workingDirectory = projektdaten.OrdnerstrukturDestinationFactoryIo;
                break;
            case PlcJobs.TemplateOrdnerKopieren: break;
            case PlcJobs.DeltaOrdnerKopieren: break;
            default: throw new ArgumentOutOfRangeException(nameof(job), job, null);
        }

        Log.Debug($"Projekt starten: WorkingDirectory:{workingDirectory}; FileName:{fileName}");

        try
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = Path.Combine(workingDirectory, fileName),
                WorkingDirectory = workingDirectory
            };
            process.StartInfo = startInfo;
            process.Start();

        }
        catch (Exception exp)
        {
            Log.Error(exp.ToString());
            MessageBox.Show(exp.ToString());
        }

        viewModel.StringStartButton = "Projekt wurde gestartet";
    }
}