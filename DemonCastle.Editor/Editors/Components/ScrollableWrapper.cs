using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class ScrollableWrapper<T> : ScrollContainer where T : Control, new() {
	private Control ControlHolder { get; }
	public T Inner { get; }

	public float Zoom { get; set; } = 1;

	public ScrollableWrapper() {
		Name = nameof(ScrollableWrapper<T>);

		AddChild(ControlHolder = new Control());

		ControlHolder.AddChild(Inner = new T());
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Inner.Scale = Vector2.One * Zoom;
		ControlHolder.CustomMinimumSize = Inner.Size * Inner.Scale;
	}

	public void CenterOnPosition(Vector2 position) {
		var target = position - Size / 2;
		ScrollHorizontal = (int)target.X;
		ScrollVertical = (int)target.Y;
	}
}