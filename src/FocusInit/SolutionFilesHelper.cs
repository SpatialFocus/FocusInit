// <copyright file="SolutionFilesHelper.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace FocusInit
{
	using System;
	using System.IO;

	public class SolutionFilesHelper
	{
		public SolutionFilesHelper(string solutionName)
		{
			SolutionName = solutionName;
		}

		protected string SolutionName { get; }

		public void CopyRemainingFiles()
		{
			string[] fileNames = new[] { ".editorconfig", ".gitignore", "Directory.Build.props", "Directory.Build.targets" };

			foreach (string fileName in fileNames)
			{
				File.Copy(Path.Combine(Settings.RepoDir, fileName), Path.Combine(Settings.WorkingDir, fileName));
			}
		}

		public void InitializeDotSettings(string solutionName, string companyName)
		{
			string dotSettingsFilePath = Path.Combine(Settings.WorkingDir, solutionName + ".sln.DotSettings");

			File.Copy(Path.Combine(Settings.RepoDir, "Solution.sln.DotSettings"), dotSettingsFilePath);

			string contents = File.ReadAllText(dotSettingsFilePath);

			contents = contents.Replace("[CompanyName]", companyName, StringComparison.InvariantCulture);

			File.WriteAllText(dotSettingsFilePath, contents);
		}

		public void InitializeReadme(string solutionName, string companyName)
		{
			string readmeFilePath = Path.Combine(Settings.WorkingDir, "README.md");

			File.Copy(Path.Combine(Settings.RepoDir, "README.template"), readmeFilePath);

			string contents = File.ReadAllText(readmeFilePath);

			contents = contents.Replace("[SolutionName]", solutionName, StringComparison.InvariantCulture);

			string footerTag = companyName == Settings.DefaultCompany ? Settings.DefaultCompanyTag : companyName;
			contents = contents.Replace("[CompanyName]", footerTag, StringComparison.InvariantCulture);

			File.WriteAllText(readmeFilePath, contents);
		}

		public void InitializeSolution(string solutionName)
		{
			string solutionFileName = solutionName + ".sln";
			string solutionFilePath = Path.Combine(Settings.WorkingDir, solutionFileName);

			File.Copy(Path.Combine(Settings.RepoDir, "Solution.sln"), solutionFilePath);

			string contents = File.ReadAllText(solutionFilePath);

			contents = contents.Replace("[Solution.sln.DotSettings]", $"{solutionFileName}.DotSettings", StringComparison.InvariantCulture);
			contents = contents.Replace("[SolutionGuid]", Guid.NewGuid().ToString("D"), StringComparison.InvariantCulture);
			contents = contents.Replace("[SolutionItemsGuid]", Guid.NewGuid().ToString("D"), StringComparison.InvariantCulture);

			File.WriteAllText(solutionFilePath, contents);
		}

		public void InitializeStylecop(string companyName)
		{
			string stylecopFilePath = Path.Combine(Settings.WorkingDir, "stylecop.json");

			File.Copy(Path.Combine(Settings.RepoDir, "stylecop.json"), stylecopFilePath);

			string contents = File.ReadAllText(stylecopFilePath);

			contents = contents.Replace("[CompanyName]", companyName, StringComparison.InvariantCulture);

			File.WriteAllText(stylecopFilePath, contents);
		}
	}
}