#load "pull_file.csx"
#load "configs.csx"

using System.Linq;

var cwd = Directory.GetCurrentDirectory();

GetConfigList().ForEach(c => PullFile($"file://{cwd}/../dotnet-configs/{c}"));
