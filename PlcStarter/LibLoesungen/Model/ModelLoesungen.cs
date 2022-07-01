using System;
using System.IO;
using System.Text;
using ICSharpCode.AvalonEdit;
using NETCore.Encrypt;

namespace LibLoesungen.Model;

public class ModelLoesungen
{
    private string _loesungPfad;
    private string _loesungDateiName;

    private const string AesKeyKey = "7L2HzKXGJrJkdpy7xDjNB1jGTmU3hccZ";
    private const string AesKeyIv = "s1gyBZNWEL3LYvkc";

    public string LoesungLesen(string projektPfad)
    {
        _loesungPfad = Path.Combine(projektPfad, "Solution");
        _loesungDateiName = Path.Combine(_loesungPfad, "Solution.enc");
        
        if (!File.Exists(_loesungDateiName))
        {
            if (!Directory.Exists(_loesungPfad)) Directory.CreateDirectory(_loesungPfad);

            File.Create(_loesungDateiName);
            return "Neue Datei erzeugt!";
        }

        var info = new FileInfo(_loesungDateiName);
        if (info.Length <= 0) return "Die Datei ist noch leer!";

        try
        {
            var aesKey = EncryptProvider.CreateAesKey();
            aesKey.Key = AesKeyKey;
            aesKey.IV = AesKeyIv;
            var buffer = File.ReadAllBytes(_loesungDateiName);
            var decrypted = EncryptProvider.AESDecrypt(buffer, aesKey.Key, aesKey.IV);
            var memoryStream = new MemoryStream(decrypted);
            var streamReader = new StreamReader(memoryStream);
            return streamReader.ReadToEnd();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public void LoesungSpeichern(TextEditor sourceCodeEditor)
    {
        var aesKey = EncryptProvider.CreateAesKey();
        aesKey.Key = "7L2HzKXGJrJkdpy7xDjNB1jGTmU3hccZ";
        aesKey.IV = "s1gyBZNWEL3LYvkc";
        var buffer = Encoding.ASCII.GetBytes(sourceCodeEditor.Text);
        var encrypted = EncryptProvider.AESEncrypt(buffer, aesKey.Key, aesKey.IV);
        File.WriteAllBytes(_loesungDateiName, encrypted);
    }
}