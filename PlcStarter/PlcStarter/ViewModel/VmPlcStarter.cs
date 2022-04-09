using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace PlcStarter.ViewModel;

public partial class VmPlcStarter : ObservableObject
{

    private readonly MainWindow _mainWindow;

    public VmPlcStarter(MainWindow mw)
    {
        _mainWindow = mw;
        BrushStartButton = Brushes.LightGray;
        StringStartButton = "Bitte ein Projekt auswählen";
    }
    /*
    private ICommand _btnHaken;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnHaken => _btnHaken ??= new RelayCommand(_mainWindow.ButtonGeaendert);
    */
}