using System.ComponentModel;
using System.Windows.Media;

namespace PlcStarter.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public VisuAnzeigen()
        {
            StartButtonFarbe =  Brushes.LightGray;
            StartButtonInhalt = "Bitte ein Projekt auswählen";
        }

        private Brush _startButtonFarbe;
        public Brush StartButtonFarbe
        {
            get => _startButtonFarbe;
            set
            {
                _startButtonFarbe = value;
                OnPropertyChanged(nameof(StartButtonFarbe));
            }
        }

        private string _startButtonInhalt;
        public string StartButtonInhalt
        {
            get => _startButtonInhalt;
            set
            {
                _startButtonInhalt = value;
                OnPropertyChanged(nameof(StartButtonInhalt));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // ReSharper disable once UnusedMember.Local
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}