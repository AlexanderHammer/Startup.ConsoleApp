using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace UpdateOnStartup
{
    public static class Helper
    {
        public static Configuration ReadConfiguration()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = Directory.GetCurrentDirectory() + @"\App.config"
            };
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        public static string ReadSetting(Configuration configuration, string key)
        {
            return configuration.AppSettings.Settings[key].Value ?? "Key not found";
        }

        public static void UpsertFile(string currentFilePath, string newFilePath)
        {
            if (File.Exists(newFilePath) && File.Exists(currentFilePath))
            {
                var newDate = File.GetCreationTimeUtc(newFilePath);
                var currentDate = File.GetCreationTimeUtc(currentFilePath);

                if (newDate > currentDate)
                {
                    File.Copy(newFilePath, currentFilePath, true);
                }
            }
            else if (File.Exists(newFilePath) && !File.Exists(currentFilePath))
            {
                File.Copy(newFilePath, currentFilePath);
            }
        }

        public static string CreateCall(string call, string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                call += $" {args[i]}";
            }
            return call;
        }

        public static void StartProgram(string target)
        {
            Console.WriteLine($"Trying to run: {target}");
            try
            {
                Process.Start(target);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
        }
    }
}
