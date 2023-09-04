using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class TileCell : Node2D {
	public TileCell(TileInfo tile, TileMapInfo tileMapInfo) {
		Position = tileMapInfo.AreaPosition;

		const int borderWidth = 2;
		AddChild(new TextureRect {
			Texture = tile.Texture,
			Size = tileMapInfo.TileSize
		});	
	}
}