using System;
using System.IO;
using System.Windows;
using NETCore.Encrypt;

namespace PlcStarter.Model;

public static partial class AlleJobs
{
    internal static void Copy(string sourceDirectory, string targetDirectory)
    {
        var diSource = new DirectoryInfo(sourceDirectory);
        var diTarget = new DirectoryInfo(targetDirectory);

        Log.Debug($"Copy: von {sourceDirectory} nach {targetDirectory}");

        if (!Directory.Exists(sourceDirectory))
        {
            Log.Error("Der Quellordner ist nicht vorhanden: " + sourceDirectory);
            MessageBox.Show("Der Quellordner ist nicht vorhanden: " + sourceDirectory);
            throw new Exception("Der Quellordner ist nicht vorhanden: " + sourceDirectory);
        }

        CopyAll(diSource, diTarget);
    }
    internal static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        try
        {
            Directory.CreateDirectory(target.FullName);
            var dateiNameDeleteMe = "-";
            foreach (var fi in source.GetFiles())
            {
                if (fi.ToString().Contains("DeleteMeNot"))
                {
                    if (!System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains("kurt.linder")) continue;

                    dateiNameDeleteMe = fi.FullName.Replace("DeleteMeNot", "DeleteMe");
                    var aesKey = EncryptProvider.CreateAesKey();
                    aesKey.Key = "7L2HzKXGJrJkdpy7xDjNB1jGTmU3hccZ";
                    aesKey.IV = "s1gyBZNWEL3LYvkc";
                    var buffer = File.ReadAllBytes(fi.FullName);
                    var decrypted = EncryptProvider.AESDecrypt(buffer, aesKey.Key, aesKey.IV);
                    File.WriteAllBytes(dateiNameDeleteMe, decrypted);
                }
                else
                {
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }
            }

            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }

            if (dateiNameDeleteMe != "-") File.Delete(dateiNameDeleteMe);
        }
        catch (Exception exp)
        {
            Log.Error(exp.ToString());
            MessageBox.Show(exp.ToString());
        }
    }
}