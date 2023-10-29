using System;
using DemonCastle.Editor.Editors.Level.Area;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;
using TileView = DemonCastle.Editor.Editors.Level.Area.AreaTiles.TileView;

namespace DemonCastle.Editor.Editors.Level.AreaOld; 

public partial class AreaTileEditor : ScrollContainer {
	public event Action<Vector2I>? TileCellSelected;
	public event Action<Vector2I>? TileCellCleared;

	private Vector2I? _previousTriggeredPosition;
	private bool? _previousTriggerWasSelect;
	
	private void LoadArea(AreaInfo areaInfo) {
		foreach (var tileMapInfo in areaInfo.TileMap) {
			Root.AddChild(new TileView(tileMapInfo));
		}
	}

	private void ClearChildren() {
		foreach (var child in Root.GetChildren()) {
			child.QueueFree();
		}
	}

	public override void _GuiInput(InputEvent @event) {
		base._GuiInput(@event);

		if (@event is InputEventMouseMotion) {
			if (Input.IsActionPressed(InputActions.EditorClick)) {
				TriggerTileCellSelected();
			} else if (Input.IsActionPressed(InputActions.EditorRightClick)) {
				TriggerTileCellCleared();
			}
		} else if (@event.IsActionPressed(InputActions.EditorClick) && MouseWithinBounds()) {
			TriggerTileCellSelected();
		} else if (@event.IsActionPressed(InputActions.EditorRightClick) && MouseWithinBounds()) {
			TriggerTileCellCleared();
		}
	}

	private void TriggerTileCellSelected() {
		var index = GetTileIndexOfMousePosition();
		if (!IndexIsValid(index)) return;
		if (_previousTriggeredPosition == index && _previousTriggerWasSelect == true) return;
		TileCellSelected?.Invoke(index);
		_previousTriggeredPosition = index;
		_previousTriggerWasSelect = true;
	}

	private void TriggerTileCellCleared() {
		var index = GetTileIndexOfMousePosition();
		if (!IndexIsValid(index)) return;
		if (_previousTriggeredPosition == index && _previousTriggerWasSelect == false) return;
		TileCellCleared?.Invoke(index);
		_previousTriggeredPosition = index;
		_previousTriggerWasSelect = false;
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