using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.TileMap; 

public partial class TileEditor : Control {
	protected PropertyCollection Properties { get; }

	public TileEditor(TileInfo tileInfo) {
		Name = $"Tile Window";
		CustomMinimumSize = new Vector2I(80, 100);
		
		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", tileInfo, x => x.Name);
		Properties.AddFile("Source", tileInfo, tileInfo.Directory,  t => t.SourceFile);
		Properties.AddString("Sprite", tileInfo, x => x.SpriteName); 
	}
}