using System.IO;
using System.Linq;
using System.Windows;
using TwinCatDelta.Model;

namespace TwinCatDelta
{
    public partial class MainWindow
    {


        internal void OrdnerDeltaKopieren_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Count == 0) return;

            foreach (var dateiInfo in _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid)
            {
                if (dateiInfo.DateiBezeichnung.Contains("DeleteMe.TcPOU")) SpezialKopieErstellen(dateiInfo);

                if (dateiInfo.TemplateDateiIdentisch) continue;
                if (dateiInfo.DeltaDateiIdentisch) continue;

                var dateinameKomplett = $"{_viewModel.ViAnzeige.OrdnerKomplettesProjekt}\\{dateiInfo.DateiBezeichnung}";
                var dateinameDelta = $"{_viewModel.ViAnzeige.OrdnerDeltaProjekt}\\{dateiInfo.DateiBezeichnung}";

                var pfad = Path.GetDirectoryName(dateinameDelta);
                if (!Directory.Exists(pfad)) Directory.CreateDirectory(pfad!);

                if (File.Exists(dateinameDelta)) MessageBox.Show($"Datei vorhanden:{dateinameDelta}");
                else File.Copy(dateinameKomplett, dateinameDelta);
            }
        }

        private void SpezialKopieErstellen(OrdnerDateiInfo dateiInfo)
        {
            var dateiname = $"{_viewModel.ViAnzeige.OrdnerKomplettesProjekt}\\{dateiInfo.DateiBezeichnung}";
            var neuerDateiName = $"{_viewModel.ViAnzeige.OrdnerDeltaProjekt}\\{dateiInfo.DateiBezeichnung.Replace("DeleteMe.TcPOU", "DeleteMeNot.TcPOU")}";

            if (File.Exists(neuerDateiName)) MessageBox.Show($"Datei vorhanden:{neuerDateiName}");
            else File.Copy(dateiname, neuerDateiName);
        }


        public static bool AreFileContentsEqual(string path1, string path2) =>
            File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));
    }
}