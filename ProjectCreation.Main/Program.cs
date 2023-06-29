using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace ProjectCreation.Main
{
    internal class Program
    {
        static void Main()
        {
            bool validInput = false;

            // Introduction
            Console.WriteLine("Welcome to the Project Creation Interface.");

            while (!validInput)
            {
                var sb = new StringBuilder();

                // Selection Dialogue
                sb.AppendLine("Please select the project you wish to create below:");
                sb.AppendLine();
                sb.AppendLine("1: C# Command Program (.NET 7.0)");
                sb.AppendLine("2: Basic Website (HTML, CSS, JS)");
                sb.AppendLine("3: Forge Minecraft Mod (1.20.1, Requires Internet Connection)");

                Console.WriteLine(sb.ToString());

                string spString = Console.ReadLine();
                int selectedProject;

                if (!int.TryParse(spString, out selectedProject) || (selectedProject < 1 || selectedProject > 3))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input, please try again.");
                }
                else
                {
                    validInput = true;

                    switch (selectedProject)
                    {
                        default:
                            Console.WriteLine("An unexpected error occurred.");
                            break;
                        case 1:
                            CommandLineInterface();
                            break;
                        case 2:
                            BasicWebsite();
                            break;
                        case 3:
                            MinecraftMod();
                            break;
                    }
                }
            }
        }

        static void CommandLineInterface()
        {
            Console.WriteLine("C# Command Line Program (.NET 7.0) Selected");
            Console.WriteLine("Enter the directory in which to create your project:");
            string directoryPath = Console.ReadLine();

            CreateDirectoryIfNotExists(directoryPath);

            Directory.SetCurrentDirectory(directoryPath); // change current directory to user input
            Console.WriteLine("What do you want to call it?");
            string projectName = Console.ReadLine();
            string command = "dotnet";
            string arguments = $"new console --language \"C#\" -n {projectName}";

            ExecuteCommand(command, arguments);

            Console.WriteLine("C# Command Line Program (.NET 7.0) Created.");
            Console.ReadKey();
        }

        static void BasicWebsite()
        {
            Console.WriteLine("Basic Website (HTML, CSS, JS) Selected");
            Console.WriteLine("Enter the path of where you want your project:");
            string directoryPath = Console.ReadLine();

            CreateDirectoryIfNotExists(directoryPath);

            string indexPath = Path.Combine(directoryPath, "index.html");
            string indexHtml = "<!DOCTYPE html>\n<html>\n<head>\n\t<title>My Webpage</title>\n\t<link rel=\"stylesheet\" href=\"style.css\">\n</head>\n<body>\n\t<h1>Hello, world!</h1>\n\t<script src=\"main.js\"></script>\n</body>\n</html>";
            File.WriteAllText(indexPath, indexHtml);

            string jsPath = Path.Combine(directoryPath, "main.js");
            string jsCode = "console.log(\"Hello, world!\");";
            File.WriteAllText(jsPath, jsCode);

            string cssPath = Path.Combine(directoryPath, "style.css");
            string cssCode = "h1 {\n\tcolor: red;\n}";
            File.WriteAllText(cssPath, cssCode);

            Console.WriteLine("Basic Website Created.");
            Console.ReadKey();
        }

        static void MinecraftMod()
        {
            string fileUrl = "https://maven.minecraftforge.net/net/minecraftforge/forge/1.20.1-47.0.35/forge-1.20.1-47.0.35-mdk.zip";
            Console.WriteLine("Forge Minecraft Mod (1.20.1) Selected");
            Console.WriteLine("Enter the path of where you want your project:");
            string directoryPath = Console.ReadLine();

            CreateDirectoryIfNotExists(directoryPath);

            int lastSlashIndex = fileUrl.LastIndexOf('/');
            string zipPath = Path.Combine(directoryPath, fileUrl.Substring(lastSlashIndex + 1));

            DownloadFile(fileUrl, zipPath);
            ExtractZip(zipPath, directoryPath);

            Console.WriteLine("Mod Directory Prepared. You will still have to generate the runs for your respective IDE (Eclipse, Intellij, etc.)");
            Console.ReadKey();
        }

        static void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        static void ExecuteCommand(string command, string arguments)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo(command, arguments);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            Process process = Process.Start(processInfo);
            process.WaitForExit();
        }

        static void DownloadFile(string fileUrl, string zipPath)
        {
            using (var client = new HttpClient())
            {
                using (var stream = client.GetStreamAsync(fileUrl).Result)
                {
                    using (var fileStream = new FileStream(zipPath, FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }
        }

        static void ExtractZip(string zipPath, string directoryPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipPath, directoryPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}