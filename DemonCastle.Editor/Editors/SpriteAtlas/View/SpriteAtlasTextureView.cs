using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;

namespace DemonCastle.Editor.Editors.SpriteAtlas.View;

public partial class SpriteAtlasTextureView : TextureView {
	private readonly SpriteAtlasInfo _spriteAtlasInfo;
	private readonly List<SpriteAtlasArea> _areas = new();

	public Action<SpriteAtlasDataInfo>? SpriteSelected;

	public SpriteAtlasTextureView(SpriteAtlasInfo spriteAtlasInfo) {
		_spriteAtlasInfo = spriteAtlasInfo;

		Name = nameof(SpriteAtlasTextureView);
		Texture = spriteAtlasInfo.Texture;

		ReloadSpriteData();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_spriteAtlasInfo.PropertyChanged += SpriteAtlasInfo_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_spriteAtlasInfo.PropertyChanged -= SpriteAtlasInfo_OnPropertyChanged;
	}

	public void SelectSprite(SpriteAtlasDataInfo? sprite) {
		foreach (var area in _areas) {
			if (area.Sprite == sprite) area.Select();
			else area.Deselect();
		}
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
			Inner.AddChild(area);
			_areas.Add(area);
			area.Selected += Area_OnSelected;
		}
	}

	private void Area_OnSelected(SpriteAtlasArea selected) {
		foreach (var area in _areas.Where(a => a != selected)) {
			area.Deselect();
		}
		SpriteSelected?.Invoke(selected.Sprite);
	}
}