// <copyright file="Program.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace FocusInit
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using LibGit2Sharp;
	using McMaster.Extensions.CommandLineUtils;

	public static class Program
	{
		public static void Main()
		{
			FileSystemHelper fileSystemHelper = new FileSystemHelper();

			Console.WriteLine(@"
	  __                            _       _ _   
	 / _| ___   ___ _   _ ___      (_)_ __ (_) |_ 
	| |_ / _ \ / __| | | / __|_____| | '_ \| | __|
	|  _| (_) | (__| |_| \__ \_____| | | | | | |_ 
	|_|  \___/ \___|\__,_|___/     |_|_| |_|_|\__|
	                                              
");
			Console.WriteLine("Initialize this folder for a new project.");

			string targetDirectory = string.IsNullOrEmpty(Settings.WorkingDir) ? Directory.GetCurrentDirectory() : Settings.WorkingDir;

			if (Directory.GetFiles(targetDirectory).Length > 0 || Directory.GetDirectories(targetDirectory).Length > 0)
			{
				if (!Prompt.GetYesNo("Directory is not empty. Delete all files/folders and continue?", false, ConsoleColor.Red))
				{
					return;
				}
			}

			fileSystemHelper.CleanupAndCreateWorkDir();

			string currentDirectoryName = new DirectoryInfo(Directory.GetCurrentDirectory()).Name;
			string solutionName = Prompt.GetString("Enter solution name", currentDirectoryName, ConsoleColor.DarkCyan);
			string companyName = Prompt.GetString("Enter company name", Settings.DefaultCompany, ConsoleColor.DarkCyan);
			Repository.Clone(Settings.RepoUrl, Settings.RepoDir);

			Console.WriteLine("Template repository cloned.");

			SolutionFilesHelper solutionFilesHelper = new SolutionFilesHelper(solutionName);
			solutionFilesHelper.InitializeSolution(solutionName);
			solutionFilesHelper.InitializeDotSettings(solutionName, companyName);
			solutionFilesHelper.InitializeStylecop(companyName);
			solutionFilesHelper.InitializeReadme(solutionName, companyName);
			solutionFilesHelper.CopyRemainingFiles();

			Console.WriteLine("Solution files have been copied and modified.");

			fileSystemHelper.DeleteRepository();

			bool createProjects = Prompt.GetYesNo("Create additional projects?", true, ConsoleColor.DarkCyan);

			if (createProjects)
			{
				Program.CreateProjects(solutionName);
			}

			Console.WriteLine("Finished. Have fun!");
		}

		private static void CreateProjects(string solutionName)
		{
			DotnetCliHelper cliHelper = new DotnetCliHelper(Settings.WorkingDir, solutionName);
			int i;

			do
			{
				Console.WriteLine("  1) Console app");
				Console.WriteLine("  2) Empty web");
				Console.WriteLine("  3) Web API");
				Console.WriteLine("  4) Web MVC");
				Console.WriteLine("  5) Xamarin Forms Shell");
				Console.WriteLine("  6) Blazor Server");
				Console.WriteLine("  7) Blazor Wasm");
				i = Prompt.GetInt("Select project type", null, ConsoleColor.DarkCyan);
			}
			while (i <= 0 || i > 7);

			List<string> projectsToReference = new List<string>();

			switch (i)
			{
				case 1:
					string consoleProject = cliHelper.CreateProject("console");
					cliHelper.AddProjectToSolution(consoleProject);
					projectsToReference.Add(consoleProject);
					break;
				case 2:
					string emptyWebProject = cliHelper.CreateProject("web", "Web");
					cliHelper.AddProjectToSolution(emptyWebProject);
					projectsToReference.Add(emptyWebProject);
					break;
				case 3:
					string webApiProject = cliHelper.CreateProject("webapi", "Web");
					cliHelper.AddProjectToSolution(webApiProject);
					projectsToReference.Add(webApiProject);
					break;
				case 4:
					string mvcProject = cliHelper.CreateProject("mvc", "Web");
					cliHelper.AddProjectToSolution(mvcProject);
					projectsToReference.Add(mvcProject);
					break;
				case 5:
					// Make sure the Xamarin Forms Shell template is installed
					cliHelper.InstallCustomProjectTemplate("SpatialFocus.DotNetNew.XamarinFormsShell::0.1.2");

					string xfShellProject = cliHelper.CreateMultiProject("focus-xfshell");

					cliHelper.AddProjectToSolution(xfShellProject);
					cliHelper.AddProjectToSolution($"{xfShellProject}.Android");
					cliHelper.AddProjectToSolution($"{xfShellProject}.iOS");

					projectsToReference.Add(xfShellProject);
					break;
				case 6:
					string blazorProject = cliHelper.CreateProject("blazorserver", "Blazor");
					cliHelper.AddProjectToSolution(blazorProject);
					projectsToReference.Add(blazorProject);
					break;
				case 7:
					string wasmProject = cliHelper.CreateProject("blazorwasm", "Blazor");
					cliHelper.AddProjectToSolution(wasmProject);
					projectsToReference.Add(wasmProject);
					break;
			}

			if (Prompt.GetYesNo("Create business project?", false, ConsoleColor.DarkCyan))
			{
				string businessProject = cliHelper.CreateProject("classlib", "Business");

				cliHelper.AddProjectToSolution(businessProject);

				foreach (string project in projectsToReference)
				{
					cliHelper.AddReferenceToProject(project, businessProject);
				}

				projectsToReference.Add(businessProject);
			}

			if (Prompt.GetYesNo("Create shared project?", false, ConsoleColor.DarkCyan))
			{
				string sharedProject = cliHelper.CreateProject("classlib", "Shared");

				cliHelper.AddProjectToSolution(sharedProject);

				foreach (string project in projectsToReference)
				{
					cliHelper.AddReferenceToProject(project, sharedProject);
				}
			}

			if (Prompt.GetYesNo("Create test project?", false, ConsoleColor.DarkCyan))
			{
				string testProject = cliHelper.CreateProject("xunit", "Test");

				cliHelper.AddProjectToSolution(testProject);

				foreach (string project in projectsToReference)
				{
					cliHelper.AddReferenceToProject(testProject, project);
				}
			}
		}
	}
}