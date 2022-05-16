using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PlcStarter.Model;

public static partial class AlleJobs
{
    private static void OrdnerErstellen(ViewModel.VmPlcStarter viewModel, PlcProjektdaten projektdaten, PlcJobs plcJobs)
    {
        Log.Debug($"Ordner erstellen: Job:{plcJobs}");

        viewModel.BrushStartButton = Brushes.Yellow;
        var ordner = projektdaten.OrdnerstrukturDestinationProjekt;

        try
        {
            switch (plcJobs)
            {
                case PlcJobs.None: break;
                case PlcJobs.ProjektKopieren: break;
                case PlcJobs.ProjektStarten: break;
                case PlcJobs.DigitalTwinKopieren:
                case PlcJobs.DigitalTwinStarten: ordner = projektdaten.OrdnerstrukturDestinationDigitalTwin; break;
                case PlcJobs.FactoryIoKopieren:
                case PlcJobs.FactoryIoStarten: ordner = projektdaten.OrdnerstrukturDestinationFactoryIo; break;
                case PlcJobs.TemplateOrdnerKopieren: break;
                case PlcJobs.DeltaOrdnerKopieren: break;

                default: throw new ArgumentOutOfRangeException(nameof(plcJobs), plcJobs, null);
            }

            viewModel.StringStartButton = "Zielordner wird gelöscht";

            if (Directory.Exists(ordner)) Directory.Delete(ordner, true);
            if (ordner != null) Directory.CreateDirectory(ordner);

            viewModel.StringStartButton = "Zielordner wurde erstellt";
        }
        catch (Exception exp)
        {
            Log.Error(exp.ToString());
            MessageBox.Show(exp.ToString());
        }

        viewModel.BrushStartButton = Brushes.LightGray;
    }
    public static void ProjektOrdnerKopieren(ViewModel.VmPlcStarter viewModel, string quelle, string ziel, string kommentarAnfang, string kommentarEnde)
    {


        viewModel.BrushStartButton = Brushes.Yellow;

        try
        {
            viewModel.StringStartButton = kommentarAnfang;
            Copy(quelle, ziel);
            viewModel.StringStartButton = kommentarEnde;
        }
        catch (Exception exp)
        {
            Log.Error(exp.ToString());
            MessageBox.Show(exp.ToString());
        }

        viewModel.BrushStartButton = Brushes.LightGray;
    }
}