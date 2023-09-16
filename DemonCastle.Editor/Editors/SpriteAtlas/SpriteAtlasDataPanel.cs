using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas; 

public partial class SpriteAtlasDataPanel : PanelContainer {
	protected PropertyCollection Properties { get; }
	public SpriteAtlasDataPanel(SpriteAtlasDataInfo spriteData) {
		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", spriteData, x => x.Name);
		Properties.AddRect2I("Region", spriteData, x => x.Region);
	}
}