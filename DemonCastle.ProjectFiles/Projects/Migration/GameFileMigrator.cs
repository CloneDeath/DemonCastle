using System;
using System.Linq;
using System.Reflection;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Resources;
using Newtonsoft.Json.Linq;

namespace DemonCastle.ProjectFiles.Projects.Migration;

public class GameFileMigrator {
	private readonly ProjectResources _resources;

	public GameFileMigrator(ProjectResources resources) {
		_resources = resources;
	}

	public FileNavigator<T> GetFile<T>(FileNavigator file)
		where T : IGameFile, new() {
		var data = Serializer.Deserialize<JObject>(file.LoadContent());
		var targetVersion = new T().FileVersion;
		var version = data.GetValue(nameof(IGameFile.FileVersion))?.Value<int>() ?? 0;
		if (version < targetVersion) {
			Migrate<T>(file, version, targetVersion);
		}
		return new FileNavigator<T>(file, _resources);
	}

	private static void Migrate<T>(FileNavigator file, int initialVersion, int targetVersion) {
		var data = Serializer.Deserialize<JObject>(file.LoadContent());
		for (var version = initialVersion; version < targetVersion; version++) {
			if (version == 0) {
				data[nameof(IGameFile.FileVersion)] = 1;
				continue;
			}
			var migrator = MigratorFactory.GetMigrator<T>(version);
			migrator.Migrate(data);
			data[nameof(IGameFile.FileVersion)] = version + 1;
		}
		file.SaveContent(Serializer.Serialize(data));
	}
}

public static class MigratorFactory {
	public static FileMigrator GetMigrator<T>(int version) {
		var migrator = GetMigratorFor<T>();
		if (migrator == null) throw new MissingMigratorException(typeof(T), version);
		var method = migrator.GetMethods().FirstOrDefault(m => m.GetCustomAttribute<ToVersionAttribute>()?.Version == version + 1);
		if (method == null) throw new MissingMigratorException(typeof(T), version);
		return new FileMigrator(method);
	}

	private static Type? GetMigratorFor<T>() {
		var assembly = typeof(MigratorFactory).Assembly;
		return assembly.GetTypes().FirstOrDefault(type => type.GetCustomAttribute<MigrationTypeAttribute>()?.Type == typeof(T));
	}
}

public class FileMigrator {
	private readonly MethodInfo _method;

	public FileMigrator(MethodInfo method) {
		_method = method;
	}

	public void Migrate(JObject file) => _method.Invoke(null, new object?[] { file });
}