using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.Editor.Editors.SpriteAtlas; 

public partial class SpriteAtlasArea : Outline {
	private readonly SpriteAtlasDataInfo _info;

	public SpriteAtlasArea(SpriteAtlasDataInfo info) {
		_info = info;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = _info.Position;
		Size = _info.Size;
	}
}