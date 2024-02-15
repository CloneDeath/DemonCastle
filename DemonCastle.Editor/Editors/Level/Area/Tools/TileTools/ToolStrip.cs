using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public enum TileTool {
	Brush,
	Fill
}

public partial class ToolStrip : VBoxContainer {
	public TileTool SelectedTool { get; private set; } = TileTool.Brush;

	protected Button BrushTool { get; }
	protected Button FillTool { get; }

	public ToolStrip() {
		Name = nameof(ToolStrip);

		var group = new ButtonGroup();
		AddChild(BrushTool = new Button {
			Icon = IconTextures.BrushToolIcon,
			ToggleMode = true,
			ButtonGroup = group,
			ButtonPressed = true
		});

		AddChild(FillTool = new Button {
			Icon = IconTextures.FillToolIcon,
			ToggleMode = true,
			ButtonGroup = group
		});
	}
}