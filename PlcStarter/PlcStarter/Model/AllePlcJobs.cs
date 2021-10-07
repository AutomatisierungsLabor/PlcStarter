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
                    OrdnerErstellen(viewModel, projektdaten.OrdnerStrukturDestination);
                    break;
                case PlcJobs.ProjektKopieren:
                    ProjektOrdnerKopieren(viewModel, projektdaten.OrdnerStrukturPlc, projektdaten.OrdnerStrukturDestination, projektdaten.OrdnerPlc);
                    break;
                case PlcJobs.CmdDateiProjektStarten:
                    ProjektStarten(viewModel, projektdaten.OrdnerStrukturDestination, projektdaten.OrdnerPlc);
                    break;

                case PlcJobs.DigitalTwinKopieren:
                    break;
                case PlcJobs.CmdDateiDigitalTwinStarten:
                    // DigitalTwinStarten(viewModel, projektdaten.OrdnerDestination, projektdaten.OrdnerDigitalTwin);
                    break;

                case PlcJobs.FactoryIoKopieren:
                    break;
                case PlcJobs.FachtoryIoStarten:
                    // FactoryIoStarten(viewModel, projektdaten.OrdnerDestination, projektdaten.OrdnerFactoryIo);
                    break;

                case PlcJobs.TemplateOrdnerKopieren:
                    break;
                case PlcJobs.DiffOrdnerKopieren:
                    break;

                default: throw new ArgumentOutOfRangeException(nameof(job), job, null);
            }
        }
        public static void OrdnerErstellen(ViewModel.ViewModel viewModel, string destination)
        {
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Zielordner wird gelöscht";

                if (Directory.Exists(destination)) Directory.Delete(destination, true);

                Directory.CreateDirectory(destination);
                viewModel.ViAnzeige.StartButtonInhalt = "Zielordner wurde erstellt";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }
        public static void ProjektOrdnerKopieren(ViewModel.ViewModel viewModel, string source, string destination, string ordner)
        {
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Projektdateien werden kopiert";

                Copy($"{source}/{ordner}", $"{destination}");

                viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde kopiert";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }
        public static void ProjektStarten(ViewModel.ViewModel viewModel, string destination, string ordner)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = $"{destination}/ProjektStarten.cmd",
                    WorkingDirectory = $"{destination}"
                }
            };
            proc.Start();

            viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";
        }

        public static void DigitalTwinStarten(ViewModel.ViewModel viewModel, string destination, string ordner)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = $"{destination}/DigitalTwinStarten.cmd",
                    WorkingDirectory = $"{destination}"
                }
            };
            proc.Start();

            viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";
        }
        public static void FactoryIoStarten(ViewModel.ViewModel viewModel, string destination, string ordner)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = $"{destination}/FactoryIoStarten.cmd",
                    WorkingDirectory = $"{destination}"
                }
            };
            proc.Start();

            viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";
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

            // Copy each file into the new directory.
            foreach (var fi in source.GetFiles()) fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);

            // Copy each subdirectory using recursion.
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}