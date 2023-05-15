using System;
using System.Diagnostics;

public static void RunCommand(string cmd, string args)
{
	Console.WriteLine($"{cmd} {args}");
	var newProc = Process.Start(new ProcessStartInfo(cmd, args) { UseShellExecute = false, RedirectStandardOutput = true, });
	while (!newProc.StandardOutput.EndOfStream)
	{
		Console.WriteLine(newProc.StandardOutput.ReadLine());
	}
	newProc.WaitForExit();
}