using System;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class ToolStrip : VBoxContainer {
	public event Action<TileTool>? ToolChanged;
	public TileTool SelectedTool { get; private set; } = TileTool.Brush;

	protected Button BrushTool { get; }
	protected Button FillTool { get; }

	public ToolStrip() {
		Name = nameof(ToolStrip);

		var group = new ButtonGroup();
		group.Pressed += Group_OnPressed;

		AddChild(BrushTool = new TileToolButton(TileTool.Brush) {
			Icon = IconTextures.BrushToolIcon,
			ToggleMode = true,
			ButtonGroup = group,
			ButtonPressed = true
		});

		AddChild(FillTool = new TileToolButton(TileTool.Fill) {
			Icon = IconTextures.FillToolIcon,
			ToggleMode = true,
			ButtonGroup = group
		});
	}

	private void Group_OnPressed(BaseButton button) {
		SelectedTool = ((TileToolButton)button).Tool;
		ToolChanged?.Invoke(SelectedTool);
	}
}