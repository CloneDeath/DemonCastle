using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class ToolStrip : VBoxContainer {
	public ToolStrip() {
		Name = nameof(ToolStrip);

		AddChild(new Button {
			Icon = IconTextures.BrushToolIcon,
			ToggleMode = true,
			ButtonPressed = true
		});
		AddChild(new Button {
			Icon = IconTextures.FillToolIcon,
			ToggleMode = true
		});
	}
}