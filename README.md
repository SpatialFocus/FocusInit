# Spatial Focus initialize project wizard

[![Nuget](https://img.shields.io/nuget/v/SpatialFocus.FocusInit)](https://www.nuget.org/packages/SpatialFocus.FocusInit/)

## Install the dotnet tool

```
dotnet tool install --global SpatialFocus.FocusInit --version 0.2.0
```

Install the tool globally. You can invoke the tool using the following command: `focus-init`

## Using the tool

Starts the tool and guides you through the setup wizard

```
focus-init
```

## Example usage

```
PS C:\temp> mkdir NewConsoleProject

    Directory: C:\temp

Mode                LastWriteTime         Length Name
----                -------------         ------ ----
d-----       13.05.2020     15:19                NewConsoleProject

PS C:\temp> cd .\NewConsoleProject\

PS C:\temp\NewConsoleProject> focus-init
Enter solution name [NewConsoleProject]
Enter company name [Spatial Focus GmbH]
Template repository cloned successfully.
Solution files have been copied and modified.
Create additional projects? [Y/n] n
Finished.

PS C:\temp\NewConsoleProject> dir

    Directory: C:\temp\NewConsoleProject

Mode                LastWriteTime         Length Name
----                -------------         ------ ----
-a----       13.05.2020     15:20          10765 .editorconfig
-a----       13.05.2020     15:20           6909 .gitignore
-a----       13.05.2020     15:20            216 Directory.Build.props
-a----       13.05.2020     15:20            668 Directory.Build.targets
-a----       13.05.2020     15:20            912 NewConsoleProject.sln
-a----       13.05.2020     15:20          22430 NewConsoleProject.sln.DotSettings
-a----       13.05.2020     15:20             99 README.md
-a----       13.05.2020     15:20            452 stylecop.json
```

----

Made with :heart: by [Spatial Focus](https://spatial-focus.net/)