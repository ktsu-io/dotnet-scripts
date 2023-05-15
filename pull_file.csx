using System;
using System.Linq;
using System.Net;

public static void PullFile(string uriString, string fileName = "")
{
	Uri uri = new Uri(uriString);
	fileName = string.IsNullOrEmpty(fileName) ? uri.Segments.Last() : fileName;
	using WebClient client = new();
	Console.WriteLine($"Downloading {uri} to {fileName}");
	client.DownloadFile(uri, fileName);
}