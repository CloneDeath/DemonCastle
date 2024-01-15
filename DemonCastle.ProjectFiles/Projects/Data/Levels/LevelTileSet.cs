using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.Files.Animations;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class LevelTileSet {
	private readonly List<TileInfo> _tiles;

	protected LevelFile Level { get; }
	protected FileNavigator<LevelFile> File { get; }

	public IEnumerable<TileInfo> Tiles => _tiles;

	public LevelTileSet(LevelFile level, FileNavigator<LevelFile> file) {
		File = file;
		Level = level;
		_tiles = Level.Tiles.Select(t => new TileInfo(File, t)).ToList();
	}

	public TileInfo GetTileInfo(Guid tileId) {
		return _tiles.First(t => t.Id == tileId);
	}

	public TileInfo CreateTile() {
		var lastTile = Level.Tiles.LastOrDefault();
		var tileData = new TileData();
		if (lastTile != null) {
			var previousState = lastTile.States.FirstOrDefault(s => s.Id == lastTile.InitialState);
			var previousAnimation = lastTile.Animations.FirstOrDefault(a => a.Id == previousState?.Animation);
			var previousFrame = previousAnimation?.Frames.FirstOrDefault();
			var animation = new AnimationData();
			tileData.Animations.Add(animation);
			animation.Frames.Add(new FrameData {
				Source = previousFrame?.Source ?? string.Empty,
				SpriteId = previousFrame?.SpriteId ?? Guid.Empty
			});

			var state = new EntityStateData {
				Animation = animation.Id
			};
			tileData.States.Add(state);
			tileData.InitialState = state.Id;
		}
		Level.Tiles.Add(tileData);
		File.Save();

		var tileInfo = new TileInfo(File, tileData);
		_tiles.Add(tileInfo);
		return tileInfo;
	}

	public void DeleteTile(TileInfo tileInfo) {
		var index = Level.Tiles.FindIndex(t => t.Id == tileInfo.Id);
		if (index >= 0) {
			Level.Tiles.RemoveAt(index);
		}

		var infoIndex = _tiles.FindIndex(t => t.Id == tileInfo.Id);
		if (infoIndex >= 0) {
			_tiles.RemoveAt(infoIndex);
		}
	}
}