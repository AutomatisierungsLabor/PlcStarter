using NETCore.Encrypt;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PlcStarter.Model
{
    public static class AllePlcJobs
    {
        public static void PlcJobAusfuehren(PlcJobs job, PlcProjektdaten projektdaten, ViewModel.ViewModel viewModel)
        {
            switch (job)
            {
                case PlcJobs.None: break;
                case PlcJobs.SorceOrdnerErstellen:
                    OrdnerErstellen(viewModel, projektdaten);
                    break;
                case PlcJobs.ProjektKopieren:
                    ProjektOrdnerKopieren(viewModel,
                        $"{projektdaten.OrdnerStrukturPlc}/{projektdaten.OrdnerPlc}", $"{projektdaten.OrdnerStrukturDestination}",
                        "Projektdateien werden kopiert", "Projekt wurde kopiert");
                    break;
                case PlcJobs.CmdDateiProjektStarten:
                    ProjektStarten(viewModel, $"{projektdaten.OrdnerStrukturDestination}/ProjektStarten.cmd", $"{projektdaten.OrdnerStrukturDestination}");
                    break;

                case PlcJobs.DigitalTwinKopieren:
                    ProjektOrdnerKopieren(viewModel,
                        $"{projektdaten.OrdnerStrukturDigitalTwin}/{projektdaten.OrdnerDigitalTwin}", $"{projektdaten.OrdnerStrukturDestination}",
                        "Digital Twin wird kopiert", "Digital Twin wurde kopiert");
                    break;
                case PlcJobs.DigitalTwinStartenSiemens:
                    ProjektStarten(viewModel, $"{projektdaten.OrdnerStrukturDestination}/DigitalTwinStartenSiemens.cmd", $"{projektdaten.OrdnerStrukturDestination}");
                    break;
                case PlcJobs.DigitalTwinStartenBeckhoff:
                    ProjektStarten(viewModel, $"{projektdaten.OrdnerStrukturDestination}/DigitalTwinStartenBeckhoff.cmd", $"{projektdaten.OrdnerStrukturDestination}");
                    break;

                case PlcJobs.FactoryIoKopieren:
                    ProjektOrdnerKopieren(viewModel,
                        $"{projektdaten.OrdnerStrukturFactoryIo}/{projektdaten.OrdnerFactoryIo}", $"{projektdaten.OrdnerStrukturDestination}/FactoryIO",
                        "Factory I/O wird kopiert", "Factory I/O wurde kopiert");
                    break;
                case PlcJobs.FactoryIoStarten:
                    ProjektStarten(viewModel, $"{projektdaten.OrdnerStrukturDestination}/FactoryIO/FactoryIoStarten.cmd", $"{projektdaten.OrdnerStrukturDestination}/FactoryIO");
                    break;

                case PlcJobs.TemplateOrdnerKopieren:
                    ProjektOrdnerKopieren(viewModel,
                        $"{projektdaten.OrdnerStrukturPlc}/{projektdaten.OrdnerTemplate}", $"{projektdaten.OrdnerStrukturDestination}",
                        "Projekt Template wird kopiert", "Projekt Template wurde kopiert");
                    break;
                case PlcJobs.DiffOrdnerKopieren:
                    ProjektOrdnerKopieren(viewModel,
                        $"{projektdaten.OrdnerStrukturPlc}/{projektdaten.OrdnerPlc}", $"{projektdaten.OrdnerStrukturDestination}",
                        "Projekt Delta wird kopiert", "Projekt Delta wurde kopiert");
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
        private static void OrdnerErstellen(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Zielordner wird gelöscht";

                if (Directory.Exists(projektdaten.OrdnerStrukturDestination)) Directory.Delete(projektdaten.OrdnerStrukturDestination, true);

                Directory.CreateDirectory(projektdaten.OrdnerStrukturDestination);
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

            foreach (var fi in source.GetFiles())
            {
                if (fi.ToString().Contains("DeleteMeNot"))
                {
                    if (!System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains("kurt.linder")) continue;

                    var neuerDateiname = fi.FullName.Replace("DeleteMeNot", "DeleteMe");
                    var aesKey = EncryptProvider.CreateAesKey();
                    aesKey.Key = "7L2HzKXGJrJkdpy7xDjNB1jGTmU3hccZ";
                    aesKey.IV = "s1gyBZNWEL3LYvkc";
                    var buffer = File.ReadAllBytes(fi.FullName);
                    var decrypted = EncryptProvider.AESDecrypt(buffer, aesKey.Key, aesKey.IV);
                    File.WriteAllBytes(neuerDateiname, decrypted);
                }
                else fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}