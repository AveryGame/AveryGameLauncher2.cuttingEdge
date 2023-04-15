namespace AgsLauncherV2.Optimized.Services
{
    internal class Logger
    {
        public static async void InitializeLogger()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\Logs\\"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\Logs\\");
                await Task.Delay(1000);
            }
            string FileInitText = $"# AveryGame Launcher Version 2, Cutting Edge branch; version {Public.Avgl2cEVersion.Split('?')[0]}.\r\n# This is a pre-release version of the launcher, and as such, may contain bugs.\r\n# Please report any bugs you find to the AveryGame Discord server.\r\n# https://discord.gg/K5SmweCMNr\r\n\r\n[INFO]: Logger instance initialized at {DateTime.Now}\r\n{LogAsString(LogType.Debug)}Build configuration is {BuildConfiguration}\r\n";
            Console.WriteLine($"Sending to LogFile: {FileInitText}");
            File.WriteAllText(Public.LogPath, FileInitText);
        }
        public static void Log(LogType lt, string message)
        {
            Console.WriteLine($"Sending to LogFile: {LogAsString(lt)}{message}");
            File.AppendAllText(Public.LogPath, $"{LogAsString(lt)}{message} \r\n");
            if (lt == LogType.Error)
            {
                Services.BasicLogic.CrashpadHandle();
            }
        }
    }
}
