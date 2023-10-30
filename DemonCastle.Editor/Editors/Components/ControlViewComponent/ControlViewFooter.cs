using Godot;

namespace DemonCastle.Editor.Editors.Components.ControlViewComponent;

public partial class ControlViewFooter : HBoxContainer {
	protected Label Footer_SizeLabel { get; }
	protected Label Footer_MousePosition { get; }

	public ControlViewFooter() {
		Name = nameof(ControlViewFooter);
		AddChild(Footer_SizeLabel = new Label());
		AddChild(Footer_MousePosition = new Label { Visible = false });
	}

	public bool MousePositionVisible {
		get => Footer_MousePosition.Visible;
		set => Footer_MousePosition.Visible = value;
	}

	public void SetSizeText(Vector2I size) {
		Footer_SizeLabel.Text = $"{size.X}x{size.Y}";
	}

	public void SetMousePositionText(Vector2I position) {
		Footer_MousePosition.Text = $"@{position.X}x{position.Y}";
	}
}