using PlcStarter.Commands;
using System.Windows.Input;

namespace PlcStarter.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }

        private readonly MainWindow _mainWindow;

        public ViewModel(MainWindow mw)
        {
            _mainWindow = mw;
            ViAnzeige = new VisuAnzeigen();
        }

        private ICommand _btnHaken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnHaken => _btnHaken ??= new RelayCommand(_mainWindow.ButtonGeaendert);
    }
}