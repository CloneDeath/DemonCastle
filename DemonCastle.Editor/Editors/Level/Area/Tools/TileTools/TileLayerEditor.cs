using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileLayerEditor : HBoxContainer {
	public TileLayerEditor() {
		Name = nameof(TileLayerEditor);

		AddChild(new Label { Text = "Layers" });
		AddChild(new OptionButton {
			Text = "0 - Default",
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		AddChild(new Button { Icon = IconTextures.AddIcon });
		AddChild(new Button { Icon = IconTextures.DeleteIcon });
	}
}