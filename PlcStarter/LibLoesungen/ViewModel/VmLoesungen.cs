using System.Threading;
using LibLoesungen.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace LibLoesungen.ViewModel;

public partial class VmLoesungen : ObservableObject
{
    private readonly ModelLoesungen _modelLoesungen;
    private readonly Loesungen _loesungen;

    public VmLoesungen(ModelLoesungen modelLoesungen, Loesungen loesungen)
    {
        _modelLoesungen = modelLoesungen;
        _loesungen = loesungen;

        System.Threading.Tasks.Task.Run(ViewModelTask);
    }
    private static void ViewModelTask()
    {
        while (true)
        {


            Thread.Sleep(10);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    public void SetLoesungText(string loesungText)
    {
        StringText = loesungText;
    }
}