using System;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Editor.Editors.TileSet.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.Collision;

public partial class TileCollisionView : HBoxContainer {
	private readonly TileProxy _tile;
	public Label Description { get; }
	public Button ButtonMakeSolid { get; }
	public Button ButtonMakeEmpty { get; }

	public TileCollisionView(TileProxy tile) {
		_tile = tile;

		Name = nameof(TileCollisionView);
		AddChild(new Label { Text = "Collision" });
		AddChild(Description = new Label());
		AddChild(ButtonMakeSolid = new Button { Text = "Make Solid" });
		ButtonMakeSolid.Pressed += ButtonMakeSolid_OnPressed;
		AddChild(ButtonMakeEmpty = new Button { Text = "Make Empty" });
		ButtonMakeEmpty.Pressed += ButtonMakeEmpty_OnPressed;
		Reload();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_tile.PropertyChanged += Tile_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_tile.PropertyChanged -= Tile_OnPropertyChanged;
	}

	private void Tile_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(_tile.Collision)) return;
		Reload();
	}

	public void Reload() {
		Description.Text = $"[{string.Join(",", _tile.Collision.Select(v => $"({v.X},{v.Y})"))}]";
	}

	private void ButtonMakeSolid_OnPressed() {
		_tile.Collision = new[] {
			Vector2.Zero,
			Vector2.Right,
			Vector2.One,
			Vector2.Down
		};
	}

	private void ButtonMakeEmpty_OnPressed() {
		_tile.Collision = Array.Empty<Vector2>();
	}
}