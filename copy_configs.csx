#load "pull_file.csx"

var cwd = Directory.GetCurrentDirectory();

PullFile($"file://{cwd}/../dotnet-configs/.editorconfig");
PullFile($"file://{cwd}/../dotnet-configs/nuget.config");
