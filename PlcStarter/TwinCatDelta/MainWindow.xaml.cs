using System.IO;
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
        internal void OrdnerVergleichen_Click(object sender, RoutedEventArgs e) => Dispatcher.Invoke(() =>
        {
            _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Clear();

            var laengeOrdnerKomplett = 1 + _viewModel.ViAnzeige.OrdnerKomplettesProjekt.Length; // inc "/"
            var filesKomplett = Directory.GetFiles(_viewModel.ViAnzeige.OrdnerKomplettesProjekt, "*.*", SearchOption.AllDirectories);

            foreach (var file in filesKomplett)
            {
                if (file.Contains("net5.0-windows")) continue;
                if (file.Contains("DigitalTwinStarten")) continue;

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
    }
}