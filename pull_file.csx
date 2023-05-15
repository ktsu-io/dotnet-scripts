using System;
using System.Linq;
using System.Net;

public static void PullFile(string uriString, string fileName = "")
{
	Uri uri = new Uri(uriString);
	fileName = string.IsNullOrEmpty(fileName) ? uri.Segments.Last() : fileName;
	using WebClient client = new();
	client.DownloadFile(uri, fileName);
}

if (Args.Any())
{
	string fileName = string.Empty;
	if (Args.Count > 1)
	{
		fileName = Args[1];
	}
	PullFile(Args[0], fileName);
}