using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Details.Sprites;

public partial class SpriteAtlasDataDetails : PropertyCollection {
	public SpriteAtlasDataDetails(SpriteAtlasDataInfoProxy spriteData) {
		Name = nameof(SpriteAtlasDataDetails);

		AddString("Name", spriteData, x => x.Name);
		AddRect2I("Region", spriteData, x => x.Region);
		AddBoolean("Flip Horizontal", spriteData, x => x.FlipHorizontal);
		AddBoolean("Flip Vertical", spriteData, x => x.FlipVertical);
		AddChild(new SpriteDefinitionView(spriteData));
	}
}