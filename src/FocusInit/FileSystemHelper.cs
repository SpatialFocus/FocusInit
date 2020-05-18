// <copyright file="FileSystemHelper.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace FocusInit
{
	using System.IO;

	public class FileSystemHelper
	{
		public void CleanupAndCreateWorkDir()
		{
			if (!string.IsNullOrEmpty(Settings.WorkingDir) && !Directory.Exists(Settings.WorkingDir))
			{
				Directory.CreateDirectory(Settings.WorkingDir);
				return;
			}

			string targetDirectory = !string.IsNullOrEmpty(Settings.WorkingDir) ? Settings.WorkingDir : Directory.GetCurrentDirectory();

			DirectoryInfo currentDirectory = new DirectoryInfo(targetDirectory);

			// Deal with readonly files (typically in .git folder)
			SetDirectoryNormal(currentDirectory.FullName);

			foreach (FileInfo file in currentDirectory.GetFiles())
			{
				file.Delete();
			}

			foreach (DirectoryInfo dir in currentDirectory.GetDirectories())
			{
				dir.Delete(true);
			}
		}

		public void DeleteRepository()
		{
			if (Directory.Exists(Settings.RepoDir))
			{
				// Deal with readonly files (typically in .git folder)
				SetDirectoryNormal(Settings.RepoDir);
				Directory.Delete(Settings.RepoDir, true);
			}
		}

		private void SetDirectoryNormal(string dir)
		{
			foreach (string subDir in Directory.GetDirectories(dir))
			{
				SetDirectoryNormal(subDir);

				DirectoryInfo subDirectory = new DirectoryInfo(subDir) { Attributes = FileAttributes.Normal };

				foreach (FileInfo file in subDirectory.GetFiles())
				{
					file.Attributes = FileAttributes.Normal;
				}
			}
		}
	}
}