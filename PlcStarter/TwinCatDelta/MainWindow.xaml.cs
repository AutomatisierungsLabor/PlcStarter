using System.IO;
using System.Linq;
using System.Windows;
using TwinCatDelta.Model;

namespace TwinCatDelta
{
    public partial class MainWindow
    {
        private readonly ViewModel.ViewModel _viewModel;

        public string OrdnerKomplett { get; set; }
        public string OrdnerTemplate { get; set; }
        public string OrdnerDelta { get; set; }
        public bool OrdnerKomplettAktualisiert { get; set; }
        public bool OrdnerTemplateAktualisiert { get; set; }
        public bool OrdnerDeltaAktualisiert { get; set; }

        public MainWindow()
        {
            _viewModel = new ViewModel.ViewModel(this);
            InitializeComponent();
            DataContext = _viewModel;

            DataGrid.ItemsSource = _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid;
        }
        internal void BtnOpenKomplett_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            OrdnerKomplett = dialog.SelectedPath;
            OrdnerKomplettAktualisiert = true;
            DataGridUpdaten();
        }

        internal void BtnOpenTemplate_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            OrdnerTemplate = dialog.SelectedPath;
            OrdnerTemplateAktualisiert = true;
            DataGridUpdaten();
        }
        internal void BtnOpenDelta_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            OrdnerDelta = dialog.SelectedPath;
            OrdnerDeltaAktualisiert = true;
            DataGridUpdaten();
        }

        private void DataGridUpdaten() => Dispatcher.Invoke(() =>
        {
            if (!OrdnerKomplettAktualisiert || !OrdnerTemplateAktualisiert || !OrdnerDeltaAktualisiert) return;

            OrdnerKomplettAktualisiert = false;
            OrdnerTemplateAktualisiert = false;
            OrdnerDeltaAktualisiert = false;

            _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Clear();

            var laengeOrdnerKomplett = 1 + OrdnerKomplett.Length; // inc "/"
            var filesKomplett = Directory.GetFiles(OrdnerKomplett, "*.*", SearchOption.AllDirectories);

            foreach (var file in filesKomplett)
            {
                var templateDateiIdentisch = false;
                var deltaDateiIdentisch = false;

                var dateiname = file[laengeOrdnerKomplett..];
                var dateinameTemplate = $"{OrdnerTemplate}/{dateiname}";
                var dateinameDelta = $"{OrdnerDelta}/{dateiname}";

                var templateDateiVorhanden = File.Exists(dateinameTemplate);
                if (templateDateiVorhanden) templateDateiIdentisch = AreFileContentsEqual(file, dateinameTemplate);

                var deltaDateiVorhanden = File.Exists(dateinameDelta);
                if (deltaDateiVorhanden) deltaDateiIdentisch = AreFileContentsEqual(file, dateinameDelta);

                _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Add(new OrdnerDateiInfo(dateiname, templateDateiVorhanden, templateDateiIdentisch, deltaDateiVorhanden, deltaDateiIdentisch));
            }
        });
        public static bool AreFileContentsEqual(string path1, string path2) => File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));
    }
}