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

		ReloadSpriteData();

		spriteAtlasInfo.PropertyChanged += SpriteAtlasInfo_OnPropertyChanged;
	}

	private void SpriteAtlasInfo_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (e.PropertyName) {
			case nameof(_spriteAtlasInfo.SpriteFile):
				Texture = _spriteAtlasInfo.Texture;
				break;
			case nameof(_spriteAtlasInfo.AtlasSprites):
				ReloadSpriteData();
				break;
		}
	}

	private void ReloadSpriteData() {
		foreach (var area in _areas) {
			area.QueueFree();
		}
		_areas.Clear();

		foreach (var dataInfo in _spriteAtlasInfo.AtlasSprites) {
			var area = new SpriteAtlasArea(dataInfo);
			MainControl.Inner.AddChild(area);
			_areas.Add(area);
			area.Selected += Area_OnSelected;
		}
	}

	private void Area_OnSelected(SpriteAtlasArea selected) {
		foreach (var area in _areas.Where(a => a != selected)) {
			area.Deselect();
		}
	}
}