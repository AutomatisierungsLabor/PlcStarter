using System.Collections.ObjectModel;
using System.ComponentModel;
using TwinCatDelta.Model;

namespace TwinCatDelta.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public VisuAnzeigen()
        {
            EnableButton = false;
        }

        private ObservableCollection<OrdnerDateiInfo> _ordnerDateiInfoDataGrid = new();
        public ObservableCollection<OrdnerDateiInfo> OrdnerDateiInfoDataGrid
        {
            get => _ordnerDateiInfoDataGrid;
            set
            {
                _ordnerDateiInfoDataGrid = value;
                OnPropertyChanged(nameof(OrdnerDateiInfoDataGrid));
            }
        }
        
        private bool _enableButton;
        public bool EnableButton
        {
            get => _enableButton;
            set
            {
                _enableButton = value;
                OnPropertyChanged(nameof(EnableButton));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}