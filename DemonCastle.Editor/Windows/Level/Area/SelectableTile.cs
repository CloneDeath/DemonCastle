using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class SelectableTile : TextureRect {
	public SelectableTile(TileInfo tile) {
		Name = nameof(SelectableTile);
		
		Texture = new AtlasTexture {
			Atlas = tile.Texture,
			Region = tile.Region
		};
		FlipH = tile.FlipHorizontal;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		
		if (Input.IsActionJustPressed(InputActions.EditorClick) && MouseWithinBounds()) {
			Modulate = Colors.Blue;
		}
	}

	private bool MouseWithinBounds() {
		var mousePosition = GetViewport().GetMousePosition();
		var myPosition = GlobalPosition;
		var delta = mousePosition - myPosition;
		var size = Size;
		return delta is { X: >= 0, Y: >= 0 }
			   && delta.X < size.X
			   && delta.Y < size.Y;
	}
}