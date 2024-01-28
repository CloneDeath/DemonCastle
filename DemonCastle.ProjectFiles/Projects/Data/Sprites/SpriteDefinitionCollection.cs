using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class SpriteDefinitionCollection<TInfo, TFile> : ObservableCollectionInfo<TInfo, TFile>, IEnumerableInfo<ISpriteDefinition>
	where TInfo : ISpriteDefinition {
	private readonly IFileNavigator _file;

	public SpriteDefinitionCollection(IFileNavigator file, IInfoFactory<TInfo, TFile> factory, List<TFile> data) : base(factory, data) {
		_file = file;
	}

	protected override void Save() {
		_file.Save();
	}

	#region IEnumerableInfo<ISpriteDefinition>
	IEnumerator<ISpriteDefinition> IEnumerable<ISpriteDefinition>.GetEnumerator() => InfoItems.Cast<ISpriteDefinition>().GetEnumerator();
	ISpriteDefinition IEnumerableInfo<ISpriteDefinition>.this[int index] => base[index];
	ISpriteDefinition IEnumerableInfo<ISpriteDefinition>.AppendNew() => base.AppendNew();
	void IEnumerableInfo<ISpriteDefinition>.Remove(ISpriteDefinition item) => base.Remove((TInfo)item);
	int IEnumerableInfo<ISpriteDefinition>.IndexOf(ISpriteDefinition option) => base.IndexOf((TInfo)option);
	#endregion
}

public class SpriteAtlasInfoFactory : IInfoFactory<SpriteAtlasDataInfo, SpriteAtlasData> {
	private readonly SpriteAtlasInfo _spriteAtlas;

	public SpriteAtlasInfoFactory(SpriteAtlasInfo spriteAtlas) {
		_spriteAtlas = spriteAtlas;
	}

	public SpriteAtlasDataInfo CreateInfo(SpriteAtlasData data) => new(_spriteAtlas, data);
	public SpriteAtlasData CreateData() {
		var lastSprite = _spriteAtlas.Sprites.LastOrDefault() as SpriteAtlasDataInfo;
		return new SpriteAtlasData {
			X = lastSprite?.X + lastSprite?.Width ?? 0,
			Y = lastSprite?.Y ?? 0,
			Height = lastSprite?.Height ?? 16,
			Width = lastSprite?.Width ?? 16,
			FlipHorizontal = lastSprite?.FlipHorizontal ?? false
		};
	}
}

public class SpriteGridInfoFactory : IInfoFactory<SpriteGridDataInfo, SpriteGridData> {
	private readonly SpriteGridInfo _spriteGrid;

	public SpriteGridInfoFactory(SpriteGridInfo spriteGrid) {
		_spriteGrid = spriteGrid;
	}

	public SpriteGridDataInfo CreateInfo(SpriteGridData data) => new(_spriteGrid, data);
	public SpriteGridData CreateData() {
		var lastSprite = _spriteGrid.Sprites.LastOrDefault() as SpriteGridDataInfo;
		return new SpriteGridData {
			X = lastSprite?.X + 1 ?? 0,
			Y = lastSprite?.Y ?? 0,
			FlipHorizontal = lastSprite?.FlipHorizontal ?? false
		};
	}
}