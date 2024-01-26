using DemonCastle.Editor.Editors.Components;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.TileSets;

public partial class TileSetSelector : VBoxContainer {
	private AddRemoveOptionButton _options;

	public TileSetSelector() {
		Name = nameof(TileSetSelector);

		AddChild(_options = new AddRemoveOptionButton {
			Label = "Tile Set"
		});
		_options.AddItem("Outdoors");
		_options.AddItem("Caves");
	}
}