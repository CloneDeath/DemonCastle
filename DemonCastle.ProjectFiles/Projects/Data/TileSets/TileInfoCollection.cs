using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.Files.Animations;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.TileSets;

public class TileInfoCollection : ObservableCollectionInfo<TileInfo, TileData> {
	private readonly IFileNavigator _file;
	public TileInfoCollection(IFileNavigator file, List<TileData> data) : base(new TileInfoFactory(file, data), data) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public TileInfo? GetTileInfo(Guid tileId) => this.FirstOrDefault(t => t.Id == tileId);
}

public class TileInfoFactory : IInfoFactory<TileInfo, TileData> {
	private readonly IFileNavigator _file;
	private readonly List<TileData> _data;

	public TileInfoFactory(IFileNavigator file, List<TileData> data) {
		_file = file;
		_data = data;
	}

	public TileInfo CreateInfo(TileData data) => new(_file, data);

	public TileData CreateData() {
		var lastTile = _data.LastOrDefault();

		var previousState = lastTile?.States.FirstOrDefault(s => s.Id == lastTile.InitialState);
		var previousAnimation = lastTile?.Animations.FirstOrDefault(a => a.Id == previousState?.Animation);
		var previousFrame = previousAnimation?.Frames.FirstOrDefault();

		var tileData = new TileData();
		var animation = new AnimationData {
			Name = previousAnimation?.Name ?? "Default"
		};
		tileData.Animations.Add(animation);
		var nextSprite = GetNextSprite(previousFrame);
		animation.Frames.Add(new FrameData {
			Source = previousFrame?.Source ?? string.Empty,
			SpriteId = nextSprite?.Id ?? Guid.Empty
		});
		var state = new EntityStateData {
			Name = previousState?.Name ?? "Default",
			Animation = animation.Id
		};
		tileData.States.Add(state);
		tileData.InitialState = state.Id;
		tileData.Name = nextSprite?.Name ?? tileData.Name;
		return tileData;
	}

	private ISpriteDefinition? GetNextSprite(FrameData? previousFrame) {
		if (previousFrame == null) return null;
		if (!_file.FileExists(previousFrame.Source)) return null;

		var sprite = _file.GetSprite(previousFrame.Source);
		var sprites = sprite.Sprites.ToList();
		var index = sprites.FindIndex(s => s.Id == previousFrame.SpriteId);
		if (index == -1 || index >= sprites.Count - 1) return null;
		return sprites[index + 1];
	}
}
