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
                    ProjektOrdnerKopieren(viewModel, projektdaten);
                    break;
                case PlcJobs.CmdDateiProjektStarten:
                    ProjektStarten(viewModel, projektdaten);
                    break;

                case PlcJobs.DigitalTwinKopieren:
                    DigitalTwinKopieren(viewModel, projektdaten);
                    break;
                case PlcJobs.DigitalTwinStartenSiemens:
                    DigitalTwinStartenSiemens(viewModel, projektdaten);
                    break;
                case PlcJobs.DigitalTwinStartenBeckhoff:
                    DigitalTwinStartenBeckhoff(viewModel, projektdaten);
                    break;

                case PlcJobs.FactoryIoKopieren:
                    FactoryIoKopieren(viewModel, projektdaten);
                    break;
                case PlcJobs.FachtoryIoStarten:
                    FactoryIoStarten(viewModel, projektdaten);
                    break;

                case PlcJobs.TemplateOrdnerKopieren:
                    TemplateOrdnerKopieren(viewModel, projektdaten);
                    break;
                case PlcJobs.DiffOrdnerKopieren:
                    DiffOrdnerKopieren(viewModel, projektdaten);
                    break;

                default: throw new ArgumentOutOfRangeException(nameof(job), job, null);
            }
        }


        #region Projekte kopieren
        private static void ProjektOrdnerKopieren(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Projektdateien werden kopiert";

                Copy($"{projektdaten.OrdnerStrukturPlc}/{projektdaten.OrdnerPlc}", $"{projektdaten.OrdnerStrukturDestination}");

                viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde kopiert";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }

        private static void DigitalTwinKopieren(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Digital Twin wird kopiert";

                Copy($"{projektdaten.OrdnerStrukturDigitalTwin}/{projektdaten.OrdnerDigitalTwin}", $"{projektdaten.OrdnerStrukturDestination}");

                viewModel.ViAnzeige.StartButtonInhalt = "Digital Twin wurde kopiert";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }
        private static void FactoryIoKopieren(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Factory I/O wird kopiert";

                Copy($"{projektdaten.OrdnerStrukturFactoryIo}/{projektdaten.OrdnerFactoryIo}", $"{projektdaten.OrdnerStrukturDestination}/FactoryIO");

                viewModel.ViAnzeige.StartButtonInhalt = "Factory I/O wurde kopiert";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }
        private static void DiffOrdnerKopieren(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Projekt Delta wird kopiert";

                Copy($"{projektdaten.OrdnerStrukturPlc}/{projektdaten.OrdnerPlc}", $"{projektdaten.OrdnerStrukturDestination}");

                viewModel.ViAnzeige.StartButtonInhalt = "Projekt Delta wurde kopiert";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }

        private static void TemplateOrdnerKopieren(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Projekt Template wird kopiert";

                Copy($"{projektdaten.OrdnerStrukturPlc}/{projektdaten.OrdnerTemplate}", $"{projektdaten.OrdnerStrukturDestination}");

                viewModel.ViAnzeige.StartButtonInhalt = "Projekt Template wurde kopiert";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }
        #endregion

        #region Projekte starten
        public static void ProjektStarten(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = $"{projektdaten.OrdnerStrukturDestination}/ProjektStarten.cmd",
                    WorkingDirectory = $"{projektdaten.OrdnerStrukturDestination}"
                }
            };
            proc.Start();

            viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";
        }
        public static void DigitalTwinStartenBeckhoff(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = $"{projektdaten.OrdnerStrukturDestination}/DigitalTwinStartenBeckhoff.cmd",
                    WorkingDirectory = $"{projektdaten.OrdnerStrukturDestination}"
                }
            };
            proc.Start();

            viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";
        }
        public static void DigitalTwinStartenSiemens(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = $"{projektdaten.OrdnerStrukturDestination}/DigitalTwinStartenSiemens.cmd",
                    WorkingDirectory = $"{projektdaten.OrdnerStrukturDestination}"
                }
            };
            proc.Start();

            viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";
        }
        public static void FactoryIoStarten(ViewModel.ViewModel viewModel, PlcProjektdaten projektdaten)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = $"{projektdaten.OrdnerStrukturDestination}/FactoryIO/FactoryIoStarten.cmd",
                    WorkingDirectory = $"{projektdaten.OrdnerStrukturDestination}/FactoryIO"
                }
            };
            proc.Start();

            viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";
        }
        #endregion

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