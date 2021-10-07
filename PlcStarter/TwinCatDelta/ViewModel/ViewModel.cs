using System.Windows.Input;
using TwinCatDelta.Commands;

namespace TwinCatDelta.ViewModel
{
    public class ViewModel
    {
        // ReSharper disable once UnusedMember.Global
        public Model.TwinCatDelta TwinCatDelta { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            TwinCatDelta = new Model.TwinCatDelta(mainWindow);
            ViAnzeige = new VisuAnzeigen();
        }

        private ICommand _btnStart;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnStart => _btnStart ??= new RelayCommand(_ => TwinCatDelta.TasterStart(), _ => true);

    }
}
