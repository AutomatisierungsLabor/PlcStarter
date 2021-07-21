using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PlcStarter.Model
{

    public static class AllePlcJobs
    {

        public static void OrdnerKopieren(ViewModel.ViewModel viewModel, string source, string destination, string ordner)
        {

            viewModel.ViAnzeige.StartButtonFarbe = Brushes.Yellow;

            try
            {
                viewModel.ViAnzeige.StartButtonInhalt = "Zielordner wird gelöscht";
                /*
                if (Directory.Exists(AktuellesProjekt.ZielOrdner)) Directory.Delete(AktuellesProjekt.ZielOrdner, true);

                _viewModel.ViAnzeige.StartButtonInhalt = "Projektdateien werden kopiert";

                Copy(AktuellesProjekt.QuellOrdner, AktuellesProjekt.ZielOrdner);
*/
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
                
                viewModel.ViAnzeige.StartButtonInhalt = "Projekt wurde gestartet";

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

             viewModel.ViAnzeige.StartButtonFarbe = Brushes.LightGray;
        }

        public static void ProjektStarten(ViewModel.ViewModel viewModel, string projektdatenOrdner)
        {
            throw new NotImplementedException();
        }
    }
}
