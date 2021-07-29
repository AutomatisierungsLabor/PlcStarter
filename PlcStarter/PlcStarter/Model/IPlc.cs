namespace PlcStarter.Model
{
    public interface IPlc
    {
        public void TabEigenschaftenHinzufuegen();
        public void AnzeigeUpdaten(TabEigenschaften tabEigenschaften);
    }
}