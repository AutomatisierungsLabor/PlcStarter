using System.IO;
using System.Linq;
using System.Windows;
using NETCore.Encrypt;
using TwinCatDelta.Model;
using File = System.IO.File;

namespace TwinCatDelta
{
    public partial class MainWindow
    {
        internal void OrdnerDeltaKopieren_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Count == 0) return;

            foreach (var dateiInfo in _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid)
            {
                if (dateiInfo.DateiBezeichnung.Contains("DeleteMe.TcPOU"))
                {
                    SpezialKopieErstellen(dateiInfo);
                    continue;
                }
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
            var komplettDateiname = @$"{_viewModel.ViAnzeige.OrdnerKomplettesProjekt}\{dateiInfo.DateiBezeichnung}";
            var deltaDateiName = @$"{_viewModel.ViAnzeige.OrdnerDeltaProjekt}\{dateiInfo.DateiBezeichnung}";
            var neuerDateiName = deltaDateiName.Replace("DeleteMe.TcPOU", "DeleteMeNot.TcPOU");

            if (File.Exists(deltaDateiName)) File.Delete(deltaDateiName);
            if (File.Exists(neuerDateiName)) File.Delete(neuerDateiName);

            var pfad = Path.GetDirectoryName(neuerDateiName);
            if (!Directory.Exists(pfad)) Directory.CreateDirectory(pfad!);

            var aesKey = EncryptProvider.CreateAesKey();
            aesKey.Key = "7L2HzKXGJrJkdpy7xDjNB1jGTmU3hccZ";
            aesKey.IV = "s1gyBZNWEL3LYvkc";
            var buffer = File.ReadAllBytes(komplettDateiname);
            var encrypted = EncryptProvider.AESEncrypt(buffer, aesKey.Key, aesKey.IV);
            File.WriteAllBytes(neuerDateiName, encrypted);

            /*
            var decrypted = EncryptProvider.AESDecrypt(encrypted, aesKey.Key, aesKey.IV);
            File.WriteAllBytes(neuerDateiName + 1, decrypted);
            */

        }
        public static bool AreFileContentsEqual(string path1, string path2) =>
            File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));
    }
}