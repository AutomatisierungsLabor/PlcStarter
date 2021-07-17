using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PlcStarter
{
    public partial class MainWindow
    {
        internal void ProjektStarten(object obj)
        {
            _viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                _viewModel.ViAnzeige.StartButtonInhalt = "Zielordner wird gelöscht";

                if (Directory.Exists(AktuellesProjekt.ZielOrdner)) Directory.Delete(AktuellesProjekt.ZielOrdner, true);

                _viewModel.ViAnzeige.StartButtonInhalt = "Projektdateien werden kopiert";

                Copy(AktuellesProjekt.QuellOrdner, AktuellesProjekt.ZielOrdner);

                _viewModel.ViAnzeige.StartButtonInhalt = "Projekt wird gestartet";

                var proc = new Process
                {
                    StartInfo =
                    {
                        FileName = AktuellesProjekt.ZielOrdner + "\\start.cmd",
                        WorkingDirectory = AktuellesProjekt.ZielOrdner
                    }
                };
                proc.Start();
                _viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            _viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
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