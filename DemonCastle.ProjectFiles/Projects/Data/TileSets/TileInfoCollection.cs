using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.Files.Animations;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
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
		if (lastTile == null) return new TileData();

		var tileData = new TileData();
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
		return tileData;
	}
}
