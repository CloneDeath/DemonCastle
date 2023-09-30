using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas; 

public partial class SpriteAtlasTextureEditor : ScrollContainer {
	protected TextureRect TextureRect { get; }

	private readonly List<SpriteAtlasArea> _areas = new();
	
	public SpriteAtlasTextureEditor(SpriteAtlasInfo spriteAtlasInfo) {
		AddChild(TextureRect = new TextureRect {
			Texture = spriteAtlasInfo.Texture
		});

		foreach (var dataInfo in spriteAtlasInfo.SpriteData) {
			var area = new SpriteAtlasArea(dataInfo);
			AddChild(area);
			_areas.Add(area);
			area.Selected += Area_OnSelected;
		}
	}

	private void Area_OnSelected(SpriteAtlasArea selected) {
		foreach (var area in _areas.Where(a => a != selected)) {
			area.IsSelected = false;
		}
	}
}