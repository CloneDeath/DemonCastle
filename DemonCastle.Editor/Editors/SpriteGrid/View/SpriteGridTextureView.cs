using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;

namespace DemonCastle.Editor.Editors.SpriteGrid.View;

public partial class SpriteGridTextureView : TextureView {
	protected SpriteGridInfo SpriteGridInfo { get; }

	public SpriteGridTextureView(SpriteGridInfo spriteGridInfo) {
		SpriteGridInfo = spriteGridInfo;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Texture = SpriteGridInfo.Texture;
	}
}