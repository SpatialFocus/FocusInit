# Spatial Focus initialize project wizard

[![Nuget](https://img.shields.io/nuget/v/SpatialFocus.FocusInit)](https://www.nuget.org/packages/SpatialFocus.FocusInit/) [![Build and publish NuGet](https://github.com/SpatialFocus/FocusInit/workflows/Build%20and%20publish%20NuGet/badge.svg?branch=master)](https://github.com/SpatialFocus/FocusInit/actions)

Initialize an empty folder for a new project by copying and modifying files from our [repository-template](https://github.com/SpatialFocus/repository-template). This includes:

- .gitignore and .editorconfig
- stylecop.json and ReSharper settings
- MSBuild properties
- Solution file including the default _Solution Items_ solution folder
- README.md

Placeholders in these files will be filled with solution name and author information.

Additionally, the wizard supports the creation of .NET Core projects in the solution. Currently, these project templates are available:

1) Console app
2) Empty web
3) Web API
4) Web MVC
5) Xamarin Forms Shell (using our [Xamarin Forms Shell template](https://github.com/SpatialFocus/DotNetNew.XamarinFormsShell))
6) Blazor Server
7) Blazor Wasm

After setting up one of these demo projects, typical generic projects can be added as well:

- Business (class library for business logic)
- Shared (class library for shared data)
- Test (xUnit test project)

## Install the dotnet tool

```
dotnet tool install --global SpatialFocus.FocusInit --version 0.4.0
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

          __                            _       _ _
         / _| ___   ___ _   _ ___      (_)_ __ (_) |_
        | |_ / _ \ / __| | | / __|_____| | '_ \| | __|
        |  _| (_) | (__| |_| \__ \_____| | | | | | |_
        |_|  \___/ \___|\__,_|___/     |_|_| |_|_|\__|


Initialize this folder for a new project.
Enter solution name [NewConsoleProject]
Enter company name [Spatial Focus GmbH]
Template repository cloned successfully.
Solution files have been copied and modified.
Create additional projects? [Y/n] n
Finished. Have fun!

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
