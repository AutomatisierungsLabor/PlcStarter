using System.Collections.Generic;

namespace PlcStarter.Model
{
    public class AlleWerte
    {
        public Dictionary<PlcSprachen, (string Prefix, string Anzeige)> AlleProgrammiersprachen { get; set; } = new();
        public Dictionary<PlcKategorie, (string Prefix, string Anzeige)> AlleKategorien { get; set; } = new();

        public AlleWerte()
        {
            AlleProgrammiersprachenEinlesen();
            AlleKategorienEinlesen();
        }

        private void AlleKategorienEinlesen()
        {
            AlleKategorien.Add(PlcKategorie.AdsRemote, (Prefix: "ADS_", Anzeige: "ADS"));
            AlleKategorien.Add(PlcKategorie.Bug, (Prefix: "BUG_", Anzeige: "BUG"));
            AlleKategorien.Add(PlcKategorie.DigitalTwin, (Prefix: "DT_", Anzeige: "DT"));
            AlleKategorien.Add(PlcKategorie.FactoryIo, (Prefix: "FIO_", Anzeige: "FIO"));
            AlleKategorien.Add(PlcKategorie.Hmi, (Prefix: "HMI_", Anzeige: "HMI"));
            AlleKategorien.Add(PlcKategorie.Nc, (Prefix: "NC_", Anzeige: "NC"));
            AlleKategorien.Add(PlcKategorie.Visu, (Prefix: "VISU_", Anzeige: "VISU"));
            AlleKategorien.Add(PlcKategorie.Snap7, (Prefix: "SNAP7_", Anzeige: "SNAP7"));
            AlleKategorien.Add(PlcKategorie.SoftwareTests, (Prefix: "TEST_", Anzeige: "TEST"));
            AlleKategorien.Add(PlcKategorie.Plc, (Prefix: "PLC_", Anzeige: "PLC"));
        }

        private void AlleProgrammiersprachenEinlesen()
        {
            AlleProgrammiersprachen.Add(PlcSprachen.As, (Prefix: "AS_", Anzeige: "AS"));
            AlleProgrammiersprachen.Add(PlcSprachen.Awl, (Prefix: "AWL_", Anzeige: "AWL"));
            AlleProgrammiersprachen.Add(PlcSprachen.Cfc, (Prefix: "CFC_", Anzeige: "CFC"));
            AlleProgrammiersprachen.Add(PlcSprachen.Cpp, (Prefix: "CPP_", Anzeige: "C++"));
            AlleProgrammiersprachen.Add(PlcSprachen.Fup, (Prefix: "FUP_", Anzeige: "FUP"));
            AlleProgrammiersprachen.Add(PlcSprachen.Kop, (Prefix: "KOP_", Anzeige: "KOP"));
            AlleProgrammiersprachen.Add(PlcSprachen.Scl, (Prefix: "SCL_", Anzeige: "SCL"));
            AlleProgrammiersprachen.Add(PlcSprachen.Stl, (Prefix: "ST_", Anzeige: "ST"));
        }
    }
}