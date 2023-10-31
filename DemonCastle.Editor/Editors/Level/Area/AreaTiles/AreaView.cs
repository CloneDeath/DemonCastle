using System;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class AreaView : SelectableControl {
	protected AreaInfo Area { get; }

	protected Outline Outline { get; }
	protected AreaTilesView Root { get; }

	private static readonly Color SelectedColor = new(Colors.White, 0.75f);
	private static readonly Color DeselectedColor = new(Colors.White, 0.3f);

	public event Action<AreaInfo, Vector2I>? AreaTileSelected;
	public event Action<AreaInfo, Vector2I>? AreaTileCleared;
	private Vector2I? _previousTriggeredPosition;
	private bool? _previousTriggerWasSelect;

	public AreaView(AreaInfo area) {
		Area = area;

		Name = nameof(AreaView);
		SelectedCursorShape = CursorShape.Cross;
		DefaultCursorShape = CursorShape.PointingHand;

		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore,
			Color = DeselectedColor
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);
		AddChild(Root = new AreaTilesView(area));
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = Area.PositionOfArea.ToLevelPositionInPixels();
		Size = Area.SizeOfArea.ToPixelSize();
		Outline.Color = IsSelected ? SelectedColor : DeselectedColor;
		Outline.Thickness = IsSelected ? 2 : 1;
	}

	protected override void OnSelected() {
		base.OnSelected();
		DeselectSiblings();
	}

	public override void _GuiInput(InputEvent @event) {
		base._GuiInput(@event);
		if (!IsSelected) return;

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
		AreaTileSelected?.Invoke(Area, index);
		_previousTriggeredPosition = index;
		_previousTriggerWasSelect = true;
	}

	private void TriggerTileCellCleared() {
		var index = GetTileIndexOfMousePosition();
		if (!IndexIsValid(index)) return;
		if (_previousTriggeredPosition == index && _previousTriggerWasSelect == false) return;
		AreaTileCleared?.Invoke(Area, index);
		_previousTriggeredPosition = index;
		_previousTriggerWasSelect = false;
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

	private bool IndexIsValid(Vector2I index) => index is { X: >= 0, Y: >= 0 } && index.X < Area.AreaSize.X && index.Y < Area.AreaSize.Y;

	private Vector2I GetTileIndexOfMousePosition() {
		var mousePosition = GetViewport().GetMousePosition();
		var myPosition = GlobalPosition;
		var offset = mousePosition - myPosition;
		var index = (Vector2I)(offset / Area.TileSize);
		return index;
	}
}