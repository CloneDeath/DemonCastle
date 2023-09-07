using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.TileMap; 

public partial class TileWindow : BaseWindow {
	protected PropertyCollection Properties { get; }

	public TileWindow(TileInfo tileInfo) {
		Name = nameof(TileWindow);
		Title = $"Tile Window";
		Size = new Vector2I(80, 100);
		MinSize = Size;
		
		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", tileInfo, x => x.Name);
		Properties.AddFile("Source", tileInfo, tileInfo.Directory,  t => t.SourceFile);
		Properties.AddString("Sprite", tileInfo, x => x.SpriteName); 
	}
}