using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;

public partial class WeaponFrameEditor : SelectableControl {
	private static readonly Color SelectedColor = Colors.White;
	private static readonly Color DeselectedColor = new(Colors.White, 0.25f);

	private Outline Outline { get; }

	public WeaponFrameEditor(WeaponFrameInfo frame) {
		Name = nameof(WeaponFrameEditor);
		CustomMinimumSize = new Vector2(60, 60);

		SelectedCursorShape = CursorShape.Arrow;
		DefaultCursorShape = CursorShape.PointingHand;

		AddChild(Outline = new Outline{ MouseFilter = MouseFilterEnum.Ignore});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect);

		AddChild(new Label {
			Text = $"{frame.Index} - {frame.Duration}s"
		});
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Outline.Color = IsSelected ? SelectedColor : DeselectedColor;
	}

	protected override void OnSelected() {
		base.OnSelected();
		DeselectSiblings();
	}
}