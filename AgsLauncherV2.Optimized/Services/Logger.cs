namespace AgsLauncherV2.Optimized.Services
{
    internal abstract class Logger
    {
        public static async void InitializeLogger()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\Logs\\"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\Logs\\");
                await Task.Delay(1000);
            }
            var fileInitText = $"# AveryGame Launcher Version 2, Cutting Edge branch; version {Avgl2CeVersion.Split('?')[0]}.\r\n# This is a pre-release version of the launcher, and as such, may contain bugs.\r\n# Please report any bugs you find to the AveryGame Discord server.\r\n# https://discord.gg/K5SmweCMNr\r\n\r\n[INFO]: Logger instance initialized at {DateTime.Now}\r\n{LogAsString(LogTypeEnum.Debug)}Build configuration is {BuildConfiguration}\r\n";
            Console.WriteLine($"Sending to LogFile: {fileInitText}");
            await File.WriteAllTextAsync(LogPath, fileInitText);
        }
        public static void Log(LogTypeEnum lt, string message)
        {
            Console.WriteLine($"Sending to LogFile: {LogAsString(lt)}{message}");
            Debug.WriteLine(LogPath, $"{LogAsString(lt)}{message} \r\n");
            if (lt == LogTypeEnum.Error)
            {
                //BasicLogic.CrashpadHandle();
            }
        }
    }
}
