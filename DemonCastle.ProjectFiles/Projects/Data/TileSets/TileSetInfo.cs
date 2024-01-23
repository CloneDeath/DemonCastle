using System;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.TileSets;

public class TileSetInfo : FileInfo<TileSetFile> {
	public TileSetInfo(FileNavigator<TileSetFile> file) : base(file) {
		TileSet = new TileInfoCollection(file, Resource.Tiles);
	}

	public string Name {
		get => Resource.Name;
		set => SaveField(ref Resource.Name, value);
	}

	public Guid Id {
		get => Resource.Id;
		set => SaveField(ref Resource.Id, value);
	}

	public IEnumerableInfo<TileInfo> TileSet { get; }
}