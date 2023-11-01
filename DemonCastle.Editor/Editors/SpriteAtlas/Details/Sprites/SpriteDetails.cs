using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Details.Sprites;

public partial class SpriteDetails : PanelContainer {
	protected PropertyCollection Properties { get; }

	public SpriteDetails(SpriteAtlasDataInfo spriteData) {
		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", spriteData, x => x.Name);
		Properties.AddRect2I("Region", spriteData, x => x.Region);
		Properties.AddBoolean("Flip Horizontal", spriteData, x => x.FlipHorizontal);
		Properties.AddChild(new SpriteDefinitionView(spriteData));
	}
}