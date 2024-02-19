using System.ComponentModel;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Components.Properties.Vector;
using DemonCastle.Editor.Editors.TileSet.Tiles.Collision;

namespace DemonCastle.Editor.Editors.TileSet.Tiles.Stairs;

public partial class TileStairView : NamedPropertyCollection {
	private readonly TileProxy _tile;

	private Vector2Property Start { get; }
	private Vector2Property End { get; }

	public TileStairView(TileProxy tile) {
		_tile = tile;
		Name = nameof(TileCollisionView);
		DisplayName = "Stairs";

		AddBoolean("Enabled", tile.Stairs, t => t.Enabled);
		Start = AddVector2("Start", tile.Stairs, t => t.Start);
		End = AddVector2("End", tile.Stairs, t => t.End);
		Refresh();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_tile.Stairs.PropertyChanged += Tile_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_tile.Stairs.PropertyChanged -= Tile_OnPropertyChanged;
	}

	private void Tile_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Refresh();
	}

	private void Refresh() {
		if (_tile.Stairs.Enabled) {
			Start.Enable();
			End.Enable();
		} else {
			Start.Disable();
			End.Disable();
		}
	}
}