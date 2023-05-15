using System;
using System.Diagnostics;

var configName = "Release";
var config = $"--configuration {configName}";
var pkg = $"**/{configName}/*.nupkg";

var commitProcess = Process.Start(new ProcessStartInfo("git", $"log -1 --format=%H") { UseShellExecute = false, RedirectStandardOutput = true, });
var commit = commitProcess.StandardOutput.ReadToEnd().Trim();
commitProcess.WaitForExit();

var statusProcess = Process.Start(new ProcessStartInfo("git", $"status") { UseShellExecute = false, RedirectStandardOutput = true, });
var status = statusProcess.StandardOutput.ReadToEnd();
statusProcess.WaitForExit();

bool ahead = status.Contains("Your branch is ahead of");
bool uptodate = status.Contains("Your branch is up to date with");
bool pristine = !status.Contains("Changes not staged for commit") && !status.Contains("Changes to be committed");

if (ahead)
{
	Console.WriteLine("[Error] You have not pushed the latest code");
}
else if (!uptodate)
{
	Console.WriteLine("[Error] You have not pulled the latest code");
}
else if (!pristine)
{
	Console.WriteLine("[Error] You have not committed the latest code");
}
else
{
	Process.Start(new ProcessStartInfo("dotnet", $"clean {config}") { UseShellExecute = false }).WaitForExit();
	Process.Start(new ProcessStartInfo("dotnet", $"build {config}") { UseShellExecute = false }).WaitForExit();
	Process.Start(new ProcessStartInfo("dotnet", $"pack {config} -p:RepositoryCommit={commit}") { UseShellExecute = false }).WaitForExit();
	Process.Start(new ProcessStartInfo("dotnet", $"nuget push {pkg} --skip-duplicate") { UseShellExecute = false }).WaitForExit();
}