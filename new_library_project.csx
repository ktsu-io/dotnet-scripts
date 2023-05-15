#load "run_command.csx"
#load "pull_file.csx"
#load "configs.csx"

using System;
using System.IO;

Console.Write("Project name: ");
var projectName = Console.ReadLine();

if (!string.IsNullOrEmpty(projectName))
{
	Directory.SetCurrentDirectory(".."); //change to the git directory
	RunCommand("dotnet", $"new sln -o {projectName}");
	Directory.SetCurrentDirectory(projectName); //change to the solution directory
	Directory.CreateDirectory($"{projectName}");

	var repo = "https://raw.githubusercontent.com/matt-edmondson/dotnet-configs/main";
	GetConfigList().ForEach(c => PullFile($"{repo}/{c}"));
	PullFile($"{repo}/empty.csproj");

	File.Move("empty.csproj", $"{projectName}\\{projectName}.csproj");
	File.Move("project.props.default", "project.props");

	Directory.SetCurrentDirectory(projectName); //change to the project directory

	var classTemplate =
$@"namespace medmondson.{projectName}
{{
	public class {projectName}
	{{
	}}
}}
";
	File.WriteAllText($"{projectName}.cs", classTemplate);

	Directory.SetCurrentDirectory(".."); //change to the solution directory
	RunCommand("dotnet", $"sln {projectName}.sln add {projectName}\\{projectName}.csproj --in-root");
}
else
{
	Console.WriteLine("Please provide a project name next time");
}