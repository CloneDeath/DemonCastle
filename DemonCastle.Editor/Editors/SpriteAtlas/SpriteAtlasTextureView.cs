using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;

namespace DemonCastle.Editor.Editors.SpriteAtlas;

public partial class SpriteAtlasTextureView : TextureView {
	private readonly List<SpriteAtlasArea> _areas = new();

	public SpriteAtlasTextureView(SpriteAtlasInfo spriteAtlasInfo) {
		Texture = spriteAtlasInfo.Texture;

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