using Godot;

namespace DemonCastle.Editor.Extensions; 

public static class NodeExtensions {
	public static EditArea GetEditArea(this Node self) {
		return (EditArea)self.GetTree().GetFirstNodeInGroup(nameof(EditArea));
	}
}