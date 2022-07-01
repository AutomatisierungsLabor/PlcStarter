using LibLoesungen.Model;
using LibLoesungen.ViewModel;

namespace LibLoesungen;

public partial class Loesungen
{
    public bool FensterAktiv { get; set; }
    public ModelLoesungen ModelLoesungen { get; set; }
    public VmLoesungen VmLoesungen { get; set; }

    public Loesungen()
    {
        ModelLoesungen = new ModelLoesungen();
        VmLoesungen = new VmLoesungen(this);

        DataContext = VmLoesungen;

        InitializeComponent();
    }
    public void FensterAusblenden()
    {
        FensterAktiv = false;
        Hide();
    }
    public void FensterAnzeigen()
    {
        FensterAktiv = true;
        Show();
    }
    public void LoesungLaden(string projektPfad) => SourceCodeEditor.Document.Text = ModelLoesungen.LoesungLesen(projektPfad);
    public void LoesungSpeichern() => ModelLoesungen.LoesungSpeichern(SourceCodeEditor);
}