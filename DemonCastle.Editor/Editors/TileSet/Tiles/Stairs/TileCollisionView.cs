using System.ComponentModel;
using DemonCastle.Editor.Editors.TileSet.Tiles.Collision;
using DemonCastle.Files;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet.Tiles.Stairs;

public partial class TileStairView : HBoxContainer {
	private readonly TileProxy _tile;

	public TileStairView(TileProxy tile) {
		_tile = tile;

		Name = nameof(TileCollisionView);
		AddChild(new Label { Text = "Stairs" });
		AddChild(Description = new Label());
		AddChild(ButtonStairsUp = new Button { Text = "Stairs Up" });
		ButtonStairsUp.Pressed += ButtonStairsUp_OnPressed;
		AddChild(ButtonStairsDown = new Button { Text = "Stairs Down" });
		ButtonStairsDown.Pressed += ButtonStairsDown_OnPressed;
		AddChild(ButtonNoStairs = new Button { Text = "Make Empty" });
		ButtonNoStairs.Pressed += ButtonNoStairs_OnPressed;
		Reload();
	}

	public Label Description { get; }
	public Button ButtonStairsUp { get; }
	public Button ButtonStairsDown { get; }
	public Button ButtonNoStairs { get; }

	public override void _EnterTree() {
		base._EnterTree();
		_tile.PropertyChanged += Tile_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_tile.PropertyChanged -= Tile_OnPropertyChanged;
	}

	private void Tile_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(_tile.Stairs)) return;
		Reload();
	}

	public void Reload() {
		var stair = _tile.Stairs;
		if (stair == null) {
			Description.Text = "No Stairs";
			return;
		}

		Description.Text = $"[({stair.Start.X},{stair.Start.Y}) -> ({stair.End.X},{stair.End.Y})]";
	}

	private void ButtonStairsUp_OnPressed() {
		_tile.Stairs = new StairData {
			Start = Vector2.Down,
			End = Vector2.Right
		};
	}

	private void ButtonStairsDown_OnPressed() {_tile.Stairs = new StairData {
			Start = Vector2.Zero,
			End = Vector2.One
		};
	}

	private void ButtonNoStairs_OnPressed() {
		_tile.Stairs = null;
	}
}