using System;
using System.IO;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;
using Newtonsoft.Json.Linq;

namespace DemonCastle.ProjectFiles.Projects.Migration;

public class GameFileMigrator {
	private readonly ProjectResources _resources;
	public GameFileMigrator(ProjectResources resources) {
		_resources = resources;
	}

	public FileNavigator<T> GetFile<T>(string path)
		where T : IGameFile, new() {
		var file = Serializer.Deserialize<JObject>(File.ReadAllText(path));
		var targetVersion = new T().FileVersion;
		var version = file.GetValue(nameof(IGameFile.FileVersion))?.Value<int>() ?? 0;
		if (version < targetVersion) {
			Migrate<T>(path, version, targetVersion);
		}
		return new FileNavigator<T>(path, _resources);
	}

	private static void Migrate<T>(string path, int initialVersion, int targetVersion) {
		var file = Serializer.Deserialize<JObject>(File.ReadAllText(path));
		for (var version = initialVersion; version < targetVersion; version++) {
			if (version == 0) {
				file[nameof(IGameFile.FileVersion)] = 1;
				continue;
			}
			var migrator = MigratorFactory.GetMigrator<T>(version);
			migrator.Migrate(file);
			file[nameof(IGameFile.FileVersion)] = version + 1;
		}
		File.WriteAllText(path, Serializer.Serialize(file));
	}
}

public static class MigratorFactory {
	public static FileMigrator GetMigrator<T>(int version) {
		throw new NotImplementedException();
	}
}

public class FileMigrator {
	public void Migrate(JObject file) {
		throw new NotImplementedException();
	}
}