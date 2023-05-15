#load "pull_file.csx"
#load "configs.csx"

GetConfigList().ForEach(c => PullFile($"https://raw.githubusercontent.com/matt-edmondson/dotnet-configs/main/{c}"));
