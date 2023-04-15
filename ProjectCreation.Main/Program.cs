using System.Diagnostics;
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
				Console.WriteLine("Please select the project you wish to create below:");
				Console.WriteLine();
				Console.WriteLine("1: C# Command Program (.NET 6.0)");
				Console.WriteLine("2: Basic Website (HTML, CSS, JS)");

				string spString = Console.ReadLine();
				int selectedProject;

				if (!int.TryParse(spString, out selectedProject) || (selectedProject != 1 && selectedProject != 2))
				{
					Console.Clear();
					Console.WriteLine("Invalid Input, please try again.");
				}
				else
				{
					validInput = true;

					//int selectedProject = Convert.ToInt16(Console.ReadLine());

					// Checks user input, if user inputted 1, will execute the function for creating a c# command line program.
					// If user inputted 2, will execute the function for creating a basic website (html, css, js).

					switch (selectedProject)
					{
						case 1:
							CommandLineInterface();
							break;

						case 2:
							BasicWebsite();
							break;
					}
				}
			}
		}
		// Creates the files for a c# command line interface program (.net 6)
		static void CommandLineInterface()
		{
			Console.WriteLine("C# Command Line Program (.NET 6.0) Selected");
			Console.WriteLine("Enter the directory in which to create your project:");

			string directoryPath = Console.ReadLine();

			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			Directory.SetCurrentDirectory(directoryPath); // change current directory to user input
			Console.WriteLine("What do you want to call it?");
			string projectname = Console.ReadLine();
			string command = "dotnet";
			string arguments = $"new console -n {projectname}";

			ProcessStartInfo processInfo = new ProcessStartInfo(command, arguments);
			processInfo.CreateNoWindow = false;
			processInfo.UseShellExecute = false;

			Process process = Process.Start(processInfo);
			process.WaitForExit();
		}

		// Creates the files for a basic website
		static void BasicWebsite()
		{
			Console.WriteLine("Basic Website (HTML, CSS, JS) Selected");
			Console.WriteLine("Enter the path of where you want your project:");

			string directoryPath = Console.ReadLine();
			// Takes the directory provided by the user and checks to see if it actually exists.
			// If it does that it will create a variable with that directory for further use.
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			// write the contents of index.html
			string indexPath = Path.Combine(directoryPath, "index.html");
			string indexHtml = "<!DOCTYPE html>\n<html>\n<head>\n\t<title>My Webpage</title>\n\t<link rel=\"stylesheet\" href=\"style.css\">\n</head>\n<body>\n\t<h1>Hello, world!</h1>\n\t<script src=\"main.js\"></script>\n</body>\n</html>";
			File.WriteAllText(indexPath, indexHtml);

			// write the contents of main.js
			string jsPath = Path.Combine(directoryPath, "main.js");
			string jsCode = "console.log(\"Hello, world!\");";
			File.WriteAllText(jsPath, jsCode);

			// write the contents of style.css
			string cssPath = Path.Combine(directoryPath, "style.css");
			string cssCode = "h1 {\n\tcolor: red;\n}";
			File.WriteAllText(cssPath, cssCode);
		}
	}
}