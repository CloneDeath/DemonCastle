using System;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Level.Area.View.Monsters;
using DemonCastle.Editor.Editors.Level.Area.View.Tiles;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View;

public partial class AreaView : SelectableControl {
	protected AreaInfo Area { get; }

	protected Outline Outline { get; }
	protected AreaTilesView Tiles { get; }
	protected AreaMonstersView Monsters { get; }

	public bool ShowSingleLayer {
		get => Tiles.ShowSingleLayer;
		set => Tiles.ShowSingleLayer = value;
	}

	public int SelectedLayerIndex {
		get => Tiles.SelectedLayerIndex;
		set => Tiles.SelectedLayerIndex = value;
	}

	protected static readonly Color SelectedColor = new(Colors.White, 0.75f);
	protected static readonly Color DeselectedColor = new(Colors.White, 0.3f);

	public event Action<AreaInfo, Vector2I>? AreaTileSelected;
	public event Action<AreaInfo, Vector2I>? AreaTileCleared;
	private Vector2I? _previousTriggeredPosition;
	private bool? _previousTriggerWasSelect;

	public AreaView(ProjectResources resources, AreaInfo area) {
		Area = area;

		Name = nameof(AreaView);
		SelectedCursorShape = CursorShape.Cross;
		DefaultCursorShape = CursorShape.PointingHand;

		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore,
			Color = DeselectedColor,
			ZIndex = 10
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);
		AddChild(Tiles = new AreaTilesView(area) {
			MouseFilter = MouseFilterEnum.Pass
		});
		AddChild(Monsters = new AreaMonstersView(resources, area) {
			MouseFilter = MouseFilterEnum.Pass
		});
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = Area.PositionOfArea.ToPixelPositionInLevel();
		Size = Area.SizeOfArea.ToPixelSize();
		Outline.Color = IsSelected ? SelectedColor : DeselectedColor;
		Outline.Thickness = IsSelected ? 2 : 1;
	}

	protected override void OnSelected() {
		base.OnSelected();
		DeselectSiblings();
	}

	public override void _GuiInput(InputEvent @event) {
		var isSelected = IsSelected;
		base._GuiInput(@event);
		if (@event is not InputEventMouse mouseEvent) return;
		if (!isSelected) return;

		if (mouseEvent is InputEventMouseMotion) {
			if (Input.IsActionPressed(InputActions.EditorClick)) {
				TriggerTileCellSelected(mouseEvent.Position);
			} else if (Input.IsActionPressed(InputActions.EditorRightClick)) {
				TriggerTileCellCleared(mouseEvent.Position);
			}
		} else if (mouseEvent.IsActionPressed(InputActions.EditorClick)) {
			TriggerTileCellSelected(mouseEvent.Position);
		} else if (mouseEvent.IsActionPressed(InputActions.EditorRightClick)) {
			TriggerTileCellCleared(mouseEvent.Position);
		}
	}

	private void TriggerTileCellSelected(Vector2 position) {
		var index = GetTileIndexOfMousePosition(position);
		if (_previousTriggeredPosition == index && _previousTriggerWasSelect == true) return;
		_previousTriggeredPosition = index;
		_previousTriggerWasSelect = true;
		if (index >= Vector2I.Zero && index < Area.SizeOfArea.ToPixelSize()) {
			AreaTileSelected?.Invoke(Area, index);
		}
	}

	private void TriggerTileCellCleared(Vector2 position) {
		var index = GetTileIndexOfMousePosition(position);
		if (_previousTriggeredPosition == index && _previousTriggerWasSelect == false) return;
		_previousTriggeredPosition = index;
		_previousTriggerWasSelect = false;
		if (index >= Vector2I.Zero && index < Area.SizeOfArea.ToPixelSize()) {
			AreaTileCleared?.Invoke(Area, index);
		}
	}

	private Vector2I GetTileIndexOfMousePosition(Vector2 position) {
		var index = (Vector2I)(position / Area.TileSize);
		return index;
	}
}