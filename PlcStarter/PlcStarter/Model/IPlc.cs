using System.Windows.Controls;

namespace PlcStarter.Model
{
    public  interface IPlc
    {

        public void TabEigenschaftenHinzufuegen();
        public void AnzeigeUpdaten(TabEigenschaften tabEigenschaften);
        void ProjektStarten(ViewModel.ViewModel viewModel, Button btn);
    }
}