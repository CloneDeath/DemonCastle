using System.Linq;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class ExpandingControl : Control {
	public override void _Process(double delta) {
		base._Process(delta);

		var children = GetChildren().Where(c => c is Control).Cast<Control>().ToList();
		if (children.Count <= 0) {
			Size = Vector2.Zero;
			CustomMinimumSize = Vector2.Zero;
		}

		var minimumBounds = GetBounds(children[0]);
		foreach (var child in children.Skip(1)) {
			var bounds = GetBounds(child);
			minimumBounds = minimumBounds.Merge(bounds);
		}

		Size = minimumBounds.Size;
		CustomMinimumSize = minimumBounds.Size;
	}

	protected static Rect2 GetBounds(Control child) {
		var position = child.Position;
		var size = child.Size;
		return new Rect2(position, size);
	}
}