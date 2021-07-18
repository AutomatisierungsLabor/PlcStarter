using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PlcStarter.Model
{
    public class ProjektEigenschaften
    {
        public static int LaufendeNummer { get; set; }
        public Steuerungen Steuerung { get; set; }
        public string QuellOrdner { get; set; }
        public string ZielOrdner { get; set; }
        public string Bezeichnung { get; set; }
        public PlcSprachen Programmiersprache { get; set; }
        public PlcKategorie PlcKategorie { get; set; }
        public WebBrowser BrowserBezeichnung { get; set; }

        public ProjektEigenschaften(MainWindow mw, Steuerungen steuerung, string quelle, string ziel)
        {
            LaufendeNummer++;
            Steuerung = steuerung;
            QuellOrdner = quelle;
            ZielOrdner = ziel;

            Programmiersprache = ProgrammierspracheBestimmen(mw, quelle);
            PlcKategorie = KategorieBestimmen(mw, quelle);
            Bezeichnung = BezeichnungBestimmen(mw, quelle);
        }

        private static PlcKategorie KategorieBestimmen(MainWindow mw, string quelle)
        {
            foreach (var kategorie in mw.AlleWerte.AlleKategorien.Where(kategorie => quelle.Contains(kategorie.Value.Prefix)))
            {
                return kategorie.Key;
            }

            MessageBox.Show("Bezeichnungsproblem: " + quelle);
            return PlcKategorie.AdsRemote;
        }

        private static PlcSprachen ProgrammierspracheBestimmen(MainWindow mw, string quelle)
        {
            foreach (var sprache in mw.AlleWerte.AlleProgrammiersprachen.Where(sprache => quelle.Contains(sprache.Value.Prefix)))
            {
                return sprache.Key;
            }

            MessageBox.Show("Bezeichnungsproblem: " + quelle);
            return PlcSprachen.As;
        }

        private static string BezeichnungBestimmen(MainWindow mw, string quelle)
        {
            var prefix = "_XX_YY_ZZ_";

            foreach (var sprache in mw.AlleWerte.AlleProgrammiersprachen.Where(sprache => quelle.Contains("_" + sprache.Value.Prefix)))
            {
                prefix = "_" + sprache.Value.Prefix;
            }

            var pos = quelle.IndexOf(prefix, StringComparison.Ordinal);
            var laenge = prefix.Length;

            return quelle.Substring(pos + laenge);
        }
    }
}