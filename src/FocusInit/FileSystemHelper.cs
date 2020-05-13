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
			if (Directory.Exists(Settings.WorkingDir))
			{
				// Deal with readonly files (typically in .git folder)
				SetDirectoryNormal(Settings.WorkingDir);
				Directory.Delete(Settings.WorkingDir, true);
			}

			Directory.CreateDirectory(Settings.WorkingDir);
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