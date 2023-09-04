using System;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class SelectableTile : TextureRect {
	protected Control SelectionBox;
	public TileInfo Tile;
	public event Action<SelectableTile>? Selected;
	
	public SelectableTile(TileInfo tile) {
		Name = nameof(SelectableTile);
		Tile = tile;
		
		Texture = new AtlasTexture {
			Atlas = tile.Texture,
			Region = tile.Region
		};
		FlipH = tile.FlipHorizontal;
		
		AddChild(SelectionBox = new Control {
			Visible = false
		});
		SelectionBox.AddChild(new ColorRect { // TOP
			Color = Colors.White,
			Position = new Vector2(-1, -1),
			Size = new Vector2(tile.Region.Size.X + 2, 1)
		});
		SelectionBox.AddChild(new ColorRect { // BOTTOM
			Color = Colors.White,
			Position = new Vector2(-1, tile.Region.Size.Y + 1),
			Size = new Vector2(tile.Region.Size.X + 2, 1)
		});
		SelectionBox.AddChild(new ColorRect { // LEFT
			Color = Colors.White,
			Position = new Vector2(-1, -1),
			Size = new Vector2(1, tile.Region.Size.Y + 2)
		});
		SelectionBox.AddChild(new ColorRect { // RIGHT
			Color = Colors.White,
			Position = new Vector2(tile.Region.Size.X + 1, -1),
			Size = new Vector2(1, tile.Region.Size.Y + 2)
		});
	}

	public override void _Process(double delta) {
		base._Process(delta);
		
		if (Input.IsActionJustPressed(InputActions.EditorClick) && MouseWithinBounds()) {
			Selected?.Invoke(this);
			SelectionBox.Visible = true;
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

	public void ClearSelection() {
		SelectionBox.Visible = false;
	}
}