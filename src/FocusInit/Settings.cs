// <copyright file="Settings.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace FocusInit
{
	using System.IO;

	public static class Settings
	{
		// Create a subfolder for debug purposes
#if DEBUG
		public const string WorkingDir = "SubDirectory";
#else
		public const string WorkingDir = "";
#endif

		public const string DefaultCompany = "Spatial Focus GmbH";

		public const string DefaultCompanyTag = "[Spatial Focus](https://spatial-focus.net/)";

		public const string RepoUrl = "https://github.com/SpatialFocus/repository-template";

		public static string RepoDir => Path.Combine(Settings.WorkingDir, "repo");
	}
}