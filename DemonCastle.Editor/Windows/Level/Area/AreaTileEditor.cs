using System;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class AreaTileEditor : ScrollContainer {
	public event Action<Vector2I>? TileCellSelected;
	public event Action<Vector2I>? TileCellCleared;
	
	private void LoadArea(AreaInfo areaInfo) {
		foreach (var tileMapInfo in areaInfo.TileMap) {
			Root.AddChild(new TileCell(tileMapInfo));
		}
	}

	private void ClearChildren() {
		foreach (var child in Root.GetChildren()) {
			child.QueueFree();
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		if (Input.IsActionJustPressed(InputActions.EditorClick) && MouseWithinBounds()) {
			var index = GetTileIndexOfMousePosition();
			if (IndexIsValid(index)) {
				TileCellSelected?.Invoke(index);
			}
		} else if (Input.IsActionJustPressed(InputActions.EditorRightClick) && MouseWithinBounds()) {
			var index = GetTileIndexOfMousePosition();
			if (IndexIsValid(index)) {
				TileCellCleared?.Invoke(index);
			}
		}
	}

	private bool IndexIsValid(Vector2I index) => index is { X: >= 0, Y: >= 0 } && index.X < Area.AreaSize.X && index.Y < Area.AreaSize.Y;

	private Vector2I GetTileIndexOfMousePosition() {
		var mousePosition = GetViewport().GetMousePosition();
		var myPosition = GlobalPosition;
		var offset = mousePosition - myPosition;
		var index = (Vector2I)(offset / Area.TileSize);
		return index;
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

	public void SetTile(Vector2I position, string tileName) {
		Area.SetTile(position, tileName);
		ClearChildren();
		LoadArea(Area);
	}

	public void ClearTile(Vector2I position) {
		Area.ClearTile(position);
		ClearChildren();
		LoadArea(Area);
	}
}