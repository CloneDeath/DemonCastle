using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Properties;

namespace DemonCastle.Editor.Editors.SpriteGrid.Details;

public partial class SpriteDetails : PropertyCollection {
	public SpriteDetails(SpriteGridDataInfoProxy spriteData) {
		Name = nameof(SpriteDetails);
		
		AddString("Name", spriteData, x => x.Name);
		AddInteger("X", spriteData, x => x.X);
		AddInteger("Y", spriteData, x => x.Y);
		AddBoolean("Flip Horizontal", spriteData, x => x.FlipHorizontal);
		
		AddChild(new SpriteDefinitionView(spriteData));
	}
}