using NETCore.Encrypt;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PlcStarter.Model;

public static class AllePlcJobs
{
    public static void PlcJobAusfuehren(PlcJobs job, PlcProjektdaten projektdaten, ViewModel.ViewModel viewModel)
    {
        switch (job)
        {
            case PlcJobs.None: break;

            case PlcJobs.ProjektKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, @$"{projektdaten.OrdnerstrukturSourceProjekt}\{projektdaten.OrdnerPlc}", $"{projektdaten.OrdnerstrukturDestinationProjekt}", "Projektdateien werden kopiert", "Projekt wurde kopiert");
                break;

            case PlcJobs.ProjektStarten:
                var nameBatchDatei = "ProjektStarten.cmd";
                if (projektdaten.SoftwareVersion == PlcSoftwareVersion.TiaPortalV17Sp1) nameBatchDatei = "ProjektStartenV17.cmd";

                ProjektStarten(viewModel, @$"{projektdaten.OrdnerstrukturDestinationProjekt}\{nameBatchDatei}", $"{projektdaten.OrdnerstrukturDestinationProjekt}");
                break;

            case PlcJobs.DigitalTwinKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, @$"{projektdaten.OrdnerstrukturSourceDigitalTwin}\{projektdaten.OrdnerDigitalTwin}", $"{projektdaten.OrdnerstrukturDestinationDigitalTwin}", "Digital Twin wird kopiert", "Digital Twin wurde kopiert");
                break;
            case PlcJobs.DigitalTwinStarten:
                ProjektStarten(viewModel, @$"{projektdaten.OrdnerstrukturDestinationDigitalTwin}\DigitalTwinStarten.cmd", $"{projektdaten.OrdnerstrukturDestinationDigitalTwin}");
                break;

            case PlcJobs.FactoryIoKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, $"{projektdaten.OrdnerstrukturSourceFactoryIo}/{projektdaten.OrdnerFactoryIo}", $"{projektdaten.OrdnerstrukturDestinationFactoryIo}", "Factory I/O wird kopiert", "Factory I/O wurde kopiert");
                break;
            case PlcJobs.FactoryIoStarten:
                ProjektStarten(viewModel, @$"{projektdaten.OrdnerstrukturDestinationFactoryIo}\FactoryIoStarten.cmd", $"{projektdaten.OrdnerstrukturDestinationFactoryIo}");
                break;

            case PlcJobs.TemplateOrdnerKopieren:
                OrdnerErstellen(viewModel, projektdaten, job);
                ProjektOrdnerKopieren(viewModel, @$"{projektdaten.OrdnerstrukturSourceProjekt}\{projektdaten.OrdnerTemplate}", $"{projektdaten.OrdnerstrukturDestinationProjekt}", "Projekt Template wird kopiert", "Projekt Template wurde kopiert");
                break;
            case PlcJobs.DiffOrdnerKopieren:
                ProjektOrdnerKopieren(viewModel, @$"{projektdaten.OrdnerstrukturSourceProjekt}\{projektdaten.OrdnerPlc}", $"{projektdaten.OrdnerstrukturDestinationProjekt}", "Projekt Delta wird kopiert", "Projekt Delta wurde kopiert");
                break;

            default: throw new ArgumentOutOfRangeException(nameof(job), job, null);
        }
    }

    public static void ProjektOrdnerKopieren(ViewModel.ViewModel viewModel, string quelle, string ziel, string kommentarAnfang, string kommentarEnde)
    {
        viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

        try
        {
            viewModel.ViAnzeige.StartButtonInhalt = kommentarAnfang;
            Copy(quelle, ziel);
            viewModel.ViAnzeige.StartButtonInhalt = kommentarEnde;
        }
        catch (Exception exp)
        {
            MessageBox.Show(exp.ToString());
        }

        viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
    }

    public static void ProjektStarten(ViewModel.ViewModel viewModel, string cmdFile, string workingDirectory)
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

        viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";
    }
    private static void OrdnerErstellen(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten, PlcJobs plcJobs)
    {
        viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;
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
            case PlcJobs.DiffOrdnerKopieren: break;

            default: throw new ArgumentOutOfRangeException(nameof(plcJobs), plcJobs, null);
        }


        try
        {
            viewModel.ViAnzeige.StartButtonInhalt = "Zielordner wird gelöscht";

            if (Directory.Exists(ordner)) Directory.Delete(ordner, true);
            if (ordner != null) Directory.CreateDirectory(ordner);

            viewModel.ViAnzeige.StartButtonInhalt = "Zielordner wurde erstellt";
        }
        catch (Exception exp)
        {
            MessageBox.Show(exp.ToString());
        }

        viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
    }
    internal static void Copy(string sourceDirectory, string targetDirectory)
    {
        var diSource = new DirectoryInfo(sourceDirectory);
        var diTarget = new DirectoryInfo(targetDirectory);

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