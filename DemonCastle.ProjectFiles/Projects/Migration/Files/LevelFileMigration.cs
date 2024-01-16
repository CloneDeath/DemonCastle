using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.Files.Animations;
using DemonCastle.Files.BaseEntity;
using DemonCastle.Files.Common;
using Newtonsoft.Json.Linq;

namespace DemonCastle.ProjectFiles.Projects.Migration.Files;

[MigrationType(typeof(LevelFile))]
public static class LevelFileMigration {
	[ToVersion(2)]
	public static void Migrate(JObject file) {
		var tiles = file[nameof(LevelFile.Tiles)] ?? new JArray();
		foreach (var tile in tiles.Cast<JObject>()) {
			var width = tile.Value<int>("Width");
			var height = tile.Value<int>("Height");
			tile[nameof(TileData.Size)] = JObject.FromObject(new Size(width, height));
			tile.Remove("Width");
			tile.Remove("Height");

			var source = tile.Value<string>("Source") ?? string.Empty;
			var spriteId = Guid.Parse(tile.Value<string>("SpriteId") ?? Guid.Empty.ToString());
			var frame = new FrameData {
				Source = source,
				SpriteId = spriteId
			};
			var animation = new AnimationData {
				Name = "Default"
			};
			animation.Frames.Add(frame);
			var state = new EntityStateData {
				Name = "Default",
				Animation = animation.Id
			};
			tile[nameof(TileData.InitialState)] = state.Id;
			tile[nameof(TileData.Animations)] = JArray.FromObject(new List<AnimationData> { animation });
			tile[nameof(TileData.States)] = JArray.FromObject(new List<EntityStateData> { state });
			tile.Remove("Source");
			tile.Remove("SpriteId");
		}
	}
}