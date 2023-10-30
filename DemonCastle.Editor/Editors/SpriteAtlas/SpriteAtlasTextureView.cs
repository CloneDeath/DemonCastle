using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;

namespace DemonCastle.Editor.Editors.SpriteAtlas;

public partial class SpriteAtlasTextureView : TextureView {
	private readonly SpriteAtlasInfo _spriteAtlasInfo;
	private readonly List<SpriteAtlasArea> _areas = new();

	public SpriteAtlasTextureView(SpriteAtlasInfo spriteAtlasInfo) {
		_spriteAtlasInfo = spriteAtlasInfo;
		Texture = spriteAtlasInfo.Texture;

		foreach (var dataInfo in spriteAtlasInfo.AtlasSprites) {
			var area = new SpriteAtlasArea(dataInfo);
			MainControl.Inner.AddChild(area);
			_areas.Add(area);
			area.Selected += Area_OnSelected;
		}

		spriteAtlasInfo.PropertyChanged += SpriteAtlasInfo_OnPropertyChanged;
	}

	private void SpriteAtlasInfo_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(_spriteAtlasInfo.SpriteFile)) return;
		Texture = _spriteAtlasInfo.Texture;
	}

	private void Area_OnSelected(SpriteAtlasArea selected) {
		foreach (var area in _areas.Where(a => a != selected)) {
			area.Deselect();
		}
	}
}