using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace PlcStarter.ViewModel;


public partial class VmPlcStarter
{
    [ObservableProperty] private Brush _brushStartButton;

    [ObservableProperty] private string _stringStartButton;
    [ObservableProperty] private string _stringButtonLoesungen;

    [ObservableProperty] private Visibility _visibilityLoesungAnzeigen;
}