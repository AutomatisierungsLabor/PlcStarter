using System.Collections.ObjectModel;
using System.ComponentModel;
using TwinCatDelta.Model;

namespace TwinCatDelta.ViewModel;

public class VisuAnzeigen : INotifyPropertyChanged
{
    public VisuAnzeigen()
    {
        EnableButton = false;

        OrdnerKomplettesProjekt = @"h:\TwinCat"; // @"s:\Linder Kurt\PlcTwinCAT_Komplett\TEST_ST_LAP_2010_1_Kompressoranlage";
        OrdnerTemplateProjekt = @"s:\Linder Kurt\PlcTwinCAT\DELTA_Template_Plc";
        OrdnerDeltaProjekt = @"s:\Linder Kurt\PlcTwinCAT\DELTA_TEST_ST_LAP_2010_1_Kompressoranlage";
    }

    private string _ordnerKomplettesProjekt;
    public string OrdnerKomplettesProjekt
    {
        get => _ordnerKomplettesProjekt;
        set
        {
            _ordnerKomplettesProjekt = value;
            OnPropertyChanged(nameof(OrdnerKomplettesProjekt));
        }
    }

    private string _ordnerTemplateProjekt;
    public string OrdnerTemplateProjekt
    {
        get => _ordnerTemplateProjekt;
        set
        {
            _ordnerTemplateProjekt = value;
            OnPropertyChanged(nameof(OrdnerTemplateProjekt));
        }
    }

    private string _ordnerDeltaProjekt;
    public string OrdnerDeltaProjekt
    {
        get => _ordnerDeltaProjekt;
        set
        {
            _ordnerDeltaProjekt = value;
            OnPropertyChanged(nameof(OrdnerDeltaProjekt));
        }
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