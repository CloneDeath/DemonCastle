using Godot;

namespace DemonCastle.Editor.Extensions; 

public static class NodeExtensions {
	public static WindowContainer GetWindowContainer(this Node self) {
		return (WindowContainer)self.GetTree().GetFirstNodeInGroup(nameof(WindowContainer));
	}
}