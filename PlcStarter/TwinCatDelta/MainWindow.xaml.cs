using System.IO;
using System.Linq;
using System.Windows;
using TwinCatDelta.Model;

namespace TwinCatDelta
{
    public partial class MainWindow
    {
        private readonly ViewModel.ViewModel _viewModel;
        public MainWindow()
        {
            _viewModel = new ViewModel.ViewModel();
            InitializeComponent();
            DataContext = _viewModel;

            DataGrid.ItemsSource = _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid;
        }
        internal void BtnOpenKomplett_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            _viewModel.ViAnzeige.OrdnerKomplettesProjekt = dialog.SelectedPath;
        }

        internal void BtnOpenTemplate_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            _viewModel.ViAnzeige.OrdnerTemplateProjekt = dialog.SelectedPath;
        }
        internal void BtnOpenDelta_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            _viewModel.ViAnzeige.OrdnerDeltaProjekt = dialog.SelectedPath;
        }

        internal void OrdnerVergleichen_Click(object sender, RoutedEventArgs e) => Dispatcher.Invoke(() =>
        {
            _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Clear();

            var laengeOrdnerKomplett = 1 + _viewModel.ViAnzeige.OrdnerKomplettesProjekt.Length; // inc "/"
            var filesKomplett = Directory.GetFiles(_viewModel.ViAnzeige.OrdnerKomplettesProjekt, "*.*", SearchOption.AllDirectories);

            foreach (var file in filesKomplett)
            {
                var templateDateiIdentisch = false;
                var deltaDateiIdentisch = false;

                var dateiname = file[laengeOrdnerKomplett..];
                var dateinameTemplate = $"{_viewModel.ViAnzeige.OrdnerTemplateProjekt}/{dateiname}";
                var dateinameDelta = $"{_viewModel.ViAnzeige.OrdnerDeltaProjekt}/{dateiname}";
                var templateDateiVorhanden = File.Exists(dateinameTemplate);
                var deltaDateiVorhanden = File.Exists(dateinameDelta);

                if (templateDateiVorhanden) templateDateiIdentisch = AreFileContentsEqual(file, dateinameTemplate);
                if (deltaDateiVorhanden) deltaDateiIdentisch = AreFileContentsEqual(file, dateinameDelta);

                _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Add(new OrdnerDateiInfo(dateiname, templateDateiVorhanden, templateDateiIdentisch, deltaDateiVorhanden, deltaDateiIdentisch));
            }
        });

        internal void OrdnerDeltaKopieren_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Count == 0) return;

            foreach (var dateiInfo in _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid)
            {
                if (dateiInfo.TemplateDateiIdentisch) continue;
                if (dateiInfo.DeltaDateiIdentisch) continue;

                var dateinameKomplett = $"{_viewModel.ViAnzeige.OrdnerKomplettesProjekt}\\{dateiInfo.DateiBezeichnung}";
                var dateinameDelta = $"{_viewModel.ViAnzeige.OrdnerDeltaProjekt}\\{dateiInfo.DateiBezeichnung}";

                var pfad = Path.GetDirectoryName(dateinameDelta);
                if (!Directory.Exists(pfad)) Directory.CreateDirectory(pfad!);

                File.Copy(dateinameKomplett, dateinameDelta);
            }
        }

        public static bool AreFileContentsEqual(string path1, string path2) => File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));
    }
}