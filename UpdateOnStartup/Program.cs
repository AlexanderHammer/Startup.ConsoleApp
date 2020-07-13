using static UpdateOnStartup.Helper;

namespace UpdateOnStartup
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = ReadConfiguration();
            var currentFilePath = ReadSetting(configuration, "currentFilePath");
            var newFilePath = ReadSetting(configuration, "newFilePath");

            UpsertFile(currentFilePath, newFilePath);

            StartProgram(CreateCall(currentFilePath, args));
        }
    }
}
