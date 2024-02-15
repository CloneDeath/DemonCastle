using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public enum TileTool {
	Brush,
	Fill
}

public partial class TileToolButton : Button {
	public TileTool Tool { get; }

	public TileToolButton(TileTool tool) {
		Tool = tool;
	}
}