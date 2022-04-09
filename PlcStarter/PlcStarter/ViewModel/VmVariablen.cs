using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PlcStarter.ViewModel;


public partial class VmPlcStarter
{
    [ObservableProperty] private Brush _brushStartButton;

    [ObservableProperty] private string _stringStartButton;

}