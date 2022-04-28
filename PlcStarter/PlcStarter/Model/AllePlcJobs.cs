using NETCore.Encrypt;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PlcStarter.Model;

public static class AllePlcJobs
{
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
                var nameBatchDatei = "ProjektStarten.cmd";
                if (projektdaten.SoftwareVersion == PlcSoftwareVersion.TiaPortalV17Sp1) nameBatchDatei = "ProjektStartenV17.cmd";

                ProjektStarten(viewModel, Path.Combine(projektdaten.OrdnerstrukturDestinationProjekt, nameBatchDatei), Path.Combine(projektdaten.OrdnerstrukturDestinationProjekt));
                break;

            case PlcJobs.DigitalTwinKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceDigitalTwin, projektdaten.OrdnerTemplateDigitalTwin), Path.Combine(projektdaten.OrdnerstrukturDestinationDigitalTwin), "Digital Twin wird kopiert (Template)", "Digital Twin wurde kopiert (Template)");
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceDigitalTwin, projektdaten.OrdnerDeltaDigitalTwin), Path.Combine(projektdaten.OrdnerstrukturDestinationDigitalTwin), "Digital Twin wird kopiert (Delta)", "Digital Twin wurde kopiert (Delta)");
                break;
            case PlcJobs.DigitalTwinStarten:
                ProjektStarten(viewModel, Path.Combine(projektdaten.OrdnerstrukturDestinationDigitalTwin, projektdaten.ProgrammDigitalTwin), Path.Combine(projektdaten.OrdnerstrukturDestinationDigitalTwin));
                break;

            case PlcJobs.FactoryIoKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, Path.Combine(projektdaten.OrdnerstrukturSourceFactoryIo, projektdaten.OrdnerFactoryIo), Path.Combine(projektdaten.OrdnerstrukturDestinationFactoryIo), "Factory I/O wird kopiert", "Factory I/O wurde kopiert");
                break;
            case PlcJobs.FactoryIoStarten:
                ProjektStarten(viewModel, Path.Combine(projektdaten.OrdnerstrukturDestinationFactoryIo, "FactoryIoStarten.cmd"), Path.Combine(projektdaten.OrdnerstrukturDestinationFactoryIo));
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

    public static void ProjektOrdnerKopieren(ViewModel.VmPlcStarter viewModel, string quelle, string ziel, string kommentarAnfang, string kommentarEnde)
    {
        viewModel.BrushStartButton = Brushes.Yellow;

        try
        {
            viewModel.StringStartButton = kommentarAnfang;
            Copy(quelle, ziel);
            viewModel.StringStartButton = kommentarEnde;
        }
        catch (Exception exp)
        {
            MessageBox.Show(exp.ToString());
        }

        viewModel.BrushStartButton = Brushes.LightGray;
    }

    public static void ProjektStarten(ViewModel.VmPlcStarter viewModel, string cmdFile, string workingDirectory)
    {
        var proc = new Process
        {
            StartInfo =
            {
                FileName = cmdFile,
                WorkingDirectory = workingDirectory
            }
        };
        proc.Start();

        viewModel.StringStartButton = "Projekt wurde gestartet";
    }
    private static void OrdnerErstellen(ViewModel.VmPlcStarter viewModel, PlcProjektdaten projektdaten, PlcJobs plcJobs)
    {
        viewModel.BrushStartButton = Brushes.Yellow;
        var ordner = projektdaten.OrdnerstrukturDestinationProjekt;

        switch (plcJobs)
        {
            case PlcJobs.None: break;
            case PlcJobs.ProjektKopieren: break;
            case PlcJobs.ProjektStarten: break;
            case PlcJobs.DigitalTwinKopieren:
            case PlcJobs.DigitalTwinStarten: ordner = projektdaten.OrdnerstrukturDestinationDigitalTwin; break;
            case PlcJobs.FactoryIoKopieren:
            case PlcJobs.FactoryIoStarten: ordner = projektdaten.OrdnerstrukturDestinationFactoryIo; break;
            case PlcJobs.TemplateOrdnerKopieren: break;
            case PlcJobs.DeltaOrdnerKopieren: break;

            default: throw new ArgumentOutOfRangeException(nameof(plcJobs), plcJobs, null);
        }


        try
        {
            viewModel.StringStartButton = "Zielordner wird gelöscht";

            if (Directory.Exists(ordner)) Directory.Delete(ordner, true);
            if (ordner != null) Directory.CreateDirectory(ordner);

            viewModel.StringStartButton = "Zielordner wurde erstellt";
        }
        catch (Exception exp)
        {
            MessageBox.Show(exp.ToString());
        }

        viewModel.BrushStartButton = Brushes.LightGray;
    }
    internal static void Copy(string sourceDirectory, string targetDirectory)
    {
        var diSource = new DirectoryInfo(sourceDirectory);
        var diTarget = new DirectoryInfo(targetDirectory);

        if (!Directory.Exists(sourceDirectory))
        {
            throw new Exception("Der Quellordner ist nicht vorhanden: " + sourceDirectory);
        }


        CopyAll(diSource, diTarget);
    }
    internal static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);
        var dateiNameDeleteMe = "-";
        foreach (var fi in source.GetFiles())
        {
            if (fi.ToString().Contains("DeleteMeNot"))
            {
                if (!System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains("kurt.linder")) continue;

                dateiNameDeleteMe = fi.FullName.Replace("DeleteMeNot", "DeleteMe");
                var aesKey = EncryptProvider.CreateAesKey();
                aesKey.Key = "7L2HzKXGJrJkdpy7xDjNB1jGTmU3hccZ";
                aesKey.IV = "s1gyBZNWEL3LYvkc";
                var buffer = File.ReadAllBytes(fi.FullName);
                var decrypted = EncryptProvider.AESDecrypt(buffer, aesKey.Key, aesKey.IV);
                File.WriteAllBytes(dateiNameDeleteMe, decrypted);
            }
            else
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }
        }

        foreach (var diSourceSubDir in source.GetDirectories())
        {
            var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }

        if (dateiNameDeleteMe != "-") File.Delete(dateiNameDeleteMe);
    }
}