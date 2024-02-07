using Godot;

namespace DemonCastle.Editor.Editors.Components.ControlViewComponent;

public partial class ControlViewFooter : HBoxContainer {
	protected Label SizeLabel { get; }
	protected Label MousePosition { get; }

	public ControlViewFooter() {
		Name = nameof(ControlViewFooter);
		AddChild(SizeLabel = new Label());
		AddChild(MousePosition = new Label { Visible = false });
	}

	public bool MousePositionVisible {
		get => MousePosition.Visible;
		set => MousePosition.Visible = value;
	}

	public void SetSizeText(Vector2I size) {
		SizeLabel.Text = $"{size.X}x{size.Y}";
	}

	public void SetMousePositionText(Vector2I position) {
		MousePosition.Text = $"@{position.X}x{position.Y}";
	}
}