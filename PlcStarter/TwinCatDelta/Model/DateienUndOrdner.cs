using System;
using System.IO;

namespace TwinCatDelta.Model
{
    public static class DateienUndOrdner
    {
        public static string OrdnerLoeschen(string ordner)
        {
            var dirInfo = new DirectoryInfo(ordner);
            if (!dirInfo.Exists) return $"{ordner} exisitiert nicht (mehr)\n";

            try
            {
                Directory.Delete(ordner, true);
                return ordner + ": gelöscht\n";
            }
            catch (Exception exp)
            {
                return $"{exp}\n";
            }
        }
    }
}