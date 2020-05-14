// <copyright file="DotnetCliHelper.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace FocusInit
{
	using System.Diagnostics;

	public class DotnetCliHelper
	{
		public DotnetCliHelper(string workDir, string solutionName)
		{
			WorkDir = workDir;
			SolutionName = solutionName;
		}

		protected string SolutionName { get; }

		protected string WorkDir { get; }

		public void AddProjectToSolution(string projectPath)
		{
			string solutionFolder = !string.IsNullOrEmpty(WorkDir) ? WorkDir + "/" : string.Empty;
			string solutionFileName = SolutionName + ".sln";

			string addToSolutionCmd = $"sln {solutionFolder}{solutionFileName} add {projectPath}";
			Process process = Process.Start("dotnet", addToSolutionCmd);

			process.WaitForExit(3000);
		}

		public void AddReferenceToProject(string projectPath, string referenceProjectPath)
		{
			string addReferenceCmd = $"add {projectPath} reference {referenceProjectPath}";
			Process process = Process.Start("dotnet", addReferenceCmd);
			process.WaitForExit(3000);
		}

		public string CreateMultiProject(string type)
		{
			string srcFolder = !string.IsNullOrEmpty(WorkDir) ? WorkDir + "/src" : "src";

			string createProjectCmd = $"new {type} -n {SolutionName} -o {srcFolder}";
			Process process = Process.Start("dotnet", createProjectCmd);

			process.WaitForExit(3000);

			return $"{srcFolder}/{SolutionName}";
		}

		public string CreateProject(string type, string projectSuffix)
		{
			string srcFolder = !string.IsNullOrEmpty(WorkDir) ? WorkDir + "/src" : "src";

			string createProjectCmd = $"new {type} -n {SolutionName}.{projectSuffix} -o {srcFolder}/{SolutionName}.{projectSuffix}";
			Process process = Process.Start("dotnet", createProjectCmd);

			process.WaitForExit(3000);

			return $"{srcFolder}/{SolutionName}.{projectSuffix}";
		}

		public void InstallCustomProjectTemplate(string type)
		{
			string installTemplateCmd = $"new -i {type}";
			Process process = Process.Start("dotnet", installTemplateCmd);

			process.WaitForExit(3000);
		}
	}
}