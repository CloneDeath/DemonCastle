using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class ScrollableWrapper<T> : ScrollContainer where T : Control, new() {
	private Control ControlHolder { get; }
	public T Inner { get; }

	public float Zoom { get; set; } = 1;

	public Vector2I ScrollPosition {
		get => new(ScrollHorizontal, ScrollVertical);
		set {
			ScrollHorizontal = value.X;
			ScrollVertical = value.Y;
		}
	}

	public Vector2 MinScrollPosition {
		get => new((float)GetHScrollBar().MinValue, (float)GetVScrollBar().MinValue);
		set {
			GetHScrollBar().MinValue = value.X;
			GetVScrollBar().MinValue = value.Y;
		}
	}

	public Vector2 MaxScrollPosition {
		get => new((float)GetHScrollBar().MaxValue, (float)GetVScrollBar().MaxValue);
		set {
			GetHScrollBar().MaxValue = value.X;
			GetVScrollBar().MaxValue = value.Y;
		}
	}

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
		var target = (position - Size / 2) * Inner.Scale;
		ScrollHorizontal = (int)target.X;
		ScrollVertical = (int)target.Y;
	}
}