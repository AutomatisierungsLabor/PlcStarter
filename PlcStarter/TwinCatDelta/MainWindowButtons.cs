using System.Windows;

namespace TwinCatDelta;

// ReSharper disable once UnusedMember.Global
public partial class MainWindow
{
    internal void BtnOpenKomplett_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new System.Windows.Forms.FolderBrowserDialog();
        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

        _viewModel.ViAnzeige.OrdnerKomplettesProjekt = dialog.SelectedPath;
    }

    internal void BtnOpenTemplate_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new System.Windows.Forms.FolderBrowserDialog();
        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

        _viewModel.ViAnzeige.OrdnerTemplateProjekt = dialog.SelectedPath;
    }

    internal void BtnOpenDelta_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new System.Windows.Forms.FolderBrowserDialog();
        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

        _viewModel.ViAnzeige.OrdnerDeltaProjekt = dialog.SelectedPath;
    }
}