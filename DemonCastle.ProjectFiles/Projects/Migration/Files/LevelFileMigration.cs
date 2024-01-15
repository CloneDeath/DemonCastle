using DemonCastle.Files;
using DemonCastle.Files.Common;
using Newtonsoft.Json.Linq;

namespace DemonCastle.ProjectFiles.Projects.Migration.Files;

[MigrationType(typeof(LevelFile))]
public static class LevelFileMigration {
	[ToVersion(2)]
	public static void Migrate(JObject file) {
		var tiles = file[nameof(LevelFile.Tiles)] ?? new JArray();
		foreach (var tile in tiles.Values()) {
			var width = tile.Value<int>("Width");
			var height = tile.Value<int>("Height");
			tile[nameof(TileData.Size)] = new JObject(new Size(width, height));
		}
	}
}