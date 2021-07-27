using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PlcStarter.Model
{

    public static class AllePlcJobs
    {



        public static void DestinationOrdnerErstellen(ViewModel.ViewModel viewModel, string destination, string ordner)
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

                Copy($"{source}/{ordner}", $"{destination}/{ordner}");

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
                    FileName = $"{destination}/{ordner}/ProjektStarten.cmd",
                    WorkingDirectory = $"{destination}/{ordner}"
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
