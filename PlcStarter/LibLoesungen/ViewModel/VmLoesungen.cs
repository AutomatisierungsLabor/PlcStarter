using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace LibLoesungen.ViewModel;

public partial class VmLoesungen : ObservableObject
{
    private readonly Loesungen _loesungen;

    public VmLoesungen(Loesungen loesungen) => _loesungen = loesungen;
}