using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Properties;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Details.Sprites;

public partial class SpriteDetails : PropertyCollection {
	public SpriteDetails(SpriteAtlasDataInfoProxy spriteData) {
		Name = nameof(SpriteDetails);

		AddString("Name", spriteData, x => x.Name);
		AddRect2I("Region", spriteData, x => x.Region);
		AddBoolean("Flip Horizontal", spriteData, x => x.FlipHorizontal);
		AddChild(new SpriteDefinitionView(spriteData));
	}
}