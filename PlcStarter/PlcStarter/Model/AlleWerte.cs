using System.Collections.Generic;

namespace PlcStarter.Model
{
    public class AlleWerte
    {
        public Dictionary<PlcStarter.PlcSprachen, (string Prefix, string Anzeige)> AlleProgrammiersprachen { get; set; } = new();
        public Dictionary<PlcStarter.PlcKategorie, (string Prefix, string Anzeige)> AlleKategorien { get; set; } = new();

        public AlleWerte()
        {
            AlleProgrammiersprachenEinlesen();
            AlleKategorienEinlesen();
        }

        private void AlleKategorienEinlesen()
        {
            AlleKategorien.Add(PlcStarter.PlcKategorie.AdsRemote, (Prefix: "ADS_", Anzeige: "ADS"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.Bug, (Prefix: "BUG_", Anzeige: "BUG"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.DigitalTwin, (Prefix: "DT_", Anzeige: "DT"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.FactoryIo, (Prefix: "FIO_", Anzeige: "FIO"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.Hmi, (Prefix: "HMI_", Anzeige: "HMI"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.Nc, (Prefix: "NC_", Anzeige: "NC"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.Visu, (Prefix: "VISU_", Anzeige: "VISU"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.Snap7, (Prefix: "SNAP7_", Anzeige: "SNAP7"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.SoftwareTests, (Prefix: "TEST_", Anzeige: "TEST"));
            AlleKategorien.Add(PlcStarter.PlcKategorie.Plc, (Prefix: "PLC_", Anzeige: "PLC"));
        }

        private void AlleProgrammiersprachenEinlesen()
        {
            AlleProgrammiersprachen.Add(PlcStarter.PlcSprachen.As, (Prefix: "AS_", Anzeige: "AS"));
            AlleProgrammiersprachen.Add(PlcStarter.PlcSprachen.Awl, (Prefix: "AWL_", Anzeige: "AWL"));
            AlleProgrammiersprachen.Add(PlcStarter.PlcSprachen.Cfc, (Prefix: "CFC_", Anzeige: "CFC"));
            AlleProgrammiersprachen.Add(PlcStarter.PlcSprachen.Cpp, (Prefix: "CPP_", Anzeige: "C++"));
            AlleProgrammiersprachen.Add(PlcStarter.PlcSprachen.Fup, (Prefix: "FUP_", Anzeige: "FUP"));
            AlleProgrammiersprachen.Add(PlcStarter.PlcSprachen.Kop, (Prefix: "KOP_", Anzeige: "KOP"));
            AlleProgrammiersprachen.Add(PlcStarter.PlcSprachen.Scl, (Prefix: "SCL_", Anzeige: "SCL"));
            AlleProgrammiersprachen.Add(PlcStarter.PlcSprachen.Stl, (Prefix: "ST_", Anzeige: "ST"));
        }
    }
}