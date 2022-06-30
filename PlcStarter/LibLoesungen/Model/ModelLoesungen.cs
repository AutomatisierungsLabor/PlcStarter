using System;
using System.IO;
using System.Threading;
using NETCore.Encrypt;

namespace LibLoesungen.Model;

public class ModelLoesungen
{
    private  string _loesungText;

    public ModelLoesungen()
    {


        System.Threading.Tasks.Task.Run(ModelTask);
    }



    private static void ModelTask()
    {
        while (true)
        {


            Thread.Sleep(100);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    public void NeueLoesungLaden(string projektPfad)
    {
        var solName = Path.Combine(projektPfad, "Solution", "Solution.enc");

        try
        {
            var aesKey = EncryptProvider.CreateAesKey();
            aesKey.Key = "7L2HzKXGJrJkdpy7xDjNB1jGTmU3hccZ";
            aesKey.IV = "s1gyBZNWEL3LYvkc";
            var buffer = File.ReadAllBytes(solName);
            var decrypted = EncryptProvider.AESDecrypt(buffer, aesKey.Key, aesKey.IV);
            _loesungText = decrypted.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public string GetLoesungText() => _loesungText;
}