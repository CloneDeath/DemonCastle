using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class TileSelectorPanel : HFlowContainer {
	public TileSelectorPanel(LevelTileSet tileSet) {
		foreach (var tile in tileSet.Tiles) {
			AddChild(new TextureRect {
				Texture = new AtlasTexture {
					Atlas = tile.Texture,
					Region = tile.Region
				}
			});
		}
	}
}