using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;


namespace JsonEditor
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            try
            {
                StreamReader file = new StreamReader("PlcStarter.json");
                
                JsonViewer.Load(file.ReadToEnd());
                TextBlock.Text = "Loading finished";

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                const string json = "{\"one\": \"two\",\"key\": \"value\"}";
                JsonViewer.Load(json);
            }
        }
    }
}
